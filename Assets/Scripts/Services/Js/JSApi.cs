using UnityEngine;

namespace Services
{
    public class JSApi
    {
        public static void Log(string msg)
        {
            Application.ExternalEval($"console.log(\'{msg}\')");
        }
        
        public static void SessionEnd(int  wonBonuses)
        {
            Application.ExternalCall("OnSessionEnd", wonBonuses);
        }
    }
}