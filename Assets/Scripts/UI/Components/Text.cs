using UnityEngine;

namespace UI.Components
{
    public class Text : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Text _text;
            
        
        public void SetText(string field)
        {
            _text.text = field;
        }
        
    }
}