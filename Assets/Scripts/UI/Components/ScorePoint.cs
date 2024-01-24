using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ScorePoint : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Image _backgroundImage;
        [Space]
        [SerializeField] private Color32 _defaultColor;
        [SerializeField] private Color32 _passedColor;
        
        public void Init(int scoreCount)
        {
            _scoreText.SetText(scoreCount.ToString());
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
    }
}