using System;
using System.Collections;
using Entities;
using Services;
using Services.Audio;
using UI.Components;
using UI.Screens;
using UnityEngine;
using Utils;
using Screen = UI.Screens.Screen;


namespace UI
{
    public class ScreenAdapter : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private float _delayBeforeShow = 1;
        [Header("Screens")] 
        [SerializeField] private RewardScreen _screenWin;
        [SerializeField] private RewardScreen _screenNotBad;
        [SerializeField] private Screen _screenLose;
        [SerializeField] private Screen _screenPause;
        [SerializeField] private Screen _tutorialScreen;
        [SerializeField] private InitScreen _initScreen;
        [SerializeField] private BlackScreen _blackScreen;
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

        public bool IsEmpty()
        {
            return _openedScreen is TutorialScreen || !_openedScreen.IsActive();
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _gameController.InitSessionEvent += OnInitSession;
                _gameController.EndGameEvent += OnEndGame;
                _gameController.PauseEvent += OnPauseGame;
                _gameController.StartGameEvent += HideTutorial;
                _gameController.CloseGameEvent += CloseGameEvent;
                
                UIEvents.ClickButtonEvent += ClickButtonEvent;
            }
            else
            {
                _gameController.InitSessionEvent -= OnInitSession;
                _gameController.EndGameEvent -= OnEndGame;
                _gameController.PauseEvent -= OnPauseGame;
                _gameController.StartGameEvent -= HideTutorial;
                
                UIEvents.ClickButtonEvent -= ClickButtonEvent;
            }
        }

        private void CloseGameEvent()
        {
            ShowScreen(_blackScreen);
        }

        private void OnInitSession(SessionData obj)
        {
            _initScreen.SetSessionPrice(obj.saveScorePoints[^1].ToString());
            _blackScreen.Hide();
            Debugging.Instance.Log("On init session", Debugging.Type.UI);
            ShowScreen(_initScreen);
        }


        private void ClickButtonEvent(EventButtonType obj)
        {
            switch (obj)
            {
                case EventButtonType.None:
                    break;
                case EventButtonType.Play:
                    _openedScreen?.Hide();
                    break;
                case EventButtonType.Close:
                    break;
                case EventButtonType.Pause:
                    break;
            }
        }

        private void HideTutorial()
        {
            _tutorialScreen.Hide();
            _gameController.StartGameEvent -= HideTutorial;
        }
        
        private void OnPauseGame(bool isPause)
        {
            TryStopCoroutine();
            if (isPause)
            {
                ShowScreen(_screenPause);
            }
            else
            {
                _openedScreen?.Hide();
            }
        }

        private void OnEndGame(int reward,bool isWin)
        {
            TryStopCoroutine();
            if (isWin)
            {
                _screenWin.SetReward(reward.ToString());
                _coroutine = StartCoroutine(ShowScreenWithDelay(_screenWin, 
                    onShow: () => AudioManager.Instance.PlayAudioEvent(AudioEventType.Win)));
            }
            else if(reward > 0 )
            {
                _screenNotBad.SetReward(reward.ToString());
                _coroutine = StartCoroutine(ShowScreenWithDelay(_screenNotBad, 
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
            ShowScreen(screen);
            onShow?.Invoke();
        }

        private void TryStopCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void ShowScreen(Screen screen)
        {
            _openedScreen?.Hide();
            _openedScreen = screen;
            _openedScreen.Show();
        }
    }
}