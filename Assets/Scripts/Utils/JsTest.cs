using System.Collections;
using Services;
using UI.Components;
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
            UIEvents.ClickButtonEvent += ClickButtonEvent;
                StartTestSession();
            }
        }

        private void ClickButtonEvent(EventButtonType buttonType)
        {
            if (buttonType == EventButtonType.Close)
            {
                
                StartCoroutine(ReloadWithDelay());
            }
        }

        public void StartTestSession()
        {
            _jsService.TestSessionData(_testSessionData);
        }

        private IEnumerator ReloadWithDelay()
        {
            Debugging.Instance.Log("ReloadWithDelay",Debugging.Type.JS);
            yield return new WaitForSeconds(1);
                _jsService.TestSessionData(_testSessionData);
        }
    }
}