using System.Linq;
using Services.Audio;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs")]
    public class AudioConfig : ScriptableObject
    {
        public AudioEventData[] AudioEvents;

        public bool TryGetAudioClip(AudioEventType audioEventType, out AudioClip clip)
        {
            var data = AudioEvents.FirstOrDefault(e => e.Type == audioEventType);
            if (data == null)
            {
                clip = null;
                return false;
            }

            clip = data.Clip;
            return true;
        }
    }
}