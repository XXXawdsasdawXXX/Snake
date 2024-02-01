using System;
using System.Collections;
using Services;
using Services.Audio;
using UnityEngine;
using Screen = UI.Screens.Screen;


namespace UI
{
    public class ScreenAdapter : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private float _delayBeforeShow = 1;
        [Header("Screens")] 
        [SerializeField] private Screen _screenWin;
        [SerializeField] private Screen _screenLose;
        [SerializeField] private Screen _screenPause;
        [SerializeField] private Screen _tutorialScreen;
        [Space] 
        [SerializeField] private Screen _openedScreen;

        private Coroutine _coroutine;

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
                _gameController.EndGameEvent += OnEndGame;
                _gameController.ResetGameEvent += OnResetGame;
                _gameController.PauseEvent += OnPauseGame;
                _gameController.StartGameEvent += HideTutorial;
            }
            else
            {
                _gameController.EndGameEvent -= OnEndGame;
                _gameController.PauseEvent -= OnPauseGame;
                _gameController.ResetGameEvent -= OnResetGame;
            }
        }

        private void HideTutorial()
        {
            _tutorialScreen.Hide();
            _gameController.StartGameEvent -= HideTutorial;
        }

        private void OnResetGame()
        {
            _openedScreen?.Hide();
        }

        private void OnPauseGame(bool isPause)
        {
            TryStopCoroutine();
            if (isPause)
            {
                _openedScreen?.Hide();
                _openedScreen = _screenPause;
                _openedScreen.Show();
            }
            else
            {
                _openedScreen?.Hide();
            }
        }

        private void OnEndGame(bool isWin)
        {
            TryStopCoroutine();
            if (isWin)
            {
                _coroutine = StartCoroutine(ShowScreenWithDelay(_screenWin, 
                    onShow: () => AudioManager.Instance.PlayAudioEvent(AudioEventType.Win)));
            }
            else
            {
                _coroutine = StartCoroutine(ShowScreenWithDelay(_screenLose,
                    onShow: () => AudioManager.Instance.PlayAudioEvent(AudioEventType.Lose)));
            }
        }

        private IEnumerator ShowScreenWithDelay(Screen screen, Action onShow = null)
        {
            yield return new WaitForSeconds(_delayBeforeShow);
            _openedScreen = screen;
            _openedScreen.Show();
            onShow?.Invoke();
        }

        private void TryStopCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }
}