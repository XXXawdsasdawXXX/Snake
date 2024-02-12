using Logic;
using Logic.Health;
using Services;
using UI.Components;
using UI.Components.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TopPanelAdapter : MonoBehaviour
    {
        [Header("services")]
        [SerializeField] private GameController _gameController;
        [SerializeField] private Score _score;
        [SerializeField] private Health _health;
        [Header("ui components")]
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private EditableText _scoreCountText;
        
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
                _gameController.InitSessionEvent += OnInitSession;
                _gameController.ResetGameEvent += OnResetGame;
                _score.ChangeEvent += OnScoreChange;
                _health.ChangeValueEvent += HealthOnChangeValueEvent;
            }
            else
            {
                _gameController.InitSessionEvent -= OnInitSession;
                _gameController.ResetGameEvent -= OnResetGame;
                _score.ChangeEvent -= OnScoreChange;
                _health.ChangeValueEvent -= HealthOnChangeValueEvent;
            }
        }

        private void HealthOnChangeValueEvent(int value)
        {
            _healthBar.SetValue(value);
        }


        private void OnResetGame()
        {
            _progressBar.Reset();
            _scoreCountText.SetText(0.ToString());
        }
        
        private void OnScoreChange(int current)
        {
            _scoreCountText.SetText(current.ToString());
            _progressBar.SetScoreValue(current);
        }

        private void OnInitSession(SessionData sessionData)
        {
            _progressBar.Init(sessionData.ScorePoints);
        }
    }
}