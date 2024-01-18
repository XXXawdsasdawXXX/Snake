using UnityEngine;
using Utils;

namespace Services
{
    public class MouseDirectionListener : IInputDirectionListener
    {
        private const float SWIPE_DISTANCE = 50;

        private Vector2Int _direction = Constants.DEFAULT_DIRECTION;
        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

        private bool _isSwiping;

        public void SetDirection(Vector2Int direction)
        {
            if (direction != Vector2Int.zero)
            {
                _direction = direction;
            }
        }
        public Vector2Int GetDirection()
        {
            return _direction;
        }

        public void SetDirection()
        {
            if (Input.GetMouseButton(0))
            {
                if (!_isSwiping)
                {
                    _isSwiping = true;
                    _tapPosition = Input.mousePosition;
                    Debugging.Instance.Log($"Is swipe true", Debugging.Type.Input);
                }

                CheckSwipe();
            }

            if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }

        private void CheckSwipe()
        {
            if (!_isSwiping)
            {
                return;
            }

            _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;

            Debugging.Instance.Log($"Mobile swipe {_swipeDelta}\nmagnitude {_swipeDelta.magnitude} ",
                Debugging.Type.Input);

            if (_swipeDelta.magnitude > SWIPE_DISTANCE)
            {
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                {
                    _direction = _swipeDelta.x > 0 ? Vector2Int.right : Vector2Int.left;
                    Debugging.Instance.Log($"Set mobile horizontal\n direction {_direction}", Debugging.Type.Input);
                }
                else
                {
                    _direction = _swipeDelta.y > 0 ? Vector2Int.up : Vector2Int.down;
                    Debugging.Instance.Log($"Set mobile vertical\n direction {_direction}", Debugging.Type.Input);
                }

                ResetSwipe();
                _tapPosition = Input.mousePosition;
            }
        }

        private void ResetSwipe()
        {
            Debugging.Instance.Log($"Mobile reset swipe {_direction}", Debugging.Type.Input);
            _isSwiping = false;
            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
        }
    }
}