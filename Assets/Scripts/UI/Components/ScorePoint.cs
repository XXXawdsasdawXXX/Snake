using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ScorePoint : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private EditableText _scoreEditableText;
        [SerializeField] private Image _backgroundImage;
        [Space]
        [SerializeField] private Color32 _defaultColor;
        [SerializeField] private Color32 _passedColor;
        
        public void Init(int scoreCount)
        {
            _scoreEditableText.SetText(scoreCount.ToString());
            Reset();
        }

        public void Reset()
        {
            _backgroundImage.color = _defaultColor;
        }

        public void SetAsPassed()
        {
            _backgroundImage.color = _passedColor;
        }

        public void SetPosition(Vector3 position)
        {
            _rectTransform.anchoredPosition = position;
        }
    }
}