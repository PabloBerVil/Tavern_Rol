using UnityEngine;

public class S_CameraBehaviour : MonoBehaviour
{
    [SerializeField] public Transform player; // arrastra aquí el objeto del jugador en el Inspector

    void LateUpdate()
    {
        if (player == null) return;

        // Dirección completa hacia el jugador, sin eliminar componentes
        Vector3 lookDirection = player.position - transform.position;

        if (lookDirection != Vector3.zero)
        {
            // Crea una rotación para que la cámara mire al jugador
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
        }
    }
}
