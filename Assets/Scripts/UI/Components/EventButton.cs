using Services;
using Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class EventButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private EventButtonType _buttonType;

        private void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                UIEvents.ClickButtonEvent?.Invoke(_buttonType);
                AudioManager.Instance.PlayAudioEvent(AudioEventType.ClickButton);
            });
        }
    }

    public enum EventButtonType
    {
        None,
        Play,
        Close,
        Pause,
    }
}