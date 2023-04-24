using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] countdownImg;
    public GameTimer gameTimer;
    public GameObject startTile, DestroyTile;
    public DestroyTile destroyTile;
    public bool isGameOver=false;
    public int min, sec;

    void Start()
    {
        //시작시 카운트 다운 초기화, 게임 시작 false 설정
        StartCoroutine(Countdown());
        gameTimer = this.GetComponent<GameTimer>();
        destroyTile = DestroyTile.GetComponent<DestroyTile>();
    }

    void Update()
    {
        if (isGameOver)
        {
            destroyTile.isGameOver = true;
            Debug.Log(min);
            Debug.Log(sec);
        }
    }

    IEnumerator Countdown()
    {
        countdownImg[0].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        countdownImg[0].SetActive(false);
        countdownImg[1].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        countdownImg[1].SetActive(false);
        countdownImg[2].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        countdownImg[2].SetActive(false);
        countdownImg[3].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        countdownImg[3].SetActive(false);
        startTile.SetActive(false);
        gameTimer.isStart = true;
        Time.timeScale = 1.0f;
        
    }

}