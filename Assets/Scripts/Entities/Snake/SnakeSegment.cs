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
        public bool IsMoving  { get; private set; }
        public Vector3 LastTarget { get; private set; }
        public Vector3 Target { get; private set; }
        
        private Vector2Int _moveDirection { get; set; }
        private bool _isSetTargets => LastTarget != Vector3.zero && Target != Vector3.zero;
      
        
        private Tween _moveTween;
        
        public void StartMove(Vector3 target, float duration)
        {
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
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void SetTarget(Vector3 target)
        {
            Target = target;
            LastTarget = target;
            IsMoving = true;
        }

        public void StopMove()
        {
            Target = Vector3.zero;
            LastTarget = Vector3.zero;
            _moveTween?.Kill();
        }
    }
}