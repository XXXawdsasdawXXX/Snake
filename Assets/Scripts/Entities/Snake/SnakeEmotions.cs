using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class SnakeEmotions : MonoBehaviour
    {
        [SerializeField] private List<SnakeEmotionObject> _emotionObjects;

        private bool _isActivePupils;

        private void Update()
        {

            foreach (var emotionObject in _emotionObjects)
            {
                if (emotionObject.IsReady())
                {
                    emotionObject.StartReaction();
                }
                else if(emotionObject.IsActive)
                {
                    emotionObject.StopReaction();
                }
            }
        }

    
     
    }
}