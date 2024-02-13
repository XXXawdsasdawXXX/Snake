using UnityEngine;

namespace UI.Components
{
    public class CoinTable : MonoBehaviour
    {
        [SerializeField] private EditableText _valueText;
        [SerializeField] private EditableText _valueShadowText;

        public void SetValue(string value)
        {
            _valueText.SetText(value);
            _valueShadowText.SetText(value);
        }
    }
}