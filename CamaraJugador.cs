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
            // Calcula la posici�n objetivo de la c�mara con el offset
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z; // Mant�n la misma profundidad de la c�mara

            // Suaviza el movimiento de la c�mara utilizando Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
