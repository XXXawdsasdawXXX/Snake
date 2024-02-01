using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakePupilsRotator : SnakeEmotionObject
    {
        [Header("Params")] 
        [SerializeField] private float _eyesMoveOffset;
        [SerializeField] private Vector2 _defaultPosition;

        [Header("Components")]
        [SerializeField] private Food _food;
        [SerializeField] private Transform _pupilsRoot;
        
        private Vector3 _centerPosition;
      
        

        private void Awake()
        {
            _centerPosition = _pupilsRoot.transform.localPosition;
        }

        public override bool IsReady()
        {
            return IsNear(_food.transform.position);
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
            _pupilsRoot.transform.localPosition = _defaultPosition;
        }
    }
}