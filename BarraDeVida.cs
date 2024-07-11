using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image vida;
    public float vidaActual = 100;
    public float vidaMaxima = 100;

    private GameManager gameManager;

    private void Start()
    {
        vida.fillAmount = vidaActual / vidaMaxima;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        vida.fillAmount = vidaActual / vidaMaxima;
    }

    public void ReducirVida(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        if (vidaActual <= 0)
        {
            gameManager.ShowGameOverPanel();
            RestaurarVida();
        }
    }

    public void RestaurarVida()
    {
        vidaActual = vidaMaxima;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cactus"))
        {
            ReducirVida(20);
        }
    }
}
