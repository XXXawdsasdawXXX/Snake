using UnityEngine;

namespace Entities
{
    public class SnakeHeadAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Eat = Animator.StringToHash("Eat");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Reset = Animator.StringToHash("Reset");

     
        public void PLayStartEat() => _animator.SetBool(Eat, true);
        
        public void PLayStopEat() => _animator.SetBool(Eat, false);

        public void PlayDead() => _animator.SetTrigger(Die);
        
        public void ResetAnimation() => _animator.SetTrigger(Reset);
        
    }
}