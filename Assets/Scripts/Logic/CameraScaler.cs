using UnityEngine;
using Utils;

namespace Logic
{
    public class CameraScaler : MonoBehaviour
    {
        [SerializeField] private Vector2 _referenceResolution = new(1080, 2640);
        [SerializeField] private float _referenceSize = 25.5f;
        [SerializeField] private Camera _camera;
    
        private void Awake()
        {
            if (Screen.width > Screen.height)
            {
                Debugging.Instance?.Log($"Camera can't set other size", Debugging.Type.Camera);
                return;
            }

            float referenceAspect = _referenceResolution.x / _referenceResolution.y;
            float currentAspect = (float)Screen.width / (float)Screen.height;

           _camera.orthographicSize = _referenceSize * referenceAspect / currentAspect;
        }
    }
}