using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputService: MonoBehaviour
    {
       [SerializeField] private Vector2 _forward = new Vector2(1, 0);
        
        private void Update()
        {
            var x = Input.GetAxisRaw("Horizontal"); 
            var y = Input.GetAxisRaw("Vertical");
            
            if (x != 0)
            {
                _forward = new Vector2(x, 0);
            }

            if (y != 0)
            {
                _forward = new Vector2(0, y);
            }
        }

        public Vector2 GetForward()
        {
            return _forward;
        }
    }
}