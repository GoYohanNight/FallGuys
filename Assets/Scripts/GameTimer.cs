using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    private float startTime;
    private float elapsedTime;
    public bool isStart;
    public bool isGameOver = false;
    public GameObject gameManager;
    public GameManager gameManagerScript;
    private int minutes, seconds;
    [SerializeField] private float StartTime = 5.5f;

    private void Start()
    {
        Invoke("StartTimer", StartTime);
        startTime = Time.time;
        elapsedTime = 0f;
    }

    private void FixedUpdate()
    {
        if (isStart && !isGameOver)
        {
            elapsedTime += Time.fixedDeltaTime;
            minutes = Mathf.FloorToInt(elapsedTime / 60f);
            seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (isGameOver)
        {
            isStart = false;
            gameManagerScript = gameManager.GetComponent<GameManager>();
            gameManagerScript.min = minutes;
            gameManagerScript.sec = seconds;
            gameManagerScript.isGameOver = true;
        }

    }

    private void StartTimer()
    {
        isStart = true;

    }

}