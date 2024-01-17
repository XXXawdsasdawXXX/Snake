using System.Collections;
using UnityEngine;

namespace Entities
{
    public class SnakeSegmentCollision : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;

        private Coroutine _coroutine;
        private void Start()
        {
            _coroutine = StartCoroutine(EnableCollision());
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator EnableCollision()
        {
            yield return new WaitForSeconds(1f);
            _collider2D.enabled = true;
        }
    }
}