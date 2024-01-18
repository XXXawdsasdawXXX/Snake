using UnityEngine;
using Utils;

namespace Services
{
    public class MobileDirectionListener : IInputDirectionListener
    {
        private const float SWIPE_DISTANCE = 60;
        
        private Vector2Int _direction = Vector2Int.right;
        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

        private bool _isSwiping;
        
        public Vector2Int GetDirection()
        {
            return _direction;
        }

        public void SetDirection()
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _isSwiping = true;
                _tapPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase is TouchPhase.Canceled or TouchPhase.Ended)
            {
                ResetSwipe();
            }

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            if (Input.touchCount > 0)
            {
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
            }

            if (_swipeDelta.magnitude > SWIPE_DISTANCE)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                {
                    _direction = _swipeDelta.x > 0 ? Vector2Int.right : Vector2Int.left;
                    Debugging.Instance.Log($"Set mobile horizontal direction {_direction}", Debugging.Type.Input);

                }
                else
                {
                    _direction = _swipeDelta.y > 0 ? Vector2Int.up : Vector2Int.down;
                    Debugging.Instance.Log($"Set mobile vertical direction {_direction}", Debugging.Type.Input);
                }

                ResetSwipe();
            }
        }

        private void ResetSwipe()
        {
            _isSwiping = false;
            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
        }
    }
}