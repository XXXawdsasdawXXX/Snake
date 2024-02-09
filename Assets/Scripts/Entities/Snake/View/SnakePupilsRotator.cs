using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class SnakePupilsRotator : SnakeEmotionObject
    {
        [Header("Params")] 
        [SerializeField] private float _eyesMoveOffset;
        [SerializeField] private Vector2 _defaultPosition;

        [Header("Components")] 
        [SerializeField] private Snake _snake;
        [SerializeField] private Food _food;
        [SerializeField] private Transform _pupilsRoot;

        private Tween _tween;
        private Vector3 _centerPosition;
        
        private void Awake()
        {
            _centerPosition = _pupilsRoot.transform.localPosition;
            _snake.ResetEvent += Reset;
        }
        
        private void OnDestroy()
        {
            _snake.ResetEvent -= Reset;
        }

        public override bool IsReady()
        {
            return  _food.IsActive && IsNear(_food.transform.position);
        }

        public override void StartReaction()
        {
            IsActive = true;
            LookTo(_food.transform.position);
        }

        public override void StopReaction()
        {
            Reset();
            IsActive = false;
        }
        
        private void LookTo(Vector3 worldPosition)
        {
            var lookDirection = _pupilsRoot.InverseTransformPoint(worldPosition) - _pupilsRoot.localPosition;

            _pupilsRoot.transform.localPosition =
                _centerPosition + Vector3.ClampMagnitude(lookDirection, _eyesMoveOffset);
        }

        private void Reset()
        {
            _tween.Complete();
            _tween =  _pupilsRoot.transform.DOLocalMove(_defaultPosition, 0.3f).SetLink(gameObject, LinkBehaviour.KillOnDestroy) ;
        }
    }
}