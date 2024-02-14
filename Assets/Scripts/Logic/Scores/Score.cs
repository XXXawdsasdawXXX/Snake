using System;
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
        public event Action AchieveMaxScoreEvent;
        public event Action<int> ChangeEvent;
        

        public void Init(int[] saveScorePoints)
        {
            _saveScorePoints = new int[saveScorePoints.Length + 1];
            for (int i = 0; i < saveScorePoints.Length  ; i++)
            {
                _saveScorePoints[i + 1] = saveScorePoints[i];
            }
        }

        public void Add()
        {
            _currentScore++;
            ChangeEvent?.Invoke(_currentScore);
            if (_currentScore % 5 == 0)
            {
                SetEvenFiveEvent?.Invoke();
            }

            if (_nextScorePointNumber < _saveScorePoints.Length )
            {
                if (_currentScore == _saveScorePoints[_nextScorePointNumber])
                {
                    _nextScorePointNumber++;
              
                    UpdateSavePointEvent?.Invoke();
                    
                    if (_nextScorePointNumber == _saveScorePoints.Length)
                    {
                        AchieveMaxScoreEvent?.Invoke();
                    }
                }
            }
        }


        public int GetCurrentStepNumber()
        {
            return _nextScorePointNumber - 1;
        }

        public int GetCurrentReward()
        {
            return _saveScorePoints[_nextScorePointNumber - 1];
        }
        public void Reset()
        {
            _currentScore = 0;
            _nextScorePointNumber = 1;
        }

        public int GetMaxReward()
        {
            return _saveScorePoints[^1];
        }
    }
}