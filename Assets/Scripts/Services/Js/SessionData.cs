using System;

namespace Services
{
    [Serializable]
    public class SessionData
    {
        public bool isMuted;
        public int[] saveScorePoints = new int[3];
    }
}