using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 3, -6);

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
