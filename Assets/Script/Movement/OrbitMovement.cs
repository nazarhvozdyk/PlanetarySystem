using UnityEngine;

public class OrbitMovement
{
    private Transform _center;
    private float _orbitRadius;
    private float _orbitSpeed;
    private float _angle;

    public void Initialize(Transform orbitCenter, float radius, float speed)
    {
        _center = orbitCenter;
        _orbitRadius = radius;
        _orbitSpeed = speed;
        _angle = Random.Range(0f, 360f);
    }

    public Vector3 GetNextPosition(float delta)
    {
        _angle += _orbitSpeed * delta;
        _angle %= 360f;

        var x = _center.position.x + Mathf.Cos(_angle * Mathf.Deg2Rad) * _orbitRadius;
        var z = _center.position.z + Mathf.Sin(_angle * Mathf.Deg2Rad) * _orbitRadius;

        return new Vector3(x, 0, z);
    }
}
