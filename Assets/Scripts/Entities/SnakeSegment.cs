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


        public void StartMove(Vector2Int target, float duration)
        {
            LastTarget = Target;
            Target = target;


            _moveTween = transform.DOMove(new Vector3(Target.x, Target.y, 0), duration).SetEase(Ease.Linear)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }


        public void StopMove()
        {
            Target = Vector2Int.zero;
            LastTarget = Vector2Int.zero;
            _moveTween?.Kill();
        }
    }
}