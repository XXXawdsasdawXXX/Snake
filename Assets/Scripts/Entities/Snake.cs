using System.Collections.Generic;
using Configs;
using DefaultNamespace;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private InputService _input;
    [SerializeField] private SnakeConfig _snakeConfig;
    [SerializeField] private SnakeSegment _headSnakeSegment;

    private SnakeSegment _segmentPrefab;
    private SnakeStaticData _data;

    private float _currentBonusSpeed;
    private Vector2Int _direction;
    public  List<SnakeSegment> Segments { get; } = new();
    private float _nextUpdate;

    private void Awake()
    {
        _data = _snakeConfig.StaticData;
        _segmentPrefab = _snakeConfig.SegmentPrefab;
        _direction = _input.GetDirection();
    }

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Time.time < _nextUpdate)
        {
            return;
        }

        TrySetInputDirection();

        var period = 1f / (_data.Speed + _currentBonusSpeed) * GetMultiplier();

        Move(period);

        _nextUpdate = Time.time + period;
    }

    public void ResetState()
    {
        Debug.Log("Reset");
        _headSnakeSegment.StopMove();
        _headSnakeSegment.transform.position = Vector3.zero;
        
        _direction = Vector2Int.right;
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
        Debug.Log("Grow");
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

        if (_direction.x != 0f)
        {
            position.x = Mathf.RoundToInt(-wall.position.x + _direction.x);
        }
        else if (_direction.y != 0f)
        {
            position.y = Mathf.RoundToInt(-wall.position.y + _direction.y);
        }

        transform.position = position;
    }
    
    private void Move(float period)
    {
        var x = _headSnakeSegment.Target.x + (_direction.x * GetMultiplier());
        var y = _headSnakeSegment.Target.y + (_direction.y * GetMultiplier());

        _headSnakeSegment.StartMove(new Vector3(x, y, 0), period);
        for (int i = 1; i < Segments.Count; i++)
        {
            Segments[i].StartMove(Segments[i - 1].LastTarget, period);
        }
    }

    private void TrySetInputDirection()
    {
        var inputDirection = _input.GetDirection();
        if (IsCanSetDirection(inputDirection))
        {
            _direction = inputDirection;
        }
    }
    private bool IsCanSetDirection(Vector2Int inputDirection)
    {
        return inputDirection != _direction && 
               (_headSnakeSegment.Target.x % 1 == 0 && _headSnakeSegment.Target.y % 1 == 0) && 
               _headSnakeSegment.MoveDirection != inputDirection * -1;
    }

    private float GetMultiplier()
    {
        return 100 / Constants.SEGMENT_COUNT * 0.01f;
    }
}
