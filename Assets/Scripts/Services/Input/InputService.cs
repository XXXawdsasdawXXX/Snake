using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour
    {
        private bool _isMobileDevice;

        private IInputDirectionListener _directionListener;
        private void Awake()
        {
            _isMobileDevice = Screen.width > Screen.height;
            _directionListener = _isMobileDevice ? new MobileDirectionListener() : new StandaloneDirectionListener();
        }
        private void Update()
        {
            _directionListener.SetDirection();
        }
        
        public Vector2Int GetDirection()
        {
            return _directionListener.GetDirection();
        }
    }

}