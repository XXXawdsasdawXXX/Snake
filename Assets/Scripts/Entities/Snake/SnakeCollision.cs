﻿using Configs;
using Services.Audio;
using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakeCollision : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private SnakeConfig _snakeConfig;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_snake.IsActive)
            {
                return;
            }
            if (other.gameObject.CompareTag(Constants.Tag.Food.ToString()))
            {
                _snake.Grow();
                AudioManager.Instance.PlayAudioEvent(AudioEventType.Grow);
            }
            else if (other.gameObject.CompareTag(Constants.Tag.Obstacle.ToString()))
            {
                _snake.InvokeCollisionEvent();
        
                AudioManager.Instance.PlayAudioEvent(AudioEventType.Death);
            }
            else if (other.gameObject.CompareTag(Constants.Tag.Wall.ToString()))
            {
              
                    _snake.InvokeCollisionEvent();
                    _snake.StopMove();
                    AudioManager.Instance.PlayAudioEvent(AudioEventType.Death);
              
            }
        }
    }
}