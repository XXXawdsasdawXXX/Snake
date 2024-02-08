using System;
using Entities;
using Services;
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
                _gameController.EndGameEvent += EndGameEvent;
            }
            else
            {
                _snake.ObstacleCollisionEvent -= OnObstacleCollision;
                _gameController.InitSessionEvent -= OnInitSession;
                _gameController.EndGameEvent -= EndGameEvent;
            }
        }

        private void EndGameEvent(bool obj)
        {
            _health.ResetHealth();
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