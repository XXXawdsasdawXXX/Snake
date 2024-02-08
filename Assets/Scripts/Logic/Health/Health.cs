using System;
using UnityEngine;
using Utils;

namespace Logic.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _max = 3;
        private int _current;
        
        public event Action<int> ChangeValueEvent;
        
        public void ResetHealth()
        {
            _current = _max;
            Debugging.Instance.Log($"Reset", Debugging.Type.Health);
            ChangeValueEvent?.Invoke(_current);
        }

        public void RemoveHealth()
        {
            if (_current <= 0)
            {
                return;
            }
            
            _current--;
            Debugging.Instance.Log($"Remove ", Debugging.Type.Health);
            ChangeValueEvent?.Invoke(_current);
        }
    }
}