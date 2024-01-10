using DefaultNamespace;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private InputService _input;
    [SerializeField] private Grid _grid;
    [Header("Params")] 
    [SerializeField] private float _moveInterval = 0.5f;

    private Vector3 _targetGridPosition;
    private Vector3 _moveDirection = Vector3.right;
    private float _timer = 0f;

    void Start()
    {
        _targetGridPosition = _grid.GetCellCenterWorld(_grid.WorldToCell(transform.position));
    }

    void Update()
    {
        SetDirection();

        _timer += Time.fixedDeltaTime;

        if (_timer >= _moveInterval)
        {
            Move();
            _timer = 0f;
        }
    }

    private void Move()
    {
        _targetGridPosition += _moveDirection * _grid.cellSize.x;
        transform.position = _targetGridPosition;
    }

    private void SetDirection()
    {
        Vector2 inputDirection = _input.GetForward();

        if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.y))
        {
            _moveDirection = new Vector3(inputDirection.x, 0f, 0f);
        }
        else
        {
            _moveDirection = new Vector3(0f, inputDirection.y, 0f);
        }
    }
}