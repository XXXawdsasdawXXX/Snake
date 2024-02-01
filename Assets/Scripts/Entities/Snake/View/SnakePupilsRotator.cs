using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakePupilsRotator : MonoBehaviour
    {
        [Header("Params")] 
        [SerializeField] private float _eyesMoveOffset;
        [SerializeField] private Vector2 _defaultPosition;
        [Header("Components")] 
        [SerializeField] private Transform _pupilsRoot;
        
        private Vector3 _centerPosition;

        private void Awake()
        {
            _centerPosition = _pupilsRoot.transform.localPosition;
        }
        
        public void LookTo(Vector3 worldPosition)
        {
            var lookDirection = _pupilsRoot.InverseTransformPoint(worldPosition) - _pupilsRoot.localPosition;

            _pupilsRoot.transform.localPosition =
                _centerPosition + Vector3.ClampMagnitude(lookDirection, _eyesMoveOffset);
        }

        public void Reset()
        {
            _pupilsRoot.transform.localPosition = _defaultPosition;
        }
    }
}