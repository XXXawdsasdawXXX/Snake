using System;
using UnityEngine;
using Utils;

namespace UI.Components
{
    public class TutorialScreen : Screen
    {
        [SerializeField] private bool _isShowTutorial = true;
        [SerializeField] private GameObject _mobileTutorial;
        [SerializeField] private GameObject _standaloneTutorial;

        private void Awake()
        {
            if (_isShowTutorial)
            {
                if (Constants.IsMobileDevice())
                {
                    _mobileTutorial.SetActive(true);
                    _standaloneTutorial.SetActive(false);
                }
                else
                {
                    _mobileTutorial.SetActive(false);
                    _standaloneTutorial.SetActive(true);
                }
            }
        }
    }
}