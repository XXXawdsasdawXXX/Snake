using UnityEngine;

namespace UI.Components
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hearts;

        public void SetValue(int health)
        {
            for (int i = 0; i < _hearts.Length; i++)
            {
                _hearts[i].SetActive(i < health);
            }
        }
    }
}