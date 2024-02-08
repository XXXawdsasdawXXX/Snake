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
        [SerializeField] private Button _pauseButton;
        [SerializeField] private HealthBar _healthBar;

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
                _gameController.PauseEvent += OnPauseGame;
                _score.ChangeEvent += OnScoreChange;
                _health.ChangeValueEvent += HealthOnChangeValueEvent;
            }
            else
            {
                _gameController.InitSessionEvent -= OnInitSession;
                _gameController.ResetGameEvent -= OnResetGame;
                _gameController.PauseEvent -= OnPauseGame;
                _score.ChangeEvent -= OnScoreChange;
                _health.ChangeValueEvent -= HealthOnChangeValueEvent;
            }
        }

        private void HealthOnChangeValueEvent(int value)
        {
            _healthBar.SetValue(value);
        }

        private void OnPauseGame(bool isPause)
        { 
            _pauseButton.interactable = !isPause;
        }

        private void OnResetGame()
        {
            _progressBar.Reset();
        }
        
        private void OnScoreChange(int current)
        {
            _progressBar.SetScoreValue(current);
        }

        private void OnInitSession(SessionData sessionData)
        {
            _progressBar.Init(sessionData.ScorePoints);
        }
    }
}