using Logic.Health;
using UnityEngine;

namespace UI.Components
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HealthIcon[] _hearts;

        public void SetValue(int health)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                if (i < health)
                    _hearts[i].Activate();
                else
                    _hearts[i].Deactivate();
            }
        }
    }
}