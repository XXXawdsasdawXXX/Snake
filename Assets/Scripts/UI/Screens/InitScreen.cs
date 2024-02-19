using System;
using Services;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Screens
{
    public class InitScreen : Screen
    {
        [SerializeField] private EditableText _sessionPriceText;
        [SerializeField] private EditableText _sessionPriceShadowText;
        [SerializeField] private InputService _inputService;
        [SerializeField] private Button _startButton;

        private void Awake()
        {
            _inputService.EnterKeyDownEvent += OnEnterKeyDown;
            _startButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            _inputService.EnterKeyDownEvent -= OnEnterKeyDown;
        }

        public void SetSessionPriceInfo(string price)
        {
            _sessionPriceText.SetText(price);
            _sessionPriceShadowText.SetText(price);
        }

        private void OnEnterKeyDown()
        {
            if (IsActive())
            {
                Hide();
            }
        }
    }
}