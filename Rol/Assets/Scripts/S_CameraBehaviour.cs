using UnityEngine;

public class S_CameraBehaviour : MonoBehaviour
{
    [SerializeField] public Transform player; // arrastra aqu� el objeto del jugador en el Inspector

    void LateUpdate()
    {
        if (player == null) return;

        // Direcci�n completa hacia el jugador, sin eliminar componentes
        Vector3 lookDirection = player.position - transform.position;

        if (lookDirection != Vector3.zero)
        {
            // Crea una rotaci�n para que la c�mara mire al jugador
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
        }
    }
}
