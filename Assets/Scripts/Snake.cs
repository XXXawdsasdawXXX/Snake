using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("Services")] 
    [SerializeField] private InputService _input;
    [Header("Snake components")]
    [SerializeField] private Transform _segmentPrefab;
    [SerializeField] private Vector2Int _direction = Vector2Int.right;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private int _initialSize = 4;
    [SerializeField] private bool _moveThroughWalls;

    private readonly List<Transform> _segments = new();
    private float _nextUpdate;

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

        _nextUpdate = Time.time + (1f / (_speed * _speedMultiplier));
    }

    public void ResetState()
    {
        _direction = Vector2Int.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (int i = 0; i < _initialSize - 1; i++)
        {
            Grow();
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(_segmentPrefab);
        segment.position = _segments[^1].position;
        _segments.Add(segment);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetState();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            if (_moveThroughWalls)
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