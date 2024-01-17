using UnityEngine;

namespace Services
{
    public class JSApi
    {
        //Метод, который го
        public static void Log(string msg)
        {
            Application.ExternalEval($"console.log(\'{msg}\')");
        }

        public static void PlayerSelectedChest(string str)
        {
            Application.ExternalCall("OnPlayerSelectedChest",str);
        }

        public static void SessionEnd()
        {
            Application.ExternalCall("OnSessionEnd");
        } 
    }
}