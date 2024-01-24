using Entities;
using Services;
using UnityEngine;

namespace Logic
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private Snake _snake;
        [SerializeField] private GameController _gameController;

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
                _gameController.StartGameEvent += StartGameEvent;
                _gameController.InitSessionEvent += InitSessionEvent;
                
                _snake.GrowEvent += OnSnakeGrow;
            }
            else
            {
                _gameController.StartGameEvent -= StartGameEvent;
                _gameController.InitSessionEvent -= InitSessionEvent;
                
                _snake.GrowEvent -= OnSnakeGrow;
            }
        }

        private void InitSessionEvent(SessionData sessionData)
        {
            _score.Init(sessionData.ScorePoints);
        }

        private void StartGameEvent()
        {
            _score.Reset();
        }

        private void OnSnakeGrow()
        {
            _score.Add();
        }
    }
}