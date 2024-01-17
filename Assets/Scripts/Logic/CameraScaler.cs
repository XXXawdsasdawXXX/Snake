using System.Collections;
using UnityEngine;
using Utils;

namespace Logic
{
    public class CameraScaler : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _distance = 10;

        private bool _isSetCorrectSize;
        private Coroutine _coroutine;

        private void Awake()
        {
            if (Screen.width > Screen.height)
            {
                Debugging.Instance?.Log($"Camera can't set other size", Debugging.Type.Camera);
                return;
            }

            _camera = Camera.main;
            _coroutine = StartCoroutine(SetCameraSize());
        }

        void OnDestroy()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private IEnumerator SetCameraSize()
        {
            Debugging.Instance?.Log($"Start set camera size: old size = {_camera.orthographicSize}",
                Debugging.Type.Camera);
            while (!_isSetCorrectSize)
            {
                Check2D();
                yield return null;
            }

            Debugging.Instance?.Log($"End set camera size: new size = {_camera.orthographicSize}",
                Debugging.Type.Camera);
        }

        private void Check2D()
        {
            float width = _camera.pixelWidth;
            float height = _camera.pixelHeight;
            Vector2 topRight = _camera.ScreenToWorldPoint(new Vector2(width, height));
            var point = new Vector3(topRight.x, 0, -10);

            var hit = Physics2D.Raycast(point, Vector3.zero, _distance, _layerMask);

            if (!hit || (hit && !hit.transform.gameObject.CompareTag(Constants.Tag.Wall.ToString())))
            {
                _camera.orthographicSize -= 0.05f;
            }
            else if (hit && hit.transform.gameObject.CompareTag(Constants.Tag.Wall.ToString()))
            {
                _isSetCorrectSize = true;
            }
        }
    }
}