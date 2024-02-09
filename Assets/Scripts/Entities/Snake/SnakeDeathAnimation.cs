using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class SnakeDeathAnimation : MonoBehaviour
    {
        [SerializeField] private Snake _snake;
        [Space]
        [SerializeField] private int _flashingCount =3;
        [SerializeField] private float _flashingDelay =1;
        [SerializeField] private GameObject[] _flashingObjects;

        private Coroutine _coroutine;

        public event Action EndDeathAnimationEvent;


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
            if (_flashingCount == 0)
            {
                yield return new WaitForSeconds(2);
                EndDeathAnimationEvent?.Invoke();
                yield break;
            }
            
            for (int i = 0; i < _flashingCount; i++)
            {
                yield return new WaitForSeconds(_flashingDelay);
                foreach (var obj in _flashingObjects)
                {
                    int layer = LayerMask.NameToLayer("NotVisible");
                    obj.layer = layer;
                }

                if (i == _flashingCount - 1)
                {
                    EndDeathAnimationEvent?.Invoke();
                }
                yield return new WaitForSeconds(_flashingDelay);
                foreach (var obj in _flashingObjects)
                {
                    int layer = LayerMask.NameToLayer("Default");
                    obj.layer = layer;
                }
                
            }
        }
    }
}