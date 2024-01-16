using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class SnakeSegment : MonoBehaviour
    {
        public Vector3 LastTarget;
        public Vector3 Target;
        private Tween _moveTween;

        public Vector2Int MoveDirection;
        
        public bool IsSetTarget => LastTarget != Vector3.zero && Target != Vector3.zero;

        public void StartMove(Vector3 target, float duration)
        {
            LastTarget = Target;
            Target = target;
          
            var dir = Target - LastTarget;
            var x = Convert.ToInt32(MathF.Ceiling(dir.x * Constants.SEGMENT_COUNT));
            var y = Convert.ToInt32(MathF.Ceiling(dir.y * Constants.SEGMENT_COUNT));
         
            MoveDirection = new Vector2Int(x,y);

            _moveTween = transform.DOMove(new Vector3(Target.x, Target.y, 0), duration).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
        
        public void StopMove()
        {
            Target = Vector3.zero;
            LastTarget = Vector3.zero;
            _moveTween?.Kill();
        }
    }
}