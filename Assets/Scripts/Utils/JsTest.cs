using Services;
using UnityEngine;

namespace Utils
{
    public class JsTest : MonoBehaviour
    {
        [SerializeField] private JSService _jsService;
        [SerializeField] private SessionData _testSessionData;
        [SerializeField] private bool _isInitSessionOnStart = true;

        private void Start()
        {
            if (_isInitSessionOnStart)
            {
                StartTestSession();
            }
        }

        public void StartTestSession()
        {
            _jsService.TestSessionData(_testSessionData);
        }
    }
}