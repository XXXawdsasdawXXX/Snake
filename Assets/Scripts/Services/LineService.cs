using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class LineService : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;

        [SerializeField] private Snake _snake;

        private void Update()
        {
            RefreshLinePoints();
            _line.positionCount = _linePoints.Count;
            _line.SetPositions(_linePoints.ToArray());
        }

        [SerializeField] private List<Vector3> _linePoints;
        [SerializeField] private int curveResolution = 2;

        private void RefreshLinePoints()
        {
             //SetPointByTarget();  
             SetPointByTransform();
        }

        private void SetPointByTarget()
        {
            var snakeSegments = _snake.Segments.Where(segment => segment.IsSetTarget).ToArray();
            var snakeSegmentsTarget = snakeSegments.Select(segment => segment.Target).ToArray();

            _linePoints = new List<Vector3>();

            for (int i = 0; i < snakeSegmentsTarget.Length - 1; i++)
            {
                _linePoints.Add(snakeSegmentsTarget[i].AsVector3());

                for (int j = 1; j < curveResolution; j++)
                {
                    float t = j / (float)curveResolution;
                    _linePoints.Add(Vector3.Lerp(snakeSegmentsTarget[i].AsVector3(),snakeSegmentsTarget[i + 1].AsVector3(), t));
                }
            }
            _linePoints.Add(snakeSegmentsTarget[^1].AsVector3());
        }

        private void SetPointByTransform()
        {
            var snakeSegments = _snake.Segments.Where(segment => segment.IsSetTarget).ToArray();
            var snakeSegmentsTarget = snakeSegments.Select(segment => segment.Target).ToArray();

            _linePoints = new List<Vector3>();

            for (int i = 0; i < snakeSegments.Length - 1; i++)
            {
                _linePoints.Add(snakeSegments[i].transform.position);

                for (int j = 1; j < curveResolution; j++)
                {
                    float t = j / (float)curveResolution;
                    _linePoints.Add(Vector3.Lerp(snakeSegments[i].transform.position,snakeSegments[i + 1].transform.position, t));
                }
            }

            if (snakeSegments.Length > 1)
            {
                _linePoints.Add(snakeSegments[^1].transform.position);
                
            }
        }
        
    }
}