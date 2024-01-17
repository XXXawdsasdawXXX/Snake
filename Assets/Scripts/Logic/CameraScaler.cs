using System.Collections;
using UnityEngine;
using Utils;

namespace Logic
{
    public class CameraScaler : MonoBehaviour
    {
        private Camera _camera;

        private bool _isSetCorrectSize;

        private Coroutine _coroutine;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _distance = 10;

        private void Awake()
        {
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

        private void Update()
        {
        }

        private IEnumerator SetCameraSize()
        {
            Debugging.Instance?.Log("start", Debugging.Type.Camera);
            while (!_isSetCorrectSize)
            {
                Check2d();
                yield return null;
            }

            Debugging.Instance?.Log("is not try set", Debugging.Type.Camera);
        }

        private void Check2d()
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

        private void Check3d(float period)
        {
            float width = _camera.pixelWidth;
            float height = _camera.pixelHeight;
            Vector2 topRight = _camera.ScreenToWorldPoint(new Vector2(width, height));

            var point = new Vector3(topRight.x, topRight.y / 2);
            point.z = -10;
            point.y = 0;
            RaycastHit hit;
            Ray ray = new Ray(point, Vector3.forward);
            Debug.DrawRay(point, Vector3.forward * _distance, Color.green, period);
            if (Physics.Raycast(ray, out hit, _distance, _layerMask))
            {
                Transform objectHit = hit.transform;
                Debugging.Instance?.Log($"objectHit name {objectHit.gameObject.name}", Debugging.Type.Camera);
                // Do something with the object that was hit by the raycast.
                if (objectHit.gameObject.CompareTag("Wall"))
                {
                    _isSetCorrectSize = true;
                }
            }
            else
            {
                _camera.orthographicSize -= 0.05f;
                Debugging.Instance?.Log($"set camera size", Debugging.Type.Camera);
            }
        }
    }
}