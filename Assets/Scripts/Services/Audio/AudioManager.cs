﻿using System;
using Configs;
using UnityEngine;
using Utils;

namespace Services.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioConfig _audioConfig;

        private bool _isMuted;
        
        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayAudioEvent(AudioEventType eventType)
        {
            if (_isMuted)
            {
                return;
            }
            
            if (_audioConfig.TryGetAudioClip(eventType, out var clip))
            {
                _audioSource.PlayOneShot(clip);
                Debugging.Instance.Log($"Play audio {eventType}",Debugging.Type.Audio);
            }
            else
            {
                Debugging.Instance.Log($"Can`t find {eventType} audio event",Debugging.Type.Audio);
            }
        }
        
        public void PlayAudioEvent(Vector2Int direction)
        {
            AudioEventType type = AudioEventType.None;

            if (direction == Vector2Int.right)
            {
                type = AudioEventType.PressRight;
            }
            else if(direction == Vector2Int.left)
            {
                type = AudioEventType.PressLeft;
            }
            else if(direction == Vector2Int.up)
            {
                type = AudioEventType.PressUp;
            }
            else if(direction == Vector2Int.down)
            {
                type = AudioEventType.PressDown;
            }
            
            if (_audioConfig.TryGetAudioClip(type, out var clip))
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }

    [Serializable]
    public class AudioEventData
    {
        public AudioEventType Type;
        public AudioClip Clip;
    }

    public enum AudioEventType
    {
        None,
        PressUp,
        PressDown,
        PressLeft,
        PressRight,
        ClickButton,
        Death,
        Grow,
        ScorePointUp,
        Win,
        Lose
    }
}