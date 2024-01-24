using UnityEngine;

namespace UI.Components
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _fill;

        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;

        private bool _full;
        
        public void UpdateValue(int current, int max)
        {
            if (_full)
            {
                return;
            }
            
            _fill.anchoredPosition =
                new Vector2(Mathf.Lerp(_minX, _maxX, (float)current / max), _fill.anchoredPosition.y);
        }
      
    }
}