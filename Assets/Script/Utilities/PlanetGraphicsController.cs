using UnityEngine;

public class PlanetGraphicsController : MonoBehaviour
{
    [SerializeField]
    private Planet _planet;
    [SerializeField]
    private PlanetData _planetData;

    private void Start()
    {
        var planetType = _planet.MassClass;
        Instantiate(_planetData.GetPlanetModelPrefab(planetType), transform);
    }
}
