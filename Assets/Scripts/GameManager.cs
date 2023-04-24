using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int Timer = 0;
    [SerializeField] private GameObject[] countdownImg;

    void Start()
    {
        //시작시 카운트 다운 초기화, 게임 시작 false 설정
        Timer = 0;
    }

    void Update()
    {
        //게임 시작시 정지
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }
        //Timer 가 90보다 작거나 같을경우 Timer 계속증가
        if (Timer <= 400)
        {
            Timer++;

            // Timer가 60보다 작을경우 3 켜기
            if (Timer < 100)
            {
                countdownImg[0].SetActive(true);
            }
            // Timer가 60보다 클경우 2켜기
            if (Timer > 120)
            {
                countdownImg[0].SetActive(false);
                countdownImg[1].SetActive(true);
            }
            // Timer가 90보다 작을경우 1켜기
            if (Timer > 300)
            {
                countdownImg[1].SetActive(false);
                countdownImg[2].SetActive(true);
            }
            // Timer 가 120보다 클경우 게임시작
            if (Timer > 400)
            {
                countdownImg[2].SetActive(false);
                countdownImg[3].SetActive(true);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //게임시작
            }
        }
    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        countdownImg[3].SetActive(false);
    }
}

