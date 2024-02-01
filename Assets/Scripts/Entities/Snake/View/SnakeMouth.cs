using UnityEngine;

namespace Entities
{
    public class SnakeMouth :SnakeEmotionObject
    {
        [SerializeField] private Food _food;
        [SerializeField] private SnakeHeadAnimation _headAnimation;

        public override bool IsReady()
        {
            return IsNear(_food.transform.position);
        }

        public override void StartReaction()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            _headAnimation.PLayStartEat();
        }

        public override void StopReaction()
        {
            _headAnimation.PLayStopEat();
            IsActive = false;
        }
        
    }
}