using UnityEngine;

namespace Logic.Health
{
    public class HealthIcon : MonoBehaviour
    {
        [SerializeField] private GameObject _activeObjets;

        public void Activate()
        {
            _activeObjets.SetActive(true);
        }

        public void Deactivate()
        {
            _activeObjets.SetActive(false);
        }
        
    }
}