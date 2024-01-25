﻿using UnityEngine;
using Utils;

namespace UI.Components.Screens
{
    public class ProgressBar : UIElement
    {
        [SerializeField] private float _weight;
        [SerializeField] private FillBar _fillBar;
        [SerializeField] private ScorePoint[] _scorePoints;

        private int[] _savePoints;

        public void Init(int[] savePoints)
        {
            Debugging.Instance.Log($"Init progress bar {savePoints[0]} {savePoints[1]} {savePoints[2]}",Debugging.Type.UI);
            _savePoints = new int[savePoints.Length + 1];
            for (int i = 1; i < _scorePoints.Length; i++)
            {
                _savePoints[i ] = savePoints[i - 1];
                _scorePoints[i].Init(_savePoints[i ]);
                _scorePoints[i].Reset();
            }

            _scorePoints[0].Init(0);
            _scorePoints[0].SetAsPassed();
            _fillBar.UpdateValue(0, _savePoints[^1]);


            for (int i = 0; i < _savePoints.Length; i++)
            {
                float percent = _savePoints[i] == 0 ? 0 : (float)_savePoints[i] / (float)_savePoints[^1];
                var x =  _weight / 100 * (percent * 100) - _weight / 2;
                var position = new Vector3(x, 0, 0);
                _scorePoints[i].SetPosition(position);
            }
        }

        public void SetScoreValue(int current)
        {
            _fillBar.UpdateValue(current, _savePoints[^1]);
        }

        public void Reset()
        {
            for (int i = 1; i < _scorePoints.Length; i++)
            {
                _scorePoints[i].Reset();
            }

            _fillBar.UpdateValue(0, _savePoints[^1]);
        }
    }
}