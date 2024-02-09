using Logic;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private Collider2D _gridArea;
        [SerializeField] private Snake _snake;
        [SerializeField] private ObstaclesController _obstaclesController;
        [SerializeField] private GameController _gameController;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public bool IsActive => _spriteRenderer != null && _spriteRenderer.enabled;

        private void Awake()
        {
            _gameController.ResetGameEvent += DisableSprite;
            _gameController.StartGameEvent += RandomizePosition;
            _gameController.StartGameEvent += EnableSprite;
        }

        private void OnDestroy()
        {
            _gameController.ResetGameEvent -= DisableSprite;
            _gameController.StartGameEvent -= RandomizePosition;
            _gameController.StartGameEvent -= EnableSprite;
        }

        private void RandomizePosition()
        {
            Bounds bounds = _gridArea.bounds;

            int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
            int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

            while (_snake.Occupies(x, y) || _obstaclesController.Occupies(x, y))
            {
                x++;

                if (x > bounds.max.x)
                {
                    x = Mathf.RoundToInt(bounds.min.x);
                    y++;

                    if (y > bounds.max.y)
                    {
                        y = Mathf.RoundToInt(bounds.min.y);
                    }
                }
            }

            transform.position = new Vector2(x, y);
        }

        public bool Occupies(int x, int y)
        {
            return Mathf.RoundToInt(transform.position.x) == x &&
                   Mathf.RoundToInt(transform.position.y) == y;
        }

        private void DisableSprite()
        {
            _spriteRenderer.enabled = false;
        }

        private void EnableSprite()
        {
            _spriteRenderer.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            RandomizePosition();
        }
    }
}