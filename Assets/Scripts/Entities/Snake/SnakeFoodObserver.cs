using UnityEngine;

namespace Entities
{
    public class SnakeFoodObserver : MonoBehaviour
    {
        [SerializeField] private Food _food;
        [SerializeField] private float _lookDistance = 5;
        [Space] 
        [SerializeField] private SnakeHeadAnimation headAnimation;
        [SerializeField] private SnakePupilsRotator _pupilsRotator;

        private bool _isReset;

        private void Update()
        {
            if (IsNearFood())
            {
                _pupilsRotator.LookTo(_food.transform.position);
                if (_isReset)
                {
                    headAnimation.PLayStartEat();
                    _isReset = false;
                }
            }
            else if (!_isReset)
            {
                headAnimation.PLayStopEat();
                _pupilsRotator.Reset();
                _isReset = true;
            }
        }

        public bool IsNearFood()
        {
            return Vector3.Distance(transform.position, _food.transform.position) <= _lookDistance;
        }
    }
}