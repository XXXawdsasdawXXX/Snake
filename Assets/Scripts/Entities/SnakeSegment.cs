using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class SnakeSegment : MonoBehaviour
    {
        public Vector2Int LastTarget;
        public Vector2Int Target;
        private Tween _moveTween;

        public Vector2Int Direction { get; set; }
        public bool IsSetTarget { get; private set; }

        public void StartMove(Vector2Int target, float duration)
        {
            LastTarget = Target;
            Target = target;

            IsSetTarget = true;

            _moveTween = transform.DOMove(new Vector3(Target.x, Target.y, 0), duration).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        public void StartMove(Vector3 target, float duration)
        {
            LastTarget = Target;
            Target = target.AsVector2Int();

            IsSetTarget = true;

            _moveTween = transform.DOMove(new Vector3(Target.x, Target.y, 0), duration).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }


        public void StopMove()
        {
            IsSetTarget = false;
            Target = Vector2Int.zero;
            LastTarget = Vector2Int.zero;
            _moveTween?.Kill();
        }
    }
}