using System;
using UI.Components;
using UnityEngine;

namespace Services
{
    public static class UIEvents
    {

        public static void InvokeClickButton(EventButtonType buttonType)
        {
            ClickButtonEvent?.Invoke(buttonType);
        }
        public static Action<EventButtonType> ClickButtonEvent;
    }
}