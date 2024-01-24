using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class SnakeSegmentCollision : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private float _enableDelay = 1.5f;
        private Coroutine _coroutine;

        private void Awake()
        {
            DisableCollision();
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        
        private IEnumerator EnableCollisionWithDelay()
        {
            yield return new WaitForSeconds(_enableDelay);
            _collider2D.enabled = true;
        }


        public  void EnableCollision()
        {
            _coroutine = StartCoroutine(EnableCollisionWithDelay());
        }
        public void DisableCollision()
        {
            _collider2D.enabled = false;
        }
    }
}