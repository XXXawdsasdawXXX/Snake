using UnityEngine;
using Utils;

namespace Logic
{
    public class CanvasScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvas;
        private void Awake()
        {
            if (!Constants.IsMobileDevice())
            {
                Debugging.Instance?.Log($"Camera can't set other size", Debugging.Type.Camera);
                return;
            }

            var oldSize = _canvas.sizeDelta;
            _canvas.sizeDelta = new Vector2(2000, oldSize.y);

        }
    }
}