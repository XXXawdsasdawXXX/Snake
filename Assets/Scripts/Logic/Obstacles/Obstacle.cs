using UnityEngine;

namespace Entities
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private Food _food;
        
        [SerializeField] private Transform[] _parts;

        public bool IsCanActivate()
        {
            if (Vector3.Distance(_snake.transform.position, transform.position) < 3f)
            {
                return false;
            }

            foreach (var part in _parts)
            {
                var x = Mathf.RoundToInt(part.position.x);
                var y = Mathf.RoundToInt(part.position.y);
                if (_snake.Occupies(x,y ) || _food.Occupies(x,y))
                {
                    return false;
                }
            }

            return true;
        }
        
        public bool Occupies(int x, int y)
        {
            foreach (var part in _parts)
            {
                if (Mathf.RoundToInt(part.transform.position.x) == x &&
                    Mathf.RoundToInt(part.transform.position.y) == y)
                {
                    return true;
                }
            }

            return false;
        }


        public void Activate()
        {
            foreach (var part in _parts)
            {
                part.gameObject.SetActive(true);
            }
        }

        public void Deactivate()
        {
            foreach (var part in _parts)
            {
                part.gameObject.SetActive(false);
            }  
        }
    }
}