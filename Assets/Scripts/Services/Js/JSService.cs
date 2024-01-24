using System;
using UnityEngine;
using Utils;

namespace Services
{
    public class JSService : MonoBehaviour
    {
        public event Action<SessionData> InitSessionEvent;
        //Этот метод будет вызван из js
        public void SessionData(string jsonData)
        {
            var sessionData = JsonUtility.FromJson<SessionData>(jsonData);

            InitSessionEvent?.Invoke(sessionData);
  
            Debugging.Instance.Log($"Session set!", Debugging.Type.JS);
        }
        
        public void SessionData(SessionData sessionData)
        {
            InitSessionEvent?.Invoke(sessionData);
  
            Debugging.Instance.Log($"Session set!", Debugging.Type.JS);
        }
        
    }
}