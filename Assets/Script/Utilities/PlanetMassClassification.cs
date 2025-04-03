using UnityEngine;

public class PlanetMassClassification : MonoBehaviour
{
    public static PlanetMassClassification Instance
    {
        get => _instance;
    }
    private static PlanetMassClassification _instance;

    // 1 earth radious in game
    [SerializeField]
    private float _earthRadius = 5f;
    // earth mass = 1000 kg in game
    [SerializeField]
    private float _earthMassInGame = 1000f;

    private void Awake()
    {
        _instance = this;
    }

    public float ConvertRadiousToGameUnits(float realRadius)
    {
        return realRadius * (_earthRadius / 1f);
    }

    public float ConvertToGameMass(double realMass)
    {
        return (float)(realMass * _earthMassInGame);
    }
}
