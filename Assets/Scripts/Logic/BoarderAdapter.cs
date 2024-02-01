using UnityEngine;
using Utils;

namespace Logic
{
    public class BoarderAdapter : MonoBehaviour
    {
        [SerializeField] private GameObject[] _boarders;

        private void Awake()
        {
            if (Constants.IsMobileDevice())
            {
                foreach (var boarder in _boarders)
                {
                    boarder.SetActive(false);
                }
            }
        }
    }
}