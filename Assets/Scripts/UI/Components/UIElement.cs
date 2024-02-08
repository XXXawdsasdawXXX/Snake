using System;
using UnityEngine;

namespace UI.Components
{
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField] protected RectTransform body;

        public bool IsActive()
        {
            return body != null && body.gameObject.activeSelf;
        }
        public virtual void Show(Action onShown = null)
        {
            body.gameObject.SetActive(true);
            onShown?.Invoke();
        }

        public virtual void Hide(Action onHidden = null)
        {
            body.gameObject.SetActive(false);
            onHidden?.Invoke();
        }
    }
}