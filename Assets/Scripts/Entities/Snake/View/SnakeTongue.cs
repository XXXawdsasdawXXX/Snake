using System;
using System.Collections;
using UnityEngine;

namespace Entities
{
    public class SnakeTongue : SnakeEmotionObject
    {
        [SerializeField] private Snake _snake;
        [SerializeField] private float _minCooldown = 5,_maxCooldown = 7;
        [SerializeField] private Food _food;
        [SerializeField] private Animator _tongueAnimator;

        public bool IsShowTongue { get; private set; }

        private bool _cooldownIsUp;

        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        
        private void Start()
        {
            StopReaction();
            _snake.ResetEvent += InvokeEndAnimation;
        }


        private void OnDestroy()
        {
            _snake.ResetEvent -= InvokeEndAnimation;
        }

        public override bool IsReady()
        {
            return !IsNear(_food.transform.position) && _cooldownIsUp;
        }

        public override void StartReaction()
        {
            IsShowTongue = true;
            _tongueAnimator.SetTrigger(Show);
            StopReaction();
        }

        public override void StopReaction()
        {
            _cooldownIsUp = false;
            StartCoroutine(CooldownRoutine());
        }

        public void HideTongue()
        {
            _tongueAnimator.SetTrigger(Hide);
        }
        
        private IEnumerator CooldownRoutine()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minCooldown,_maxCooldown));
            _cooldownIsUp = true;
        }

        /// <summary>
        /// Animation event
        /// </summary>
        private void InvokeEndAnimation()
        {
            IsShowTongue = false;
        }
    }
}