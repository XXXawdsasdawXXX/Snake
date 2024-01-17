using UnityEngine;

namespace Services
{
    public class JSService : MonoBehaviour
    {
        //Этот метод будет вызван из js
        public void SessionData(string jsonData)
        {
            /*var sessionData = JsonUtility.FromJson<SessionData>(jsonData);*/

            /*InvokeSessionEvent(sessionData);
            _isFirstSession = false;
            Log(
                $"Ticket set! Step = {sessionData.StepID} | prize {sessionData.prize} | last prize {sessionData.lastPrize} ");*/
        }
    }
}