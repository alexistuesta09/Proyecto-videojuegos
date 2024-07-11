using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject inicioPanel;
    public GameObject completadoPanel;
    public GameObject gameOverPanel;
    public TimerScript timerScript;
    public AudioSource mainAudioSource;
    public AudioSource audioclickOne;
    public GameObject llavePrefab; // Prefab de la llave que deseas instanciar
    public Transform llaveSpawnPoint; // Punto donde deseas que aparezca la llave

    private bool gameStarted = false;
    private BarraDeVida barraDeVida;
    private LuisMov playerMovement;

    private void Start()
    {
        InitializeGame();
        barraDeVida = FindObjectOfType<BarraDeVida>();
        playerMovement = FindObjectOfType<LuisMov>();
    }

    public void InitializeGame()
    {
        timerScript = FindObjectOfType<TimerScript>();
        mainAudioSource = Camera.main.GetComponent<AudioSource>();

        // Inicializar paneles
        inicioPanel.SetActive(true);
        completadoPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        inicioPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        completadoPanel.SetActive(false);
        Time.timeScale = 1;

        if (timerScript != null)
        {
            timerScript.StartTimer();
        }

        if (mainAudioSource != null && !mainAudioSource.isPlaying)
        {
            mainAudioSource.Play();
        }
    }

    public void RestartGame()
    {
        inicioPanel.SetActive(true);
        completadoPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        if (llavePrefab != null && llaveSpawnPoint != null)
        {
            Instantiate(llavePrefab, llaveSpawnPoint.position, Quaternion.identity);
        }

        if (timerScript != null)
        {
            timerScript.ResetTimer();
        }

        if (playerMovement != null)
        {
            playerMovement.ResetPlayerPosition();
        }
        else
        {
            Debug.LogWarning("No se pudo encontrar el objeto del jugador para reiniciar su posición.");
        }

        if (mainAudioSource != null && mainAudioSource.isPlaying)
        {
            mainAudioSource.Stop();
        }

        if (barraDeVida != null)
        {
            barraDeVida.RestaurarVida();
        }

        Time.timeScale = 0;
    }

    public void ShowInicioPanel()
    {
        Debug.Log("Mostrando panel de inicio desde GameManager.");

        completadoPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        inicioPanel.SetActive(true);

        if (timerScript != null)
        {
            timerScript.ResetTimer();
        }

        if (mainAudioSource != null)
        {
            mainAudioSource.Play();
        }

        gameStarted = false;
        Time.timeScale = 0;

        if (playerMovement != null)
        {
            playerMovement.ResetPlayerPosition();
        }

        if (barraDeVida != null)
        {
            barraDeVida.RestaurarVida();
        }
    }

    public void ShowCompletadoPanel()
    {
        Debug.Log("Mostrando panel Completado desde GameManager DESDE EL CÓDIGO.");

        completadoPanel.SetActive(true);

        if (timerScript != null)
        {
            timerScript.StopTimer();
            timerScript.StartTimer();
        }

        gameStarted = false;
        Time.timeScale = 0;

        StartCoroutine(FadeOutMusic());
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

        if (timerScript != null)
        {
            timerScript.StopTimer();
        }

        gameStarted = false;
        Time.timeScale = 0;

        if (playerMovement != null)
        {
            playerMovement.ResetPlayerPosition();
        }

        if (barraDeVida != null)
        {
            barraDeVida.RestaurarVida();
        }
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = mainAudioSource.volume;

        while (mainAudioSource.volume > 0)
        {
            mainAudioSource.volume -= startVolume * Time.deltaTime / 2;
            yield return null;
        }

        mainAudioSource.Stop();
        mainAudioSource.volume = startVolume;
    }

    public void PlayAudioClick()
    {
        if (audioclickOne != null && !audioclickOne.isPlaying)
        {
            audioclickOne.Play();
        }
    }
}
