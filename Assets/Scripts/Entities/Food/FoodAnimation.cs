using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class FoodAnimation : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DOScale(new Vector3(1.4f,1.4f,1), 1).SetLoops(-1, LoopType.Yoyo)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}