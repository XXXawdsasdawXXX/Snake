﻿using System;
using Services;
using UI.Components;
using UnityEngine;

namespace Logic
{
    public class Score : MonoBehaviour
    {
        public int CurrentScore => _currentScore;
        private int _currentScore;
        private int[] _saveScorePoints;
        private int _nextScorePointNumber;

        public event Action SetEvenFiveEvent;
        public event Action UpdateSavePointEvent;
        public event Action SetMaxScoreEvent;

        public void Init(int[] saveScorePoints)
        {
            _saveScorePoints = saveScorePoints;
        }

        public void Add()
        {
            _currentScore++;

            if (_currentScore % 5 == 0)
            {
                SetEvenFiveEvent?.Invoke();
            }

            if (_nextScorePointNumber < _saveScorePoints.Length - 1)
            {
                if (_currentScore == _saveScorePoints[_nextScorePointNumber + 1])
                {
                    _nextScorePointNumber++;
                    UpdateSavePointEvent?.Invoke();
                    if (_nextScorePointNumber == _saveScorePoints.Length - 1)
                    {
                        SetMaxScoreEvent?.Invoke();
                    }
                }
            }
        }

        public void Reset()
        {
            _currentScore = 0;
            _nextScorePointNumber = 1;
        }
    }
}