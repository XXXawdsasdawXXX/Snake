using System;
using UnityEngine;
using Utils;

namespace Services
{
    public class KeyDirectionListener: IInputDirectionListener
    {
        private Vector2Int _direction = Vector2Int.right;
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
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");

            if (x != 0 && _direction * -1 != new Vector2Int(Convert.ToInt32(x), 0))
            {
                _direction = new Vector2Int(Convert.ToInt32(x), 0);
                Debugging.Instance.Log($"Set pc horizontal direction {_direction}", Debugging.Type.Input);
            }

            if (y != 0 && _direction * -1 != new Vector2Int(0, Convert.ToInt32(y)))
            {
                _direction = new Vector2Int(0, Convert.ToInt32(y));
                Debugging.Instance.Log($"Set pc vertical direction {_direction}", Debugging.Type.Input);
            }
        }
    }
}