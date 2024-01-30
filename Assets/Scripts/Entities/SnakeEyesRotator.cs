using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakeEyesRotator : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float _eyesMoveOffset;
        [SerializeField] private float _lookDistance = 5;
        [SerializeField] private Vector2 _defaultPosition;
        [Header("Components")]
        [SerializeField] private Food _food;
        [SerializeField] private Transform _eyesRoot;

        private Vector3 _centerPosition;

        private void Awake()
        {
            _centerPosition = _eyesRoot.transform.localPosition;
        }

        private void Update()
        {
            if (IsNearFood())
            {
                var foodPosition = _food.transform.position;
                var lookDirection = _eyesRoot.InverseTransformPoint(foodPosition) - _eyesRoot.localPosition;

                _eyesRoot.transform.localPosition =
                    _centerPosition + Vector3.ClampMagnitude(lookDirection, _eyesMoveOffset);
            }
            else if (!DefaultView())
            {
                _eyesRoot.transform.localPosition =  _defaultPosition;
            }
        }

        private bool DefaultView()
        {
            return _eyesRoot.transform.localPosition == _defaultPosition.AsVector3();
        }

        private bool IsNearFood()
        {
            return Vector3.Distance(transform.position, _food.transform.position) <= _lookDistance;
        }
    }
}