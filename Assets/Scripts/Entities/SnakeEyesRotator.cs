using UnityEngine;

namespace Entities
{
    public class SnakeEyesRotator : MonoBehaviour
    {
        [SerializeField] private Food _food;
        [SerializeField] private Transform _eyesRoot;
        [SerializeField] private float _eyesMoveOffset;

        private Vector3 _defaultLocalPosition;

        private void Awake()
        {
            _defaultLocalPosition = _eyesRoot.transform.localPosition;
        }

        private void Update()
        {
            var foodPosition = _food.transform.position;
            var lookDirection = _eyesRoot.InverseTransformPoint(foodPosition) - _eyesRoot.localPosition;

            _eyesRoot.transform.localPosition = _defaultLocalPosition + Vector3.ClampMagnitude(lookDirection, _eyesMoveOffset);
        }
    }
}