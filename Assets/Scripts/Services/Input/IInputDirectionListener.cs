using UnityEngine;

namespace Services
{
    public interface IInputDirectionListener
    {
        Vector2Int GetDirection();
        void SetDirection();
    }
}