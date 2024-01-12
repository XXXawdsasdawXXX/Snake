using System;
using System.Collections.Generic;
using Configs;
using DefaultNamespace;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private InputService _input;
    [SerializeField] private SnakeConfig _snakeConfig;

    private Transform _segmentPrefab;
    private SnakeStaticData _data;
    
    private float _currentBonusSpeed = 0;
    private Vector2Int _direction;
    private readonly List<Transform> _segments = new();
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
    
    private void FixedUpdate()
    {
        if (Time.time < _nextUpdate)
        {
            return;
        }

        if (_input.GetForward() != Vector2Int.zero)
        {
            _direction = _input.GetForward();
        }

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        
        int x = Mathf.RoundToInt(transform.position.x) + _direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + _direction.y;
        transform.position = new Vector2(x, y);

        _nextUpdate = Time.time + (1f / (_data.Speed + _currentBonusSpeed));
    }

    public void ResetState()
    {
        _direction = Vector2Int.right;
        transform.position = Vector3.zero;
        _currentBonusSpeed = 0;
        
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (int i = 0; i < _data.InitialSize - 1; i++)
        {
            Grow();
        }
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in _segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }

        return false;
    }

    private void Grow()
    {
        Transform segment = Instantiate(_segmentPrefab);
        segment.position = _segments[^1].position;
        _segments.Add(segment);
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