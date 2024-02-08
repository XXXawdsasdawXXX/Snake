using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class SnakeDeathFlashing : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [Space]
        [SerializeField] private int _flashingCount =3;
        [SerializeField] private float _delay =1;
        [SerializeField] private GameObject[] _flashingObjects;

        private Coroutine _coroutine;

        public event Action LastFlashingEvent;


        private void OnEnable()
        {
            _snake.ObstacleCollisionEvent += PlayFlashingAnimation;
        }


        private void OnDisable()
        {
            _snake.ObstacleCollisionEvent -= PlayFlashingAnimation;
            
        }

        [ContextMenu("Play animation")]
        public void PlayFlashingAnimation()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(FlashRoutine());
        }
        
        private IEnumerator FlashRoutine()
        {
            for (int i = 0; i < _flashingCount; i++)
            {
                yield return new WaitForSeconds(_delay);
                foreach (var obj in _flashingObjects)
                {
                    int layer = LayerMask.NameToLayer("NotVisible");
                    obj.layer = layer;
                }

                if (i == _flashingCount - 1)
                {
                    LastFlashingEvent?.Invoke();
                }
                yield return new WaitForSeconds(_delay);
                foreach (var obj in _flashingObjects)
                {
                    int layer = LayerMask.NameToLayer("Default");
                    obj.layer = layer;
                }
                
            }
        }
    }
}