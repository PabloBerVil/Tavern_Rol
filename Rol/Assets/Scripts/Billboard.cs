using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // Apunta hacia la cámara sin rotarse verticalmente
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0; // para que no se incline hacia arriba/abajo
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}
