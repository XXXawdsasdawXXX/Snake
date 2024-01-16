using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private Vector2Int _forward = new Vector2Int(1, 0);

        private void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");

            if (x != 0 && _forward * -1 != new Vector2Int(Convert.ToInt32(x), 0))
            {
                _forward = new Vector2Int(Convert.ToInt32(x), 0);
            }

            if (y != 0 && _forward * -1 != new Vector2Int(0, Convert.ToInt32(y)))
            {
                _forward = new Vector2Int(0, Convert.ToInt32(y));
            }
        }

        public Vector2Int GetDirection()
        {
            return _forward;
        }
    }
}