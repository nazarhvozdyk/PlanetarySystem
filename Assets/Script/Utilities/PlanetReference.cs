using UnityEngine;

public class PlanetReference : MonoBehaviour
{
    [SerializeField]
    private Planet _planet;
    public Planet planet { get => _planet; }

    private void Awake()
    {
        if (_planet == null)
            _planet = GetComponentInParent<Planet>();
    }
}
