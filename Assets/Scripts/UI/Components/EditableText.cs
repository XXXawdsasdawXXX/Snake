using TMPro;
using UnityEngine;

namespace UI.Components
{
    public class EditableText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void SetText(string field)
        {
            _text.SetText(field);
        }
        
    }
}