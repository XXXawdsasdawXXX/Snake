using System.Collections;
using Entities;
using Logic;
using UI.Components;
using UnityEngine;
using Utils;

namespace Services
{
    public partial class GameController : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private Score _score;
        [SerializeField] private InputService _input;
        [SerializeField] private JSService _jsService;

        [SerializeField] private GameState _gameState;
        public GameState GameState => _gameState;

        private bool _isPlaying;
        private bool _isPause;

        private void Awake()
        {
            //DOTween.SetTweensCapacity(500);
            _jsService.InitSessionEvent += OnInitSession;
        }

        private void OnDestroy()
        {
            SubscribeToEvents(false);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                UIEvents.ClickButtonEvent += OnClickButton;

                _input.SetNewDirectionEvent += TryStartGame;
                _snake.ObstacleCollisionEvent += LoseGame;
                _score.SetMaxScoreEvent += WonGame;
            }
            else
            {
                UIEvents.ClickButtonEvent -= OnClickButton;

                _input.SetNewDirectionEvent -= TryStartGame;
                _snake.ObstacleCollisionEvent -= LoseGame;
                _score.SetMaxScoreEvent -= WonGame;
            }
        }

        private void OnClickButton(EventButtonType buttonType)
        {
            switch (buttonType)
            {
                case EventButtonType.None:
                default:
                    break;
                case EventButtonType.Close:
                    CloseGame();
                    break;
                case EventButtonType.Pause:
                    PauseGame(!_isPause);
                    break;
                case EventButtonType.Play:
                    ResetGame();
                    break;
            }
        }

        private void OnInitSession(SessionData sessionData)
        {
            _jsService.InitSessionEvent -= OnInitSession;
            Debugging.Instance.Log($"On init", Debugging.Type.GameController);
            _gameState = GameState.AwaitInput;
            InvokeInitSession(sessionData);
            SubscribeToEvents(true);
        }

        private void TryStartGame(Vector2Int obj)
        {
            if (_gameState == GameState.AwaitInput && !_isPlaying)
            {
                _isPlaying = true;
               
                StartCoroutine(StartGame());
            }
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(0.25f);
            _gameState = GameState.Play;
            _snake.StartMove();
            InvokeStartGameEvent();
            Debugging.Instance.Log($"Start game", Debugging.Type.GameController);
        }

        private void ResetGame()
        {
            _isPlaying = false;
            _gameState = GameState.AwaitInput;
            _snake.ResetState();
            InvokeResetGameEvent();
            Debugging.Instance.Log($"Reset game", Debugging.Type.GameController);
        }

        private void WonGame()
        {
            Debugging.Instance.Log($"Won game", Debugging.Type.GameController);
            _gameState = GameState.EndGame;
            InvokeEndGameEvent(isWon: true);
            _isPlaying = false;
        }

        private void LoseGame()
        {
            Debugging.Instance.Log($"Lose game", Debugging.Type.GameController);
            _gameState = GameState.EndGame;
            InvokeEndGameEvent(isWon: false);
            _isPlaying = false;
        }

        private void PauseGame(bool isPause)
        {
            Debugging.Instance.Log($"Puse game {isPause}", Debugging.Type.GameController);
            _isPause = isPause;
            if (isPause)
            {
                _gameState = GameState.Pause;
            }
            else
            {
                _gameState = GameState.AwaitInput;
            }

            InvokePauseGame(isPause);
        }

        private void CloseGame()
        {
            Debugging.Instance.Log($"Close game", Debugging.Type.GameController);
            InvokeCloseGame(_score.CurrentScore);
        }
    }
}