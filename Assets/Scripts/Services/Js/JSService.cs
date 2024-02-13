using System;
using UnityEngine;
using Utils;

namespace Services
{
    public class JSService : MonoBehaviour
    {
        public event Action<SessionData> InitSessionEvent;
        public event Action<bool> SetMuteAudioEvent;
        public event Action CloseEvent;

        //Эти методы будут вызваны из js
        
        public void MuteAudio()
        {
            SetMuteAudioEvent?.Invoke(true);
        }

        public void UnMuteAudio()
        {
            SetMuteAudioEvent?.Invoke(false);
        }
        
        public void SessionData(string jsonData)
        {
            var sessionData = JsonUtility.FromJson<SessionData>(jsonData);

            InitSessionEvent?.Invoke(sessionData);
            SetMuteAudioEvent?.Invoke(sessionData.isMuted);
  
            Debugging.Instance.Log($"Session set!", Debugging.Type.JS);
        }
        
        public void TestSessionData(SessionData sessionData)
        {
            InitSessionEvent?.Invoke(sessionData);
  
            Debugging.Instance.Log($"Test session set!", Debugging.Type.JS);
        }
        
    }
}