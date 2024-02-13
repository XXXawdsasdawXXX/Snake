using UnityEngine;

namespace Services.Audio
{
    public class AudioMuteToggle : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private JSService _jsService;

        private void Awake()
        {
            SubscribeToEvents(true);
        }

        private void OnDestroy()
        {
            SubscribeToEvents(false);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _jsService.SetMuteAudioEvent += JsServiceOnSetMuteAudioEvent;
            }
            else
            {
                _jsService.SetMuteAudioEvent -= JsServiceOnSetMuteAudioEvent;
            }
        }

        private void JsServiceOnSetMuteAudioEvent(bool isMute)
        {
            _audioSource.mute = isMute;
        }
    }
}