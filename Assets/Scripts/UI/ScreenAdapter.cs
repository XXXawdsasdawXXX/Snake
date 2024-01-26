using System.Collections;
using Services;
using UnityEngine;
using Screen = UI.Components.Screen;


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
                _gameController.PauseEvent += PauseEvent;
                _gameController.StartGameEvent += StartGameEvent;
            }
            else
            {
                _gameController.EndGameEvent -= OnEndGame;
                _gameController.PauseEvent -= PauseEvent;
                _gameController.StartGameEvent -= StartGameEvent;
            }
        }

        private void StartGameEvent()
        {
            _openedScreen?.Hide();
        }

        private void PauseEvent(bool isPause)
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
                _coroutine = StartCoroutine(ShowScreenWithDelay(_screenWin));
            }
            else
            {
                _coroutine = StartCoroutine(ShowScreenWithDelay(_screenLose));
            }
        }
        
        private IEnumerator ShowScreenWithDelay(Screen screen)
        {
            yield return new WaitForSeconds(_delayBeforeShow);
            _openedScreen = screen;
            _openedScreen.Show();
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