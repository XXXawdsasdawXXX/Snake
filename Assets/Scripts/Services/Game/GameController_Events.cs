﻿using System;

namespace Services
{
    public  partial class GameController
    {
        
        private void InvokeResetGameEvent() => ResetGameEvent?.Invoke();
        public Action ResetGameEvent;
        
        private void InvokeStartGameEvent() => StartGameEvent?.Invoke();
        public  Action StartGameEvent;

        private void InvokeEndGameEvent(int rewardValue, bool isMaxReward) => EndGameEvent?.Invoke(rewardValue,isMaxReward);
        public  Action<int, bool> EndGameEvent;

        private void InvokeInitSession(SessionData sessionData) => InitSessionEvent?.Invoke(sessionData);
        public  Action<SessionData> InitSessionEvent;

        private void InvokePauseGame(bool isPause) => PauseEvent?.Invoke(isPause);
        public  Action<bool> PauseEvent;

        private void InvokeCloseGame(int wonBonus)
        {
            CloseGameEvent?.Invoke();
            JSApi.SessionEnd(wonBonus);
        }
        public Action CloseGameEvent;

    }
}