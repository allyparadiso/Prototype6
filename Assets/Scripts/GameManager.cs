using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public FirstPersonController playerController;

    public TMP_Text timerText;
    public TMP_Text scoreText;
    private float timeRemaining = 10f;
    private bool isTimerRunning = false;

    private float score = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        //playerController = FirstPersonController.Instance;
        isTimerRunning = true;
        score = 0;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                DisplayTime(timeRemaining);
            }
        }
        DisplayScore();
    }

    public void AddScore()
    {
        score++;
        DisplayScore();
    }

    void DisplayTime(float timeToDisplay)
    {
        timerText.text = timeToDisplay.ToString();
        
    }

    void DisplayScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
//timer
//score
//update ui