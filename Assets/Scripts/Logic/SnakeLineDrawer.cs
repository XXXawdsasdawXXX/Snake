using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;
using Utils;

namespace Logic
{
    public class SnakeLineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private RoundedCornerLine _roundedCorner;
        [SerializeField] private Transform _trail;
        [SerializeField] private Snake _snake;

        [SerializeField] private int curveResolution = 2;
        [SerializeField] private List<Vector3> _linePoints;

        private int _currentSize;

        private void Awake()
        {
            _snake.ResetEvent += OnResetSnake;
        }

        private void Update()
        {
            if (_snake.IsActive)
            {
                RefreshLinePoints();
                /*_line.positionCount = _linePoints.Count;
                 _line.SetPositions(_linePoints.ToArray());*/
                _roundedCorner.SetPoints(_linePoints.AsListVector2());
            }
        }

        private void OnDestroy()
        {
            _snake.ResetEvent -= OnResetSnake;
        }

        private void OnResetSnake()
        {
            ResetLine();
        }

        private void RefreshLinePoints()
        {
            var snakeSegments = _snake.Segments.Where(segment => segment.IsMoving).ToArray();

            SetPoints(snakeSegments);
        }

        private void ResetLine()
        {
            var snakeSegments = _snake.Segments.Where(segment => segment.IsMoving).ToArray();

            SetPoints(snakeSegments);
            _line.positionCount = _linePoints.Count;
            _line.SetPositions(_linePoints.ToArray());
            _roundedCorner.SetPoints(_linePoints.AsListVector2());
        }

        private void SetPoints(SnakeSegment[] snakeSegments)
        {
            _linePoints = new List<Vector3>();

            var length = GetLenght(snakeSegments);

            for (int i = 0; i < length - 1; i++)
            {
                _linePoints.Add(snakeSegments[i].transform.position);

                for (int j = 1; j < curveResolution; j++)
                {
                    float t = j / (float)curveResolution;
                    _linePoints.Add(Vector3.Lerp(snakeSegments[i].transform.position,
                        snakeSegments[i + 1].transform.position,
                        t));
                }
            }

            if (snakeSegments.Length > 1)
            {
                _linePoints.Add(snakeSegments[^1].transform.position);
            }

            if (_trail != null)
            {
                _trail.transform.position = snakeSegments[^1].transform.position;
            }
        }

        private int GetLenght(SnakeSegment[] snakeSegments)
        {
            for (int i = snakeSegments.Length + 1; i > 0; i--)
            {
                if ((i - 1) % Constants.SEGMENT_COUNT == 0)
                {
                    return i;
                }
            }
            return snakeSegments.Length;
        }
    }
}