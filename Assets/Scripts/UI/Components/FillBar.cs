using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        
        public void UpdateValue(int current, int max)
        {
            _fill.fillAmount = (float)current / (float)max;
        }   
    }
}