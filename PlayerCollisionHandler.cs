using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         Debug.Log("Colisión detectada con: " + collision.gameObject.name);

        if (collision.gameObject.name == "Llave")
    {
        if(gameManager == null)
        {
            Debug.LogError("GameManager no está asignado en PlayerCollisionHandler."); // Verifica si GameManager es null
        }
        else
        {
            Debug.Log("Llamando a ShowCompletadoPanel desde PlayerCollisionHandler."); // Depuración para verificar si se llama correctamente al método ShowCompletadoPanel
            gameManager.ShowCompletadoPanel();
        }
        
        Destroy(collision.gameObject); // Opcional: Destruir la llave después de recogerla
    }
    }
}
