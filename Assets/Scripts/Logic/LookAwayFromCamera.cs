using UnityEngine;

public class LookAwayFromCamera : MonoBehaviour
{
    private void Update()
    {
        Vector3 directionToCamera = (Camera.main.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(-directionToCamera);
    }
}