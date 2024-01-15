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
    public  List<SnakeSegment> Segments { get; private set; } = new();
    private float _nextUpdate;


    private void Awake()
    {
        _data = _snakeConfig.StaticData;
        _segmentPrefab = _snakeConfig.SegmentPrefab;
        _direction = _input.GetForward();
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

        if (_input.GetForward() != Vector2Int.zero && _input.GetForward() != _direction)
        {
            _direction = _input.GetForward();
        }

        var period = 1f / (_data.Speed + _currentBonusSpeed);

        var x = _headSnakeSegment.Target.x + _direction.x;
        var y = _headSnakeSegment.Target.y + _direction.y;

        _headSnakeSegment.StartMove(new Vector2Int(x, y), period);
        for (int i = 1; i < Segments.Count; i++)
        {
            Segments[i].StartMove(Segments[i - 1].LastTarget, period);
        }

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

    private void Grow()
    {
        Debug.Log("Grow");
        for (int i = 0; i < 2; i++)
        {
            SnakeSegment segment = Instantiate(_segmentPrefab);
            segment.transform.position = new Vector3(Segments[^1].LastTarget.x, Segments[^1].LastTarget.y, 0);
            Segments.Add(segment);
        }
    }

    private void AddSpeedMultiplier()
    {
        _currentBonusSpeed += _data.BonusSpeedStep;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
            AddSpeedMultiplier();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetState();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            if (_data.MoveThroughWalls)
            {
                Traverse(other.transform);
            }
            else
            {
                ResetState();
            }
        }
    }

    private void Traverse(Transform wall)
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
}