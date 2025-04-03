using System.Collections.Generic;

public interface IPlanetarySystem
{
    public IEnumerable<IPlanetaryObject> PlanetaryObjects { get; }
    public void UpdateSystem(float deltaTime);
}
