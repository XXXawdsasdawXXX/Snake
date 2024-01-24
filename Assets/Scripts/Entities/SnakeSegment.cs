using System;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakeSegment : MonoBehaviour
    {
        [SerializeField] private SnakeSegmentCollision _segmentCollision;
        public SnakeSegmentCollision Collision => _segmentCollision;
        public bool IsSetTarget => LastTarget != Vector3.zero && Target != Vector3.zero;
        public Vector3 LastTarget { get; private set; }
        public Vector3 Target { get; private set; }
        public Vector2Int MoveDirection { get;  private set; }
      
        private Tween _moveTween;

        
        public void StartMove(Vector3 target, float duration)
        {
            LastTarget = Target;
            Target = target;

            var dir = Target - LastTarget;
            var x = Convert.ToInt32(MathF.Ceiling(dir.x * Constants.SEGMENT_COUNT));
            var y = Convert.ToInt32(MathF.Ceiling(dir.y * Constants.SEGMENT_COUNT));

            MoveDirection = new Vector2Int(x, y);

            _moveTween = transform.DOMove(new Vector3(Target.x, Target.y, 0), duration).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void SetTarget(Vector3 target)
        {
            Target = target;
        }

        public void StopMove()
        {
            Target = Vector3.zero;
            LastTarget = Vector3.zero;
            _moveTween?.Kill();
        }
    }
}