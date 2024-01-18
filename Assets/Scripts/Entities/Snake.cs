using System;
using System.Collections.Generic;
using Configs;
using Services;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private InputService _input;
        [SerializeField] private SnakeConfig _snakeConfig;
        [SerializeField] private SnakeSegment _headSnakeSegment;

        private SnakeSegment _segmentPrefab;
        private SnakeStaticData _data;
        public  List<SnakeSegment> Segments { get; } = new();
        private Vector2Int _moveDirection;
        private float _currentBonusSpeed;
        private float _nextUpdate;
        public event Action<Vector2Int> SetNewMoveDirectionEvent; 

        private void Awake()
        {
            _data = _snakeConfig.StaticData;
            _segmentPrefab = _snakeConfig.SegmentPrefab;
            _moveDirection = _input.GetDirection();
            ResetState();
        }
        
        private void Update()
        {
            if (Time.time < _nextUpdate)
            {
                return;
            }

            TrySetMoveDirection();

            var period = 1f / (_data.Speed + _currentBonusSpeed) * GetMultiplier();

            Move(period);

            _nextUpdate = Time.time + period;
        }

        public void ResetState()
        {
            Debugging.Instance.Log("Reset",Debugging.Type.Snake);
            _headSnakeSegment.StopMove();
            _headSnakeSegment.transform.position = Vector3.zero;
        
            _moveDirection = Constants.DEFAULT_DIRECTION;
            _currentBonusSpeed = 0;
        
            for (int i = 1; i < Segments.Count; i++)
            {
                Segments[i].StopMove();
                Destroy(Segments[i].gameObject);
            }

            Segments.Clear();
            Segments.Add(_headSnakeSegment);
        
            for (int i = 0; i < _data.InitialSize - 1; i++)
            {
                Grow();
            }
        }

        public bool Occupies(int x, int y)
        {
            foreach (SnakeSegment segment in Segments)
            {
                if (Mathf.RoundToInt(segment.transform.position.x) == x &&
                    Mathf.RoundToInt(segment.transform.position.y) == y)
                {
                    return true;
                }
            }

            return false;
        }

        public void Grow()
        {
            Debugging.Instance.Log("Grow",Debugging.Type.Snake);
            for (int i = 0; i < Constants.SEGMENT_COUNT; i++)
            {
                SnakeSegment segment = Instantiate(_segmentPrefab);
                segment.transform.position = new Vector3(Segments[^1].LastTarget.x, Segments[^1].LastTarget.y, 0);
                Segments.Add(segment);
            }
        }

        public void AddSpeedMultiplier()
        {
            _currentBonusSpeed += _data.BonusSpeedStep;
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

        private void TrySetMoveDirection()
        {
            var inputDirection = _input.GetDirection();
            if (IsCanSetDirection(inputDirection))
            {
                _moveDirection = inputDirection;
                SetNewMoveDirectionEvent?.Invoke(_moveDirection);
            }
        }
        private bool IsCanSetDirection(Vector2Int inputDirection)
        {
            return inputDirection != _moveDirection && 
                   (_headSnakeSegment.Target.x % 1 == 0 && _headSnakeSegment.Target.y % 1 == 0) && 
                   _headSnakeSegment.MoveDirection != inputDirection * -1;
        }

        private float GetMultiplier()
        {
            return 100 / Constants.SEGMENT_COUNT * 0.01f;
        }
    }
}
