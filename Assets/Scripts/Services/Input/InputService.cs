using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour
    {
        private IInputDirectionListener _mouseDirectionListener;
        private IInputDirectionListener _keyDirectionListener;
        
        private bool _isMouse;
        
        private void Awake()
        {
            _isMouse = Screen.width < Screen.height;
            _keyDirectionListener =  new KeyDirectionListener();
            _mouseDirectionListener = new MouseDirectionListener();
        }
        
        private void Update()
        {
            if (Input.anyKeyDown)
            {
                _isMouse = false;
                _keyDirectionListener.SetDirection(_mouseDirectionListener.GetDirection());
            }
            if (Input.GetMouseButtonDown(0))
            {
                _isMouse = true;
                _mouseDirectionListener.SetDirection(_keyDirectionListener.GetDirection());
            }

            if (_isMouse)
            {
                _mouseDirectionListener.SetDirection();
            }
            else
            {
                _keyDirectionListener.SetDirection();
            }
        }
        
        public Vector2Int GetDirection()
        {
            return _mouseDirectionListener.GetDirection();
        }
    }

}