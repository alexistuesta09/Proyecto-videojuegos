using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeRemaining = 60f;
    public Text countdownText;

    private bool timerRunning = false;
    private float initialTime;
    private GameManager gameManager;

    private void Start()
    {
        initialTime = timeRemaining;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateCountdownText();

                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    gameManager.ShowGameOverPanel();
                }
            }
            else
            {
                timerRunning = false;
                gameManager.ShowGameOverPanel();
            }
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        timeRemaining = initialTime;
        UpdateCountdownText();
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", Mathf.Max(0, minutes), Mathf.Max(0, seconds));
    }
}
