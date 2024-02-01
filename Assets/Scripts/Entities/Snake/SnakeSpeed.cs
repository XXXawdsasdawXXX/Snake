using Configs;
using UnityEngine;

namespace Entities
{
    public class SnakeSpeed : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private SnakeConfig _snakeConfig;
        [SerializeField] private float _speedIncrease = 0.1f;

        private SnakeStaticData _data;
        private float _targetBonusSpeed;
        private float _currentBonusSpeed;


        private void Awake()
        {
            _data = _snakeConfig.StaticData;
        }

        private void Update()
        {
            if (_snake.IsActive && _targetBonusSpeed > _currentBonusSpeed)
            {
                _currentBonusSpeed = Mathf.Lerp(_currentBonusSpeed, _targetBonusSpeed, _speedIncrease * Time.deltaTime);
            }
        }

        public void AddBonusSpeed()
        {
            if (_data.Speed + _currentBonusSpeed < _data.MaxSpeed)
            {
                _targetBonusSpeed += _data.BonusSpeedStep;
            }
        }

        public float GetSpeed()
        {
            return _data.Speed + _currentBonusSpeed;
        }

        public void ResetBonusSpeed()
        {
            _targetBonusSpeed = 0;
            _currentBonusSpeed = 0;
        }
    }
}