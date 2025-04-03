using UnityEngine;

public class PlanetSelector : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private CameraMovement _cameraMovement;
    private bool _isPlanetSelected;

    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    private void Update()
    {
        if (_isPlanetSelected)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                UnSelectPlanet();

            return;
        }

        if (Input.GetMouseButtonDown(0))
            RayCast();
    }
    private void RayCast()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var isHit = Physics.Raycast(ray, out RaycastHit hit);
        if (isHit)
        {
            var planetRef = hit.collider.GetComponent<PlanetReference>();
            if (planetRef == null)
                return;

            SelectPlanet(planetRef.planet);
        }
    }

    private void SelectPlanet(Planet planet)
    {
        // disable script and wait for camera moving process to end
        // after that enable the script
        enabled = false;
        _isPlanetSelected = true;
        _cameraMovement.AttachToPlanet(planet, () => { enabled = true; });
    }

    private void UnSelectPlanet()
    {
        // disable script and wait for camera moving process to end
        // after that enable the script
        enabled = false;
        _isPlanetSelected = false;
        _cameraMovement.ResetCamera(() => { enabled = true; });
    }
}
