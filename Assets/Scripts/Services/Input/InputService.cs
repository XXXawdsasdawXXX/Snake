using System;
using Services.Audio;
using UnityEngine;
using Utils;

namespace Services
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;

        private IInputDirectionListener _mouseDirectionListener;
        private IInputDirectionListener _keyDirectionListener;
        private IInputDirectionListener _currentDirectionListener;

        private bool _isMouse;
        private bool _isPlaying;

        private Vector2Int _direction;
        public event Action<Vector2Int> SetNewDirectionEvent;

        private void Awake()
        {
            _isMouse = Screen.width < Screen.height;
            _keyDirectionListener = new KeyDirectionListener();
            _mouseDirectionListener = new MouseDirectionListener();
            SubscribeToEvents(true);
        }

        private void Update()
        {
            if (Input.anyKeyDown && _isMouse)
            {
                _isMouse = false;
                _keyDirectionListener.SetDirection(_mouseDirectionListener.GetDirection());
                _currentDirectionListener = _keyDirectionListener;
            }

            if (Input.GetMouseButtonDown(0) && !_isMouse)
            {
                _isMouse = true;
                _mouseDirectionListener.SetDirection(_keyDirectionListener.GetDirection());
                _currentDirectionListener = _mouseDirectionListener;
            }

            if (_gameController.GameState is GameState.Play or GameState.AwaitInput)
            {
                if (!_isPlaying)
                {
                    _isPlaying = true;
                }
                
                _currentDirectionListener?.SetDirection();

                var dir = GetDirection();
                if (_direction != dir)
                {

                    Debugging.Instance.Log($"Switch is playing {_isPlaying} ", Debugging.Type.Input);

                    Debugging.Instance.Log($"Set new direction {dir} ", Debugging.Type.Input);
                    _direction = dir;
                    SetNewDirectionEvent?.Invoke(_direction);
                }
            }
        }


        private void OnDestroy()
        {
            SubscribeToEvents(false);
        }

        public Vector2Int GetDirection()
        {
            return _currentDirectionListener?.GetDirection() ?? Vector2Int.zero;
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _gameController.PauseEvent += OnPauseGame;
                _gameController.ResetGameEvent += OnResetGame;
            }
            else
            {
                _gameController.PauseEvent -= OnPauseGame;
                _gameController.ResetGameEvent -= OnResetGame;
            }
        }

        private void OnResetGame()
        {
            _currentDirectionListener?.Reset();
            _isPlaying = false;
        }

        private void OnPauseGame(bool isPause)
        {
            if (isPause)
            {
                _direction = Vector2Int.zero;
                _currentDirectionListener.Reset();
            }

            Debugging.Instance.Log($"On pause -> is playing {_isPlaying} ", Debugging.Type.Input);
        }
    }
}