using System.Collections.Generic;
using UnityEngine;

public class PlanetarySystem : IPlanetarySystem
{
    private Dictionary<Planet, OrbitMovement> _planetMovements = new Dictionary<Planet, OrbitMovement>();

    public IEnumerable<IPlanetaryObject> PlanetaryObjects => _planetMovements.Keys;

    public void AddPlanet(Planet planet, Transform systemCenter, float orbitalRadius, float orbitalSpeed)
    {
        var orbit = new OrbitMovement();
        orbit.Initialize(systemCenter, orbitalRadius, orbitalSpeed);
        _planetMovements.Add(planet, orbit);
    }

    public void UpdateSystem(float deltaTime)
    {
        // update position in planets
        foreach (KeyValuePair<Planet, OrbitMovement> item in _planetMovements)
        {
            var pos = item.Value.GetNextPosition(deltaTime);
            item.Key.transform.position = pos;
        }
    }
}
