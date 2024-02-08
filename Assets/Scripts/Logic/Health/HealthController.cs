using System;
using Entities;
using Services;
using UI.Components;
using UnityEngine;

namespace Logic.Health
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private GameController _gameController;
        [SerializeField] private Snake _snake;

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
                _snake.ObstacleCollisionEvent += OnObstacleCollision;
                _gameController.InitSessionEvent += OnInitSession;
                UIEvents.ClickButtonEvent += ClickButtonEvent;
            }
            else
            {
                _snake.ObstacleCollisionEvent -= OnObstacleCollision;
                _gameController.InitSessionEvent -= OnInitSession;
        
            }
        }

        private void ClickButtonEvent(EventButtonType obj)
        {
            switch (obj)
            {
                case EventButtonType.Play:
                    _health.ResetHealth();
                    break;
            }
        }

        private void OnInitSession(SessionData obj)
        {
            _health.ResetHealth();
        }

        private void OnObstacleCollision()
        {
            _health.RemoveHealth();
        }
    }
}