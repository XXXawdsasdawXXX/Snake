using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Components
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        
        public void UpdateValue(int current, int max)
        {
            Debugging.Instance.Log($"Fill amount {current} / {max} = {current/max}");
            _fill.fillAmount = (float)current / (float)max;
        }
      
    }
}