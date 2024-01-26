using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class TextInitSetter : MonoBehaviour
    {
        [SerializeField] private bool _isInitial = true;
        [SerializeField] private Text _text;
        [SerializeField] private string _field;

        private void Awake()
        {
            if (_isInitial)
            {
                _text.text = _field;
            }
        }

        private void OnValidate()
        {
            if (_text == null)
            {
                TryGetComponent(out _text);
            }
            if (_field == "" && _text != null)
            {
                _field = _text.text;
            }
        }
    }
}