using UnityEngine;

public class PlanetarySystemManager : MonoBehaviour
{
    [SerializeField]
    private double _totalMasse = 100f;
    [SerializeField]
    private PlanetarySystemFactory _factory;
    private IPlanetarySystem _system;

    private void Start()
    {
        enabled = false;
    }

    [ContextMenu("Create")]
    private void Create()
    {
        _system = _factory.Create(_totalMasse);
        enabled = true;
    }

    private void Update()
    {
        _system.UpdateSystem(Time.deltaTime);
    }
}
