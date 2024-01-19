using System;
using UnityEngine;

namespace Services
{
    public interface IInputDirectionListener
    {
        event Action<Vector2Int> SetNewDirectionEvent; 
        void SetDirection(Vector2Int direction);
        Vector2Int GetDirection();
        void SetDirection();
    }
}