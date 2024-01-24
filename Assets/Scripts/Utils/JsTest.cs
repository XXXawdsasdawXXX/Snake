using System;
using Services;
using UnityEngine;

namespace Utils
{
    public class JsTest : MonoBehaviour
    {
        [SerializeField] private JSService _jsService;
        [SerializeField] private SessionData _testSessionData;

        private void Start()
        {
            StartTestSession();
        }

        public void StartTestSession()
        {
            //var jsonData = JsonUtility.ToJson(_testSessionData);
            _jsService.SessionData(_testSessionData);
        }
    }
}