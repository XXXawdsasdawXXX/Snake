using System.Collections;
using DG.Tweening;
using Entities;
using Logic;
using Logic.Health;
using UI;
using UI.Components;
using UnityEngine;
using Utils;

namespace Services
{
    public partial class GameController : MonoBehaviour
    {
        [SerializeField] private SnakeDeathAnimation _snakeAnimation;
        [SerializeField] private Snake _snake;
        [SerializeField] private Score _score;
        [SerializeField] private InputService _input;
        [SerializeField] private JSService _jsService;
        [SerializeField] private Health _health;
        [SerializeField] private ScreenAdapter _screenAdapter;

        [SerializeField] private GameState _gameState;
        public GameState GameState => _gameState;

        private bool _isPlaying;
        private bool _isPause;

        private void Awake()
        {
            DOTween.SetTweensCapacity(500, 50);
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
                _health.ChangeValueEvent += CheckLose;
                _input.SetNewDirectionEvent += TryStartGame;
                _score.AchieveMaxScoreEvent += WonGame;
                _snakeAnimation.EndDeathAnimationEvent += ResetGame;
            }
            else
            {
                UIEvents.ClickButtonEvent -= OnClickButton;

                _health.ChangeValueEvent += CheckLose;
                _input.SetNewDirectionEvent -= TryStartGame;
                _score.AchieveMaxScoreEvent -= WonGame;
                _snakeAnimation.EndDeathAnimationEvent -= ResetGame;
            }
        }
        

        private void CheckLose(int healthValue)
        {
            Debugging.Instance.Log($"Check lose {_score.GetCurrentStepNumber()}", Debugging.Type.GameController);
            if ((healthValue == 0 || _score.GetCurrentStepNumber() > 0) && _gameState == GameState.Play)
            {
                LoseGame();
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
                case EventButtonType.Play:
                    ResetGame();
                    break;
                case EventButtonType.Pause:
                    PauseGame(!_isPause);
                    break;
            }
        }

        private void OnInitSession(SessionData sessionData)
        {
            Debugging.Instance.Log($"On init", Debugging.Type.GameController);
            InvokeInitSession(sessionData);
            ResetGame();

            SubscribeToEvents(true);
        }

        private void TryStartGame(Vector2Int direction)
        {
            if (_gameState == GameState.AwaitInput && direction != Vector2Int.zero)
            {
                if (_isPlaying)
                {
                    _gameState = GameState.Play;
                    _snake.StartMove();
                }
                else if(_screenAdapter.IsEmpty())
                {
                    StartCoroutine(StartGame());
                }
            }
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(0.25f);

            if (_isPause || _isPlaying)
            {
                yield break;
            }

            Debugging.Instance.Log($"Start game", Debugging.Type.GameController);

            _isPlaying = true;
            _gameState = GameState.Play;
            _snake.StartMove();
            InvokeStartGameEvent();
        }

        private void ResetGame()
        {
            Debugging.Instance.Log($"Reset game", Debugging.Type.GameController);
            _isPlaying = false;
            _gameState = GameState.AwaitInput;
            _snake.ResetState();
            InvokeResetGameEvent();
        }

        private void WonGame()
        {
            Debugging.Instance.Log($"Won game", Debugging.Type.GameController);
            _gameState = GameState.EndGame;
            _snake.StopMove();
            InvokeEndGameEvent(_score.GetMaxReward(),true);
            _isPlaying = false;
        }

        private void LoseGame()
        {
            Debugging.Instance.Log($"Lose game", Debugging.Type.GameController);
            _gameState = GameState.EndGame;
            InvokeEndGameEvent(_score.GetCurrentReward(),false);
            _isPlaying = false;
        }

        private void CloseGame()
        {
            Debugging.Instance.Log($"Close game", Debugging.Type.GameController);
            InvokeCloseGame(_score.GetCurrentReward());
        }

        private void PauseGame(bool isPause)
        {
            if (_isPause == isPause || _gameState is GameState.EndGame)
            {
                return;
            }

            Debugging.Instance.Log($"Pause game {isPause}", Debugging.Type.GameController);

            _isPause = isPause;
            if (isPause)
            {
                Time.timeScale = 0;
                InvokePauseGame(true);
            }
            else
            {
                Time.timeScale = 1;
                InvokePauseGame(false);
            }
        }
    }
}