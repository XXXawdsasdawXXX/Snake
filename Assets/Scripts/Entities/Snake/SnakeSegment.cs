using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeSegmentCollision _segmentCollision;
        public SnakeSegmentCollision Collision => _segmentCollision;
        
        public bool IsMoving /*{ get; private set; }*/;
        public Vector3 LastTarget /*{ get; private set; }*/;
        public Vector3 Target /*{ get; private set; }*/;

        public float Distance => Vector3.Distance(transform.position, Target);
        private Vector2Int _moveDirection { get; set; }

        private Tween _moveTween;

        [SerializeField] private /*readonly */List<Vector3> _way = new();

        
        public void StartMove(Vector3 target, float duration, Action onEndMove = null)
        {
            AddPoint(target);

            LastTarget = Target;
            Target = target;
            
            if (LastTarget != Vector3.zero && Target != Vector3.zero && !IsMoving && Vector3.Distance(transform.position, Target) <0.3f)
            {
                IsMoving = true;
            }

            var dir = Target - LastTarget;
            var x = Convert.ToInt32(MathF.Ceiling(dir.x * Constants.SEGMENT_COUNT));
            var y = Convert.ToInt32(MathF.Ceiling(dir.y * Constants.SEGMENT_COUNT));

            _moveDirection = new Vector2Int(x, y);

            _moveTween = transform.DOMove(new Vector3(Target.x, Target.y, 0), duration).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy).OnComplete(() => onEndMove?.Invoke());
        }

        public void MoveBack()
        {
            StopMove();
            Sequence sequence = DOTween.Sequence();
            for (int i = _way.Count -1; i >= 0 ; i--)
            {
                sequence.Append(transform.DOMove(_way[i], 0.05f).SetEase(Ease.Linear)
                    .SetLink(gameObject, LinkBehaviour.KillOnDestroy));
            }
        }

        public void SetTarget(Vector3 target)
        {
            Target = target;
            IsMoving = true;
        }

        public void StopMove()
        {
            /*Target = Vector3.zero;
            LastTarget = Vector3.zero;*/
            _moveTween?.Kill();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Target, 0.05f);
            Gizmos.DrawSphere(LastTarget, 0.03f);
        }

        private void AddPoint(Vector3 target)
        {
            if (_way.Count == 0 && target == Vector3.zero)
            {
                return;
            }
            
            if (_way.Count >= Constants.SEGMENT_COUNT)
            {
                _way.RemoveAt(0);
            }

            _way.Add(target);
        }
    }
}