using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    private float startTime;
    private float elapsedTime;
    public bool isStart;
    [SerializeField] private float StartTime = 5.5f;

    private void Start()
    {
        Invoke("StartTimer", StartTime);
        startTime = Time.time;
        elapsedTime = 0f;
    }

    private void FixedUpdate()
    {
        if (isStart)
        {
            elapsedTime += Time.fixedDeltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    private void StartTimer()
    {
        isStart = true;
    }

}