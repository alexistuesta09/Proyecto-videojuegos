using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado del seguimiento
    public Vector3 offset = new Vector3(-5f, 0f, 0f); // Offset para mantener al jugador ligeramente a la izquierda

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición objetivo de la cámara con el offset
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Mantén la misma profundidad de la cámara

            // Suaviza el movimiento de la cámara utilizando Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
