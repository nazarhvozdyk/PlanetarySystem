using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _angles;

    private void Update()
    {
        transform.localEulerAngles += _angles * Time.deltaTime;
    }
}
