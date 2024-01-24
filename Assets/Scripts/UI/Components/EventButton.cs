using Services;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Components
{
    public class EventButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private EventButtonType _buttonType;

        private void Awake()
        {
            _button.onClick.AddListener(() => { UIEvents.ClickButtonEvent?.Invoke(_buttonType); });
        }
    }

    public enum EventButtonType
    {
        None,
        Play,
        Close,
        Pause
    }
}