using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform _startCameraTransform;
    [SerializeField]
    private float _cameraMovementTime = 3f;
    [SerializeField]
    private float _rotationSpeed = 60;
    [SerializeField]
    private float _zoomSpeed = 3f;

    private float _minDistance;
    private float _maxDistance;

    private bool _isAttachedToPlanet;
    private Planet _currentPlanet;
    private float _distance;

    public delegate void CallBack();

    private void Start()
    {
        if (_startCameraTransform == null)
        {
            _startCameraTransform = new GameObject("Camera start position").transform;
            _startCameraTransform.position = transform.position;
            _startCameraTransform.rotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (!_isAttachedToPlanet)
            return;

        Zoom();

        if (!Input.GetMouseButton(1))
            return;

        CameraOrbitalMovement();
    }

    private void CameraOrbitalMovement()
    {
        var horizontal = Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        var vertical = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;

        transform.RotateAround(_currentPlanet.transform.position, Vector3.up, horizontal);
        transform.RotateAround(_currentPlanet.transform.position, transform.right, -vertical);
    }

    private void Zoom()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
        _distance = Mathf.Clamp(_distance - scroll, _minDistance, _maxDistance);
        transform.localPosition = transform.localPosition.normalized * _distance;
    }

    public void AttachToPlanet(Planet planet, CallBack callBack)
    {
        _currentPlanet = planet;
        StartCoroutine(AttachCameraToPlanet(callBack));
    }

    public void ResetCamera(CallBack callBack)
    {
        _isAttachedToPlanet = false;
        _currentPlanet = null;
        StartCoroutine(ResetCameraTransform(callBack));
    }

    private IEnumerator ResetCameraTransform(CallBack callBack)
    {
        var time = 0f;
        transform.SetParent(null);
        var startPos = transform.position;
        var startRotation = transform.rotation;

        while (time < _cameraMovementTime)
        {
            time += Time.deltaTime;

            var process = time / _cameraMovementTime;

            var newPos = Vector3.Lerp(startPos, _startCameraTransform.position, process);
            var newRot = Quaternion.Lerp(startRotation, _startCameraTransform.rotation, process);

            transform.position = newPos;
            transform.rotation = newRot;

            yield return null;
        }

        transform.position = _startCameraTransform.position;
        transform.rotation = _startCameraTransform.rotation;

        callBack();
    }

    private IEnumerator AttachCameraToPlanet(CallBack callBack)
    {
        var time = 0f;
        var planetRadious = _currentPlanet.radious;

        while (time < _cameraMovementTime)
        {
            time += Time.deltaTime;
            var process = time / _cameraMovementTime;

            var targetPos = _currentPlanet.transform.position - _currentPlanet.transform.forward * planetRadious;

            var newPos = Vector3.Lerp(_startCameraTransform.position, targetPos, process);
            transform.position = newPos;
            transform.LookAt(_currentPlanet.transform);
            yield return null;
        }

        transform.SetParent(_currentPlanet.transform);
        transform.localPosition = -_currentPlanet.transform.forward * planetRadious;

        // current distance to planet
        _distance = planetRadious;
        // min distance to planet
        _minDistance = planetRadious * 0.85f;
        // max distance to planet
        _maxDistance = planetRadious * 1.5f;

        _isAttachedToPlanet = true;
        callBack();
    }
}
