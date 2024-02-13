using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class InitScreen : Screen
    {
        [SerializeField] private EditableText _sessionPriceText;
        [SerializeField] private EditableText _sessionPriceShadowText;
        [SerializeField] private Button _startButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(Hide);
        }

        public void SetSessionPrice(string price)
        {
            _sessionPriceText.SetText(price);
            _sessionPriceShadowText.SetText(price);
        }
    }
}