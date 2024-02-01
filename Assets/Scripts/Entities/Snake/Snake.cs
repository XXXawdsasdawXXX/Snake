using System;
using System.Collections.Generic;
using Configs;
using Logic;
using Services;
using Services.Audio;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Snake : MonoBehaviour
    {
        public List<SnakeSegment> Segments /*{ get; }*/ = new();
        public bool IsActive { get; private set; }

        [SerializeField] private InputService _input;
        [SerializeField] private Score _score;
        [SerializeField] private Transform _trailSegmentsRoot;

        [Header("SnakeComponents")] 
        [SerializeField] private SnakeConfig _snakeConfig;
        [SerializeField] private SnakeSegment _headSnakeSegment;
        [SerializeField] private SnakeHeadAnimation _snakeHeadAnimation;

        private SnakeSegment _segmentPrefab;
        private SnakeStaticData _data;

        private readonly Queue<Vector2Int> _inputDirections = new();
        private Vector2Int _moveDirection;

        private float _currentBonusSpeed;
        private float _nextUpdate;

        public event Action<Vector2Int> SetNewMoveDirectionEvent;
        public event Action ObstacleCollisionEvent;
        public event Action GrowEvent;
        public event Action ResetEvent;


        private void Awake()
        {
            _data = _snakeConfig.StaticData;
            _segmentPrefab = _snakeConfig.SegmentPrefab;
            _moveDirection = _input.GetDirection();
            SetNewMoveDirectionEvent?.Invoke(_moveDirection);
        }

        private void OnEnable()
        {
            _input.SetNewDirectionEvent += AddInputDirection;
            _score.SetEvenFiveEvent += AddSpeedMultiplier;
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }

            if (Time.time < _nextUpdate)
            {
                return;
            }

            TrySetDirection();

            var period = 1f / (_data.Speed + _currentBonusSpeed) * GetMultiplier();

            Move(period);

            _nextUpdate = Time.time + period;
        }

        private void OnDisable()
        {
            _input.SetNewDirectionEvent -= AddInputDirection;
            _score.SetEvenFiveEvent -= AddSpeedMultiplier;
        }

        public void StartMove()
        {
            IsActive = true;
            foreach (var snakeSegment in Segments)
            {
                snakeSegment.Collision.EnableCollision();
            }
        }

        public void StopMove()
        {
            IsActive = false;
            foreach (var snakeSegment in Segments)
            {
                snakeSegment.Collision.DisableCollision();
            }
        }

        public void InvokeCollisionEvent()
        {
            if (IsActive)
            {
                _snakeHeadAnimation.PlayDead();
                ObstacleCollisionEvent?.Invoke();
            }
        }

        public void ResetState()
        {
            Debugging.Instance.Log("Reset", Debugging.Type.Snake);

            IsActive = false;
            _snakeHeadAnimation.ResetAnimation();

            _headSnakeSegment.StopMove();
            _headSnakeSegment.transform.position =
                Vector3.zero - Constants.DEFAULT_DIRECTION.AsVector3() * GetMultiplier();
            _currentBonusSpeed = 0;

            _inputDirections.Clear();
            _moveDirection = Constants.DEFAULT_DIRECTION;
            SetNewMoveDirectionEvent?.Invoke(_moveDirection);

            for (int i = 1; i < Segments.Count; i++)
            {
                Segments[i].StopMove();
                Destroy(Segments[i].gameObject);
            }

            Segments.Clear();
            Segments.Add(_headSnakeSegment);

            InitGrow();

            ResetEvent?.Invoke();
        }

        public void Grow()
        {
            for (int i = 0; i < Constants.SEGMENT_COUNT; i++)
            {
                SnakeSegment segment = Instantiate(_segmentPrefab, _trailSegmentsRoot, true);
                segment.transform.position = new Vector3(Segments[^1].LastTarget.x, Segments[^1].LastTarget.y, 0);
                Segments.Add(segment);
                segment.Collision.EnableCollision();
            }

            GrowEvent?.Invoke();
        }

        private void InitGrow()
        {
            _headSnakeSegment.SetTarget(_headSnakeSegment.transform.position);
            for (int i = 0; i < Constants.SEGMENT_COUNT * _data.InitialSize; i++)
            {
                SnakeSegment segment = Instantiate(_segmentPrefab, _trailSegmentsRoot, true);

                var positionX = i * GetMultiplier();
                segment.transform.position = new Vector3(_headSnakeSegment.transform.position.x - positionX,
                    _headSnakeSegment.transform.position.y, 0);
                segment.SetTarget(new Vector3(_headSnakeSegment.transform.position.x - positionX,
                    _headSnakeSegment.transform.position.y, 0));
                Segments.Add(segment);
                segment.gameObject.name += $"{Segments.Count}";
            }
        }

        public void AddSpeedMultiplier()
        {
            if (_data.Speed + _currentBonusSpeed < _data.MaxSpeed)
            {
                _currentBonusSpeed += _data.BonusSpeedStep;
            }
        }

        public bool Occupies(int x, int y)
        {
            foreach (var segment in Segments)
            {
                var distance = Vector3.Distance(segment.transform.position, new Vector3(x, y, 0));
                if (distance < 1)
                {
                    return true;
                }
            }

            return false;
        }

        public void Traverse(Transform wall)
        {
            Vector3 position = transform.position;

            if (_moveDirection.x != 0f)
            {
                position.x = Mathf.RoundToInt(-wall.position.x + _moveDirection.x);
            }
            else if (_moveDirection.y != 0f)
            {
                position.y = Mathf.RoundToInt(-wall.position.y + _moveDirection.y);
            }

            transform.position = position;
        }

        private void Move(float period)
        {
            var x = _headSnakeSegment.Target.x + (_moveDirection.x * GetMultiplier());
            var y = _headSnakeSegment.Target.y + (_moveDirection.y * GetMultiplier());

            _headSnakeSegment.StartMove(new Vector3(x, y, 0), period);
            for (int i = 1; i < Segments.Count; i++)
            {
                Segments[i].StartMove(Segments[i - 1].LastTarget, period);
            }
        }

        private void TrySetDirection()
        {
            if (_headSnakeSegment.Target.x % 1 == 0 && _headSnakeSegment.Target.y % 1 == 0)
            {
                Debugging.Instance.Log($"try setn direction { _inputDirections.Count}",Debugging.Type.Snake);
                for (int i = 0; i < _inputDirections.Count; i++)
                {
                    var inputDirection = _inputDirections.Dequeue();

                    Debugging.Instance.Log($"try setn direction {inputDirection} vs {_moveDirection}",Debugging.Type.Snake);
                    if (IsCanSetDirection(inputDirection))
                    {
                        _moveDirection = inputDirection;
                        AudioManager.Instance.PlayAudioEvent(_moveDirection);
                        SetNewMoveDirectionEvent?.Invoke(_moveDirection);
                        break;
                    }
                }
            }
        }

        private void AddInputDirection(Vector2Int direction)
        {
            if (_inputDirections.Count < 5)
            {
                _inputDirections.Enqueue(_input.GetDirection());
            }
        }

        private bool IsCanSetDirection(Vector2Int inputDirection)
        {
            return inputDirection != _moveDirection && _moveDirection != inputDirection * -1 &&
                   inputDirection != Vector2Int.zero;
        }

        private float GetMultiplier()
        {
            return 100 / Constants.SEGMENT_COUNT * 0.01f;
        }
    }
}