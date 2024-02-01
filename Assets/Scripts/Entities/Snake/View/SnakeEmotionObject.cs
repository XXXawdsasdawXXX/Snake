using UnityEngine;

namespace Entities
{
    public abstract class SnakeEmotionObject : MonoBehaviour
    {
        [SerializeField] private float _reactionDistance;

        public bool IsActive { get; protected set; }
        public abstract bool IsReady();

        public abstract void StartReaction();

        public abstract void StopReaction();

        protected bool IsNear(Vector3 objectPosition)
        {
            return Vector3.Distance(transform.position,objectPosition) <= _reactionDistance;
        }
    }
}