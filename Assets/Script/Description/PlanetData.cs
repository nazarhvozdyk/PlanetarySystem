using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "ScriptableObjects/PlanetData", order = 1)]
public class PlanetData : ScriptableObject
{
    [System.Serializable]
    public class PlanetType
    {
        public MassClassEnum massClass;
        public double MinMass;
        public double MaxMass;
        public float MinRadius;
        public float MaxRadius;
        public GameObject modelPrefab;
    }

    public PlanetType[] planetTypes;

    public MassClassEnum GetMassClass(double mass)
    {
        foreach (var type in planetTypes)
        {
            if (mass >= type.MinMass && mass < type.MaxMass)
                return type.massClass;
        }

        return MassClassEnum.None;
    }

    public float GetRadius(double mass)
    {
        foreach (var type in planetTypes)
        {
            if (mass >= type.MinMass && mass < type.MaxMass)
            {
                var radius = Random.Range(type.MinRadius, type.MaxRadius);
                var value = PlanetMassClassification.Instance.ConvertRadiousToGameUnits(radius);
                return value;
            }
        }

        var defaultValue = PlanetMassClassification.Instance.ConvertRadiousToGameUnits(1f);

        return defaultValue;
    }

    public GameObject GetPlanetModelPrefab(MassClassEnum massClass)
    {
        foreach (var item in planetTypes)
            if (item.massClass == massClass)
                return item.modelPrefab;

        return null;
    }
}
