using System;
using UnityEngine;

namespace Services
{
    public class InputService : MonoBehaviour
    {
        private IInputDirectionListener _mouseDirectionListener;
        private IInputDirectionListener _keyDirectionListener;
        
        private bool _isMouse;

        private Vector2Int _direction;
        public event Action<Vector2Int> SetNewDirectionEvent; 
        private void Awake()
        {
            _isMouse = Screen.width < Screen.height;
            _keyDirectionListener =  new KeyDirectionListener();
            _mouseDirectionListener = new MouseDirectionListener();
        }
        
        private void Update()
        {
            if (Input.anyKeyDown && _isMouse)
            {
                _isMouse = false;
                _keyDirectionListener.SetDirection(_mouseDirectionListener.GetDirection());
            }
            if (Input.GetMouseButtonDown(0) && !_isMouse)
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

            var dir = GetDirection();
            if (_direction != dir)
            {
                _direction = dir;
                SetNewDirectionEvent?.Invoke(_direction);
            }
        }
        
        public Vector2Int GetDirection()
        {
            return _isMouse ? _mouseDirectionListener.GetDirection() : _keyDirectionListener.GetDirection();
        }
    }

}