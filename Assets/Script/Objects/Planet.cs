using UnityEngine;

public class Planet : MonoBehaviour, IPlanetaryObject
{
    private double _mass;
    [SerializeField]
    private PlanetData _planetData;

    public double Mass => _mass;

    public MassClassEnum MassClass => _planetData.GetMassClass(_mass);

    public void Initialize(double newMass)
    {
        _mass = newMass;

        var radius = _planetData.GetRadius(_mass);
        transform.localScale = Vector3.one * radius;
    }
}
