using UnityEngine;

namespace Services
{
    public interface IInputDirectionListener
    {
        void SetDirection(Vector2Int direction);
        Vector2Int GetDirection();
        void SetDirection();
    }
}