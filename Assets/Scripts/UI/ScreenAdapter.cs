using System;
using System.Collections;
using Services;
using UI.Components;
using UnityEngine;
using Utils;
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

        [SerializeField] private Screen _openedScreen;

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
                UIEvents.ClickButtonEvent += OnClickButton;
            }
            else
            {
                _gameController.EndGameEvent -= OnEndGame;
                UIEvents.ClickButtonEvent -= OnClickButton;
            }
        }

        private void OnEndGame(bool isWin)
        {
            if (isWin)
            {
                StartCoroutine(ShowScreenWithDelay(_screenWin));
            }
            else
            {
                StartCoroutine(ShowScreenWithDelay(_screenLose));
            }
        }

        private void OnClickButton(EventButtonType type)
        {
            switch (type)
            {
                case EventButtonType.None:
                default:
                    break;
                case EventButtonType.Play:
                    _openedScreen?.Hide();
                    break;
                case EventButtonType.Close:
                    break;
                case EventButtonType.Pause:
                    break;
               
            }
            Debugging.Instance.Log($"Screen Controller -> On Click {type}", Debugging.Type.UI);
        }

        private IEnumerator ShowScreenWithDelay(Screen screen)
        {
            yield return new WaitForSeconds(_delayBeforeShow);
            _openedScreen = screen;
            _openedScreen.Show();
        }
    }
}