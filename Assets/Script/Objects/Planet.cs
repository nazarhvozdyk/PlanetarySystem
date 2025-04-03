using UnityEngine;

public class Planet : MonoBehaviour, IPlanetaryObject
{
    private double _mass;
    [SerializeField]
    private PlanetData _planetData;
    public float radious { get; private set; }

    public double Mass => _mass;

    public MassClassEnum MassClass => _planetData.GetMassClass(_mass);

    public void Initialize(double newMass)
    {
        _mass = newMass;
        radious = _planetData.GetRadius(newMass);
    }
}
