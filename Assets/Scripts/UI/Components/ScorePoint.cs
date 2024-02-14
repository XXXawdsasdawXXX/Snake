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
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _passedSprite;
        [SerializeField] private GameObject _starObject;
        
        public void Init(int scoreCount)
        {
            _scoreEditableText.SetText(scoreCount.ToString());
            Reset();
        }

        public void Reset()
        {
            _backgroundImage.sprite = _defaultSprite;
            _starObject.SetActive(false);
        }

        public void SetAsPassed()
        {
            _backgroundImage.sprite = _passedSprite;
            _starObject.SetActive(true);
        }

        public void SetPosition(Vector3 position)
        {
            var pos = new Vector3(position.x, _rectTransform.anchoredPosition.y, 0);
            _rectTransform.anchoredPosition = pos;
        }
    }
}