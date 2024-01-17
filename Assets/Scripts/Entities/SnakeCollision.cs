using Configs;
using UnityEngine;
using Utils;

namespace Entities
{
    public class SnakeCollision : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private SnakeConfig _snakeConfig;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.Food.ToString()))
            {
                _snake.Grow();
                _snake.AddSpeedMultiplier();
            }
            else if (other.gameObject.CompareTag(Constants.Tag.Obstacle.ToString()))
            {
                _snake.ResetState();
            }
            else if (other.gameObject.CompareTag(Constants.Tag.Wall.ToString()))
            {
                if (_snakeConfig.StaticData.MoveThroughWalls)
                {
                    _snake.Traverse(other.transform);
                }
                else
                {
                    _snake.ResetState();
                }
            }
        }
    }
}