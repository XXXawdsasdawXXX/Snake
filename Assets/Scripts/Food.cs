using UnityEngine;

namespace DefaultNamespace
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private Collider2D _gridArea;
        [SerializeField] private Snake _snake;
        
        private void Start()
        {
            RandomizePosition();
        }

        private void RandomizePosition()
        {
            Bounds bounds = _gridArea.bounds;

            int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
            int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

            while (_snake.Occupies(x, y))
            {
                x++;

                if (x > bounds.max.x)
                {
                    x = Mathf.RoundToInt(bounds.min.x);
                    y++;

                    if (y > bounds.max.y) {
                        y = Mathf.RoundToInt(bounds.min.y);
                    }
                }
            }

            transform.position = new Vector2(x, y);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            RandomizePosition();
        }  
    }
}