using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    public int PlayerFloor = 0;
    
    [SerializeField] private GameObject[] floorsSet;
    [SerializeField] private int NumForErase = 15;
    [SerializeField] private Material mat;
    [SerializeField] private CreateMapManager createMapManager;

    public int max=320;
    //List<int> randomList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelectTile());
        //Debug.Log(SelectTile(maxTileNum)); //함수실행
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SelectTile()
    {
        /*
         * 플레이어 스크립트에 현재 층 정보 변수 생성
         * playerFloor 와 플레이어 층이 다르면
         * playerFloor 업데이트 해주고
         * max = 320 으로 초기화
         */
        GameObject tile;

        while (true)
        {
            List<int> randomList = new List<int>();
            Debug.Log("Start of Coroutine");

            Debug.Log(max);
            int nowPlayerFloor = PlayerFloor;
            // (남은 타일 개수 < 지워야 하는 타일 개수) 라면 전부 삭제 후 층 내려감
            if (max <= NumForErase)
            {
                Transform[] childList = floorsSet[nowPlayerFloor].GetComponentsInChildren<Transform>();

                if (childList != null)
                {
                    for (int i = 1; i < childList.Length; i++)
                    {
                        if (childList[i] != transform)
                            childList[i].gameObject.GetComponent<MeshRenderer>().material = mat;
                            Destroy(childList[i].gameObject, 1f);
                    }
                }
                
                if (nowPlayerFloor == 3)
                {
                    //코루틴종료
                    StopCoroutine(SelectTile());
                }
                else
                {
                    //nowPlayerFloor++;
                    PlayerFloor++;
                    max = 320;
                }
            }
            else
            {
                //타일 랜덤 삭제
                for (int i = 0; i < NumForErase;)
                {
                    int currentNumber = Random.Range(0, max); //0~319
                    if (randomList.Contains(currentNumber)) //랜덤뽑은 숫자가 중복이면
                    {
                        currentNumber = Random.Range(0, max); //다시 뽑기
                    }
                    else
                    {
                        randomList.Add(currentNumber);
                        i++;
                    }
                }

                for (int i = 0; i < randomList.Count; i++)
                {
                    tile = floorsSet[nowPlayerFloor].transform.GetChild(randomList[i]).gameObject;
                    tile.GetComponent<MeshRenderer>().material = mat;

                    Destroy(tile, 1f);
                }
                max -= NumForErase;
                Debug.Log(max);
            }
            yield return new WaitForSeconds(3f);
            Debug.Log("End of Coroutine");

        }
        
    }
 

}

//현재 캐릭터 몇층인지?
//타일 없애기 전에 플레이어 층 확인하고 그 층의 타일 삭제
//플레이어가 층을 내려가거나 타일 개수가 전부 사라질때까지


//인덱스배열에서 랜덤 10개 뽑기: Random.Range(0,320) 10번
//GetChild 함수로 삭제:
//남은 갯수중에서 랜덤 10개 뽑기
//GetChild 함수로 삭제
//반복
