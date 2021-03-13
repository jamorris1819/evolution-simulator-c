using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using Engine.Render.Core.Shaders;
using Evolution.Environment.Life.Creatures.Limbs;
using Evolution.Genetics.Modules.Limbs;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures.Legs
{
    public sealed class WalkingLimb : Limb
    {
        private bool _isDown;
        private readonly LegModel _model;

        private readonly Entity[] _entities;
        private readonly Vector2 _baseOffset;
        private readonly float _legDirection;
        private readonly float _length;

        public bool IsFootDown => _isDown;

        public Vector2 BasePoint => _parentPosition.Position + Rotate(_baseOffset, _parentPosition.Angle);

        public override LimbType LimbType => LimbType.WalkingLeg;

        /// <summary>
        /// Constructs a leg
        /// </summary>
        public WalkingLimb(Entity parent, EntityManager entityManager, LegModel legModel) : base(parent, entityManager)
        {
            _baseOffset = legModel.BaseOffset;
            _legDirection = legModel.LegDirection;
            _length = legModel.Length;

            _model = legModel;

            _entities = new Entity[3];

            var random = new Random();
            var dist = Math.Sin(_legDirection) * _length * -2 * random.NextDouble();

            _footPosition = BasePoint + new Vector2((float)Math.Sin(_parentPosition.Angle + _legDirection),
                                        (float)Math.Cos(_parentPosition.Angle + _legDirection)) * _length;
            _footPosition += new Vector2((float)Math.Cos(_parentPosition.Angle), -(float)Math.Sin(_parentPosition.Angle)) * (float)dist;

           /* _footPosition = BasePoint + new Vector2(
                        -(float)Math.Sin(_parentPosition.Angle + _legDirection) * _length,
                        (float)Math.Cos(_parentPosition.Angle + _legDirection) * _length
                    );*/

            _isDown = legModel.BaseOffset.X > 0;

            Initialise(entityManager);
        }

        /// <summary>
        /// Initialises the leg and creates entities for rendering
        /// </summary>
        public override void Initialise(EntityManager entityManager)
        {
            if (Initialised) return;

            // Create entity for segment 1
            _entities[0] = new Entity("leg");
            _entities[0].AddComponent(new TransformComponent(new Vector2(0, 0)));
            var rc = new RenderComponent(_model.Segment1);
            rc.Shaders.Add(new ShaderConfiguration(Shaders.Standard)
            {
                SortingLayer = 1
            });
            //rc.Shaders.Add(new ShaderConfiguration(Shaders.StandardOutline));
            rc.Shaders.Add(new ShaderConfiguration(Shaders.StandardShadow)
            {
                SortingLayer = -1
            });
            _entities[0].AddComponent(rc);

            // Create entity for segment 2
            _entities[1] = new Entity("leg");
            _entities[1].AddComponent(new TransformComponent(new Vector2(0, 0)));
            rc = new RenderComponent(_model.Segment2);
            rc.Shaders.Add(new ShaderConfiguration(Shaders.Standard)
            {
                SortingLayer = 1
            });
            //rc.Shaders.Add(new ShaderConfiguration(Shaders.StandardOutline));
            rc.Shaders.Add(new ShaderConfiguration(Shaders.StandardShadow)
            {
                SortingLayer = -1
            });
            _entities[1].AddComponent(rc);

            // Create entity for segment 2
            _entities[2] = new Entity("foot");
            _entities[2].AddComponent(new TransformComponent(new Vector2(0, 0)));
            var foot = Circle.Generate(_model.LegThickness, 32);
            foot = VertexHelper.SetColour(foot, rc.VertexArray.Vertices[0].Colour);
            rc = new RenderComponent(foot);
            rc.Shaders.Add(new ShaderConfiguration(Shaders.Standard)
            {
                SortingLayer = 1
            });
            //rc.Shaders.Add(new ShaderConfiguration(Shaders.StandardOutline));
            rc.Shaders.Add(new ShaderConfiguration(Shaders.StandardShadow)
            {
                SortingLayer = -1
            });
            _entities[2].AddComponent(rc);

            entityManager.AddEntities(_entities);
            Initialised = true;
        }

        /// <summary>
        /// Causes the foot to step down.
        /// </summary>
        public void StepDown()
        {
            var delta = _footPosition - BasePoint;

            if (delta.Length < _length)
            {
                _isDown = true;
            }
        }

        public override void Update(float deltaTime, float bodySpeed)
        {
            if (!Initialised) return;

            UpdateFootPosition(deltaTime, bodySpeed);
            UpdateRenderEntities();
        }

        /// <summary>
        /// Calculates if and where the foot should move
        /// </summary>
        private void UpdateFootPosition(float deltaTime, float bodySpeed)
        {
            var delta = _footPosition - BasePoint;

            if (_isDown)
            {
                if (delta.Length > _length)
                {
                    _isDown = false;

                    if(bodySpeed >0f)
                        ((WalkingLimb)Counterpart)?.StepDown();
                }
            }
            else
            {
                var aim = BasePoint + new Vector2(
                        -(float)Math.Sin(_parentPosition.Angle + _legDirection) * _length,
                        (float)Math.Cos(_parentPosition.Angle + _legDirection) * _length
                    );

                var deltaAim = aim - _footPosition;

                if (deltaAim.Length < _length * 0.1f)
                {
                    _isDown = true;
                }
                else
                {
                    _footPosition += deltaAim.Normalized() * deltaTime * (0.25f + bodySpeed) * 4f;// * (float)Math.Pow(0.8f + _length, 2);
                }
            }
        }

        /// <summary>
        /// Updates the leg segments in order to animate the legs.
        /// </summary>
        private void UpdateRenderEntities()
        {
            var delta = _footPosition - BasePoint;

            var d = delta.Length / _length;

            var elbowAngle = Math.Acos(Math.Abs(d) > 1 ? 1 : d) * Math.Sign(_legDirection);
            var footDirection = Math.Atan2(_footPosition.Y - BasePoint.Y, _footPosition.X - BasePoint.X);

            var elbowX = BasePoint.X + Math.Cos(footDirection + elbowAngle) * _length * 0.5f;
            var elbowY = BasePoint.Y + Math.Sin(footDirection + elbowAngle) * _length * 0.5f;

            if (!double.IsNaN(elbowX))
            {
                var firstLimbDir = new Vector2((float)elbowX, (float)elbowY) - BasePoint;
                var firstLimbAngle = GetAngle(new Vector2(0, 0), firstLimbDir.Normalized());
                _entities[0].GetComponent<TransformComponent>().Position = BasePoint;
                _entities[0].GetComponent<TransformComponent>().Angle = firstLimbAngle;

                var secondLimbDir = _footPosition - new Vector2((float)elbowX, (float)elbowY);
                var secondLimbAngle = GetAngle(new Vector2(0, 0), secondLimbDir.Normalized());
                _entities[1].GetComponent<TransformComponent>().Position = new Vector2((float)elbowX, (float)elbowY);
                _entities[1].GetComponent<TransformComponent>().Angle = secondLimbAngle;
            }

            _entities[2].GetComponent<TransformComponent>().Position = _footPosition;
        }

        private static Vector2 Rotate(Vector2 v, float rad)
        {
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);

            float tx = v.X;
            float ty = v.Y;
            v.X = cos * tx - sin * ty;
            v.Y = sin * tx + cos * ty;
            return v;
        }

        private float GetAngle(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
        }
    }
}
