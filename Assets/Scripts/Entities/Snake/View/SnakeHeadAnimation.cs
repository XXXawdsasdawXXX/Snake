using System.Collections;
using UnityEngine;

namespace Entities
{
    public class SnakeHeadAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _pupilsRoot;
        [SerializeField] private SnakeTongue _tongue;
        
        
        private static readonly int Eat = Animator.StringToHash("Eat");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Reset = Animator.StringToHash("Reset");
        
        private Coroutine _coroutine;
        public void PLayStartEat()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(EatRoutine());
        }

        public void PLayStopEat() => _animator.SetBool(Eat, false);

        public void PlayDead()
        {
            _animator.SetTrigger(Die);
            _pupilsRoot.SetActive(false);
            _tongue.gameObject.SetActive(false);
        }

        public void ResetAnimation()
        {
            _animator.SetTrigger(Reset);
            _pupilsRoot.SetActive(true);
            _tongue.gameObject.SetActive(true);
        }

        private IEnumerator EatRoutine()
        {
            yield return new WaitUntil(() => !_tongue.IsShowTongue);
            _animator.SetBool(Eat, true);
            _coroutine = null;
        }
    }
}