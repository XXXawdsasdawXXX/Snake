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
        public bool IsMoving { get; private set; }
        public Vector3 LastTarget { get; private set; }
        public Vector3 Target { get; private set; }
        public Vector3 LastPoint => _way[0];


        private Vector2Int _moveDirection { get; set; }
        private bool _isSetTargets => LastTarget != Vector3.zero && Target != Vector3.zero;

        private Tween _moveTween;

        private List<Vector3> _way = new List<Vector3>();

        public void StartMove(Vector3 target, float duration, Action onEndMove = null)
        {
            AddPoint(target);

            LastTarget = Target;
            Target = target;

            if (_isSetTargets && !IsMoving)
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

            //sequence.AppendCallback(StopMove);
        }

        public void SetTarget(Vector3 target)
        {
            Target = target;
            IsMoving = true;
        }

        public void StopMove()
        {
            Target = Vector3.zero;
            LastTarget = Vector3.zero;
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
            if (_way.Count > Constants.SEGMENT_COUNT)
            {
                _way.RemoveAt(0);
            }

            _way.Add(target);
        }
    }
}