using System;
using UnityEngine;

namespace Services
{
    public class JSApi
    {
        [Obsolete("Obsolete")]
        public static void Log(string msg)
        {
            Application.ExternalEval($"console.log(\'{msg}\')");
        }
        
        [Obsolete("Obsolete")]
        public static void SessionEnd(int wonBonuses)
        {
            Application.ExternalEval("var sessionEnd = new CustomEvent(\"sessionEnd\",{bubbles: truedetail:{wonBonuses:" +
                                     $"{wonBonuses}" +
                                     "}}); " +
                                     "dispatchEvent(sessionEnd);");
        }
    }
}