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
        private readonly float _segmentLength;
        private readonly int _segmentCount;
        private readonly Entity[] _entities;

        private readonly Entity _parent;
        private readonly Vector2 _baseOffset;
        private readonly Vector2 _targetOffset;
        private readonly float _length;
        private bool _isDown;
        private Vector2 _footPosition;

        private float _calculatedMaxLength;

        public bool Initialised { get; private set; }

        public Leg Counterpart { get; set; }

        public bool IsFootDown => _isDown;

        Entity debugEntity;

        public Leg(Entity parent, Vector2 baseOffset, Vector2 targetOffset, float length, int count)
        {
            _parent = parent;
            _baseOffset = baseOffset;
            _targetOffset = targetOffset;

            _length = (targetOffset - new Vector2(0, (_targetOffset - _baseOffset).Y * 2)).Length;


            _segmentLength = length;
            _segmentCount = count + 1;
            _entities = new Entity[_segmentCount];

            var baseToTarget = (_targetOffset - _baseOffset).Normalized();
            _targetOffset = _baseOffset + (baseToTarget * (_segmentCount - 1) * _segmentLength);
            
            _footPosition = Rotate(new Vector2(_targetOffset.X, 0) + _parent.GetComponent<PositionComponent>().Position, _parent.GetComponent<PositionComponent>().Angle);

            if (_targetOffset.X > 0)
            {
                _footPosition = _targetOffset;
                StepDown();
            }
            else
            {

                _footPosition -= new Vector2(0, _footPosition.Y * 0.5f);
            }
        }

        public void Initialise(EntityManager entityManager)
        {
            if (Initialised) return;

            // create shape
            var legShape = Rectangle.Generate(_segmentLength, _segmentLength * 0.2f);
            legShape = VertexHelper.Translate(legShape, new Vector2(_segmentLength * 0.5f, 0));
            legShape = VertexHelper.SetColour(legShape, new Vector3(0.4f));

            _calculatedMaxLength = _segmentLength * (_segmentCount - 1);

            for (int i = 0; i < _segmentCount; i++)
            {
                _entities[i] = new Entity("leg");
                _entities[i].AddComponent(new PositionComponent(new Vector2(_segmentLength * i, 0)));
                var rc = new RenderComponent(legShape);
                rc.VertexArrayObject.Outlined = true;
                rc.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
                _entities[i].AddComponent(rc);
            }

            _entities[0].GetComponent<RenderComponent>().VertexArrayObject.Enabled = false;
            entityManager.AddEntities(_entities);

            debugEntity = new Entity("debug");
            debugEntity.AddComponent(new PositionComponent());
             var rc2 = new RenderComponent(VertexHelper.SetColour(Circle.Generate(0.075f, 32), new Vector3(1, 0, 0)));
            rc2.Shaders.Add(Engine.Render.Core.Shaders.Enums.ShaderType.Standard);
            rc2.VertexArrayObject.Outlined = true;
            debugEntity.AddComponent(rc2);

            entityManager.AddEntity(debugEntity);
        }

        public void SetFootPosition(Vector2 target)
        {
            var originalTarget = target;

            for(int i = 0; i < _segmentCount - 1; i++)
            {
                var r = Reach(_entities[i].GetComponent<PositionComponent>().Position, _entities[i + 1].GetComponent<PositionComponent>().Position, target);
                _entities[i].GetComponent<PositionComponent>().Position = r.Item1;
                target = r.Item2;
            }

            _entities[_segmentCount - 1].GetComponent<PositionComponent>().Position = target;


            target = _parent.GetComponent<PositionComponent>().Position + Rotate(_baseOffset, _parent.GetComponent<PositionComponent>().Angle);

            for (int i = _segmentCount - 1; i > 0; i--)
            {
                var r = Reach(_entities[i].GetComponent<PositionComponent>().Position, _entities[i - 1].GetComponent<PositionComponent>().Position, target);
                _entities[i].GetComponent<PositionComponent>().Position = r.Item1;
                target = r.Item2;
            }
            _entities[0].GetComponent<PositionComponent>().Position = target;

            for (int i = _segmentCount - 1; i >= 0; i--)
            {
                var pos1 = _entities[i].GetComponent<PositionComponent>().Position;
                var pos2 = i == 0 ? originalTarget : _entities[i - 1].GetComponent<PositionComponent>().Position;
                var angle = GetAngle(pos1, pos2);
                _entities[i].GetComponent<PositionComponent>().Angle = angle;// * (pos1.Y > pos2.Y ? 1 : -1);
            }

            //_entities[0].GetComponent<PositionComponent>().Angle = GetAngle(_entities[0].GetComponent<PositionComponent>().Position, target);

            /*for (int i = 0; i < _segmentCount; i++)
            {
                if(i == _segmentCount - 1)
                {
                    target = _parent.GetComponent<PositionComponent>().Position + _offset;
                }
                else
                {
                    target = _entities[i + 1].GetComponent<PositionComponent>().Position;
                }

                var angle = GetAngle(target, _entities[i].GetComponent<PositionComponent>().Position);
                _entities[i].GetComponent<PositionComponent>().Angle = -angle;
            }*/
        }

        public void StepDown() => _isDown = true;

        public void Update(float deltaTime, float bodySpeed)
        {
            var actualTarget = Rotate(_targetOffset, _parent.GetComponent<PositionComponent>().Angle) + _parent.GetComponent<PositionComponent>().Position;
            float distance = Vector2.Distance(actualTarget, _footPosition);

            var actualBase = Rotate(_baseOffset, _parent.GetComponent<PositionComponent>().Angle) + _parent.GetComponent<PositionComponent>().Position;
            float legLength = Vector2.Distance(_footPosition, actualBase);

            if (_isDown)
            {

                if(distance > _length || (legLength > _calculatedMaxLength && distance > _length * 0.1f))
                {
                    _isDown = false;

                    if(!Counterpart.IsFootDown)
                        Counterpart?.StepDown();
                }


                if (_isDown && Counterpart.IsFootDown)
                {
                    _isDown = false;
                }
            }
            else
            {
                var aim = (actualTarget - _footPosition);
                float lengthAim = aim.Length;

                if (lengthAim < _length * 0.1f)
                {
                    _isDown = true;
                }
                else
                {
                    _footPosition += (aim / _calculatedMaxLength) * deltaTime * (3 + bodySpeed);
                }
            }
            SetFootPosition(_footPosition);

            debugEntity.GetComponent<PositionComponent>().Position = _footPosition;
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
