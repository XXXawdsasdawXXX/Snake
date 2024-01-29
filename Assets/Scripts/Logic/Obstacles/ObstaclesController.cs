using System.Collections.Generic;
using System.Linq;
using Entities;
using Services;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Logic
{
    public class ObstaclesController : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField, Min(0)] private int _chanceToActivate = 50;
        [SerializeField, Min(0)] private int _runtimeChanceToActivate = 60;
        [SerializeField] private bool _isActiveAllOnStart = true;
        [Header("Cooldown")] [SerializeField] private float _minCooldown;
        [SerializeField] private float _maxCooldown;
        [Space] [SerializeField] private List<Obstacle> _allObstacles;
        private readonly List<Obstacle> _activeObstacles = new();

        private bool _isActive;
        private float _currentCooldown;
        private float _cooldown;

        private void Awake()
        {
            SubscribeToEvents(true);

            ResetObstacles();
        }


        private void Update()
        {
            if (_isActiveAllOnStart)
            {
                return;
            }

            if (_isActive && _gameController.GameState == GameState.Play)
            {
                _currentCooldown += Time.deltaTime;

                if (_currentCooldown >= _cooldown)
                {
                    ResetCooldown();

                    RuntimeActiveNextObstacle();
                }
            }
        }

        private void OnDestroy()
        {
            SubscribeToEvents(false);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _gameController.ResetGameEvent += ResetObstacles;
                _gameController.StartGameEvent += ActiveController;
                _gameController.EndGameEvent += DisableController;
            }
            else
            {
                _gameController.ResetGameEvent -= ResetObstacles;
                _gameController.StartGameEvent -= ActiveController;
                _gameController.EndGameEvent -= DisableController;
            }
        }

        private void ActiveNextObstacle()
        {
            var randomChance = Random.Range(0, 100);
            if (randomChance < 100 - _chanceToActivate)
            {
                return;
            }

            Extensions.ShuffleList(_allObstacles);
            var obstacle = _allObstacles[0];
            if (obstacle != null)
            {
                _activeObstacles.Add(obstacle);
                _allObstacles.Remove(obstacle);
                obstacle.Activate();
            }
            
        }

        private void RuntimeActiveNextObstacle()
        {
            var randomChance = Random.Range(0, 100);
            if (randomChance < 100 - _runtimeChanceToActivate)
            {
                return;
            }

            Extensions.ShuffleList(_allObstacles);
            var obstacle = _allObstacles.FirstOrDefault(o => o.IsCanActivate());
            if (obstacle != null)
            {
                _activeObstacles.Add(obstacle);
                _allObstacles.Remove(obstacle);
                obstacle.Activate();
            }

            if (_allObstacles.Count == 0)
            {
                _isActive = false;
            }
        }

        private void ActiveController()
        {
            _isActive = true;
            ResetCooldown();
        }

        private void DisableController(bool isWon)
        {
            _isActive = false;
        }

        private void ResetCooldown()
        {
            _cooldown = Random.Range(_minCooldown, _maxCooldown);
            _currentCooldown = 0;
        }

        private void ResetObstacles()
        {
            _allObstacles.AddRange(_activeObstacles);
            _activeObstacles.Clear();

            foreach (var activeObstacle in _allObstacles)
            {
                activeObstacle.Deactivate();
            }

            if (_isActiveAllOnStart)
            {
                for (int i = 0; i < _allObstacles.Count; i++)
                {
                    ActiveNextObstacle();
                }
            }
        }

        public bool Occupies(int x, int y)
        {
            foreach (var obstacle in _activeObstacles)
            {
                if (obstacle.Occupies(x, y))
                {
                    return true;
                }
            }

            return false;
        }
    }
}