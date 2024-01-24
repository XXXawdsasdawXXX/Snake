using UnityEngine;
using Utils;

namespace Logic
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField] private Vector2 _referenceResolution = new(1080, 2640);
        [SerializeField] private Vector2 _referencePosition = new(0,2.4f);
        [SerializeField] private float _referenceSize = 25.5f;
        [Space]
        [SerializeField] private float _minSize = 12.9f;
        [SerializeField] private float _maxSize = 25f;
    
        private void Awake()
        {
            if (Screen.width > Screen.height)
            {
                Debugging.Instance?.Log($"Camera can't set other size", Debugging.Type.Camera);
                return;
            }

            SetResolution();

            SetPosition();

        }
        
        private void SetResolution()
        {
            float referenceAspect = _referenceResolution.x / _referenceResolution.y;
            float currentAspect = (float)Screen.width / (float)Screen.height;

            _camera.orthographicSize = Mathf.Clamp(_referenceSize * referenceAspect / currentAspect, _minSize, _maxSize);
        }

        private void SetPosition()
        {
            Vector3 cameraPosition = _referencePosition;
            var sizeDifferent = _referenceSize - _camera.orthographicSize;
            cameraPosition.y -= sizeDifferent;
            cameraPosition.z = -10;
            _camera.transform.position = cameraPosition;
        }
    }
}