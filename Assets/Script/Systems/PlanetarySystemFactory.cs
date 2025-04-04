using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
{
    [SerializeField] private PlanetData _planetData;
    [SerializeField] private Planet _planetPrefab;
    [SerializeField] private Transform _systemCenter;
    [Space()]
    [SerializeField] private int _planetCount = 5;
    [Space()]
    [SerializeField] private float _minOrbitRadius = 2f;
    [SerializeField] private float _maxOrbitRadius = 10f;
    [Space()]
    [SerializeField] private float _minOrbitSpeed = 5f;
    [SerializeField] private float _maxOrbitSpeed = 20f;
    [Space()]
    [SerializeField] private float _baseOrbitalDistance = 20f;
    [SerializeField] private float _orbitalSpeedFactor = 2f;

    private void Start()
    {
        if (_systemCenter == null)
            _systemCenter = new GameObject("Planetary Center").transform;
    }

    private IPlanetarySystem GeneratePlanets(double[] masses)
    {
        var planetSystem = new PlanetarySystem();

        for (int i = 0; i < masses.Length; i++)
        {
            var planet = CreatePlanet(masses[i]);

            // movement setup
            var orbitalRadius = _baseOrbitalDistance * (i + 1);
            var orbitalSpeed = _orbitalSpeedFactor / (i + 1);

            planetSystem.AddPlanet(planet, _systemCenter, orbitalRadius, orbitalSpeed);
        }

        return planetSystem;
    }

    private Planet CreatePlanet(double mass)
    {
        // values
        var radius = _planetData.GetRadius(mass);

        // creating planet
        var planetObj = Instantiate(_planetPrefab, _systemCenter.position, Quaternion.identity);
        planetObj.Initialize(mass);

        return planetObj;
    }

    public IPlanetarySystem Create(double totalMass)
    {
        var masses = GenerateMassDistribution(_planetCount, totalMass);
        var planetarySystem = GeneratePlanets(masses);

        var sum = 0d;
        foreach (var item in masses)
            sum += item;

        return planetarySystem;
    }

    private double[] GenerateMassDistribution(int count, double totalMass)
    {
        var minMass = _planetData.GetMinimumMass();
        var maxMass = totalMass * 0.6;

        var masses = new double[count];
        var sumMass = 0d;

        for (int i = 0; i < count; i++)
        {
            masses[i] = minMass * Mathf.Pow((float)(maxMass / minMass), Mathf.Pow(Random.Range(0f, 1f), 2f));
            sumMass += masses[i];
        }

        // normalize mass so they sum euqals totalMass
        for (int i = 0; i < count; i++)
            masses[i] = masses[i] / sumMass * totalMass;

        return masses;
    }
}
