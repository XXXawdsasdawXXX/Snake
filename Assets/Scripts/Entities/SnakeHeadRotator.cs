using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Entities
{
    public class SnakeHeadRotator : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private Transform _headRoot;

        private Dictionary<Vector2Int, float> _rotateForwards = new Dictionary<Vector2Int, float>
        {
            {new Vector2Int(0,1),0},
            {new Vector2Int(1,0),-90},
            {new Vector2Int(0,0),-90},
            {new Vector2Int(0,-1),-180},
            {new Vector2Int(-1,0), 90},
        };

        private void Awake()
        {
            SetHeadRotation(Vector2Int.right);
        }

        private void OnEnable()
        {
            _snake.SetNewMoveDirectionEvent += OnSetNewMoveDirection;
        }

        private void OnDisable()
        {
            _snake.SetNewMoveDirectionEvent -= OnSetNewMoveDirection;
            
        }

        private void OnSetNewMoveDirection(Vector2Int newDirection)
        {
            SetHeadRotation(newDirection);
        }

        private void SetHeadRotation(Vector2Int newDirection)
        {
            _headRoot.transform.rotation = Quaternion.Euler(0, 0, _rotateForwards[newDirection]);
        }
    }
}