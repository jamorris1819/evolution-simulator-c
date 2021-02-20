using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Managers;
using Engine.Render;
using Engine.Render.Core;
using Engine.Render.Core.Data.Primitives;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolution.Environment.Life.Creatures
{
    public class Leg
    {
        private PositionComponent _parentPosition;

        private readonly Entity[] _entities;

        private readonly Entity _parent;
        private readonly Vector2 _baseOffset;
        private readonly float _legDirection;
        private readonly float _length;
        private bool _isDown;
        private Vector2 _footPosition;

        public bool Initialised { get; private set; }

        public Leg Counterpart { get; set; }

        public bool IsFootDown => _isDown;

        public Vector2 BasePoint => _parentPosition.Position + Rotate(_baseOffset, _parentPosition.Angle);

        Entity debugEntity;
        Entity debugEntity2;
        Entity debugEntity3;

        public Leg(Entity parent, Vector2 baseOffset, float legDirection, float length, float initialProgress)
        {
            _parent = parent;
            _baseOffset = baseOffset;
            _legDirection = legDirection;
            _length = length;

            _parentPosition = parent.GetComponent<PositionComponent>();
            _footPosition = BasePoint + new Vector2(-(float)Math.Sin(_parentPosition.Angle + _legDirection),
                                        (float)Math.Cos(_parentPosition.Angle + _legDirection)) * _length;

            _entities = new Entity[2];

            var dist = Math.Sin(_legDirection) * _length * -2 * initialProgress;

            _footPosition += new Vector2(-(float)Math.Sin(_parentPosition.Angle), (float)Math.Cos(_parentPosition.Angle)) * (float)dist;

            _isDown = true;
        }

        public void Initialise(EntityManager entityManager)
        {
            if (Initialised) return;

            // create shape
            var legShape = Rectangle.Generate(_length * 0.5f, 0.025f);
            legShape = VertexHelper.Translate(legShape, new Vector2(_length * 0.25f, 0));
            legShape = VertexHelper.SetColour(legShape, new Vector3(0.4f));

            for (int i = 0; i < 2; i++)
            {
                _entities[i] = new Entity("leg");
                _entities[i].AddComponent(new PositionComponent(new Vector2(0, 0)));
                var rc = new RenderComponent(legShape);
                rc.VertexArrayObject.Outlined = true;
                rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
                _entities[i].AddComponent(rc);
            }

            entityManager.AddEntities(_entities);

            debugEntity = new Entity("debug");
            debugEntity.AddComponent(new PositionComponent());
            var rc2 = new RenderComponent(VertexHelper.SetColour(Circle.Generate(0.075f, 32), new Vector3(1, 0, 0)));
            rc2.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            rc2.VertexArrayObject.Outlined = true;
            debugEntity.AddComponent(rc2);

            entityManager.AddEntity(debugEntity);

            debugEntity2 = new Entity("debug");
            debugEntity2.AddComponent(new PositionComponent());
            rc2 = new RenderComponent(VertexHelper.SetColour(Circle.Generate(0.075f, 32), new Vector3(0, 1, 0)));
            rc2.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            rc2.VertexArrayObject.Outlined = true;
            debugEntity2.AddComponent(rc2);

            entityManager.AddEntity(debugEntity2);

            debugEntity3 = new Entity("debug");
            debugEntity3.AddComponent(new PositionComponent());
            rc2 = new RenderComponent(VertexHelper.SetColour(Circle.Generate(0.075f, 32), new Vector3(0, 0, 1)));
            rc2.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            rc2.VertexArrayObject.Outlined = true;
            debugEntity3.AddComponent(rc2);

            entityManager.AddEntity(debugEntity3);

            Initialised = true;
        }

        public void StepDown()
        {
            var delta = _footPosition - BasePoint;

            if(delta.Length < _length)
            {
                _isDown = true;
            }
        }

        public void Update(float deltaTime, float bodySpeed)
        {
            if (!Initialised) return;

            // Calculate updated positions
            var delta = _footPosition - BasePoint;

            if (_isDown)
            {
                if(delta.Length > _length)
                {
                    _isDown = false;

                    Counterpart?.StepDown();
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
                    _footPosition += deltaAim.Normalized() * deltaTime * (bodySpeed) * 4f;
                }
            }

            debugEntity.GetComponent<PositionComponent>().Position = _footPosition;
            debugEntity3.GetComponent<PositionComponent>().Position = BasePoint;

            // Move entities to represent joints
            var elbowAngle = Math.Acos(delta.Length / _length) * Math.Sign(_legDirection);
            var footDirection = Math.Atan2(_footPosition.Y - BasePoint.Y, _footPosition.X - BasePoint.X);

            var elbowX = BasePoint.X + Math.Cos(footDirection + elbowAngle) * _length * 0.5f;
            var elbowY = BasePoint.Y + Math.Sin(footDirection + elbowAngle) * _length * 0.5f;

            debugEntity2.GetComponent<PositionComponent>().Position = new Vector2((float)elbowX, (float)elbowY);

            Console.WriteLine(Vector2.Distance(new Vector2((float)elbowX, (float)elbowY), BasePoint));
        }

        private static Vector2 Rotate(Vector2 v, float rad)
        {
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);

            float tx = v.X;
            float ty = v.Y;
            v.X = (cos * tx) - (sin * ty);
            v.Y = (sin * tx) + (cos * ty);
            return v;
        }

        private float GetAngle(Vector2 pos1, Vector2 pos2)
        {
            return (float)Math.Atan2(pos2.Y - pos1.Y, pos2.X - pos1.X);
        }

        private (Vector2, Vector2) Reach(Vector2 head, Vector2 tail, Vector2 target)
        {
            // returns new head and tail in the format of:
            //   [new_head, new_tail]
            // where `new_head` has been moved to `tgt`

            // calculate the current length
            // (in practice, this should be calculated once and saved,
            //  not re-calculated every time `reach` is called)
            var c_dx = tail.X - head.X;
            var c_dy = tail.Y - head.Y;
            var c_dist = (float)Math.Sqrt(c_dx * c_dx + c_dy * c_dy);

            // calculate the stretched length
            var s_dx = tail.X - target.X;
            var s_dy = tail.Y - target.Y;
            var s_dist = (float)Math.Sqrt(s_dx * s_dx + s_dy * s_dy);

            // calculate how much to scale the stretched line
            var scale = c_dist / s_dist;

            return (
                target,
                new Vector2(target.X + s_dx * scale, target.Y + s_dy * scale));
        }
    }
}
