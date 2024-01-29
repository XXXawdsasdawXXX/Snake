using Logic;
using Services;
using UI.Components.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TopPanelAdapter : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private Score _score;
        [SerializeField] private Button _pauseButton;

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
            }
            else
            {
                _gameController.InitSessionEvent -= OnInitSession;
                _gameController.ResetGameEvent -= OnResetGame;
                _gameController.PauseEvent -= OnPauseGame;
                _score.ChangeEvent -= OnScoreChange;
            }
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