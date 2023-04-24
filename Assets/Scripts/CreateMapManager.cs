using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateMapManager : MonoBehaviour
{
    private int thisFloor=3; //현재 층 
    [SerializeField] private float floorTerm = 10; //층 끼리의 간격

    [SerializeField] Vector3 firstPos;       //초기 위치
    [SerializeField] private int NullBottonSize = 3; // 빈공간 크기
                                                     // EX)
                                                     // 00 111111 00
                                                     // 000 1111 000 
                                                     // 0000 11 0000
                                                     // 이런 식

    [SerializeField] private int MaxX = 10;         //가로 크기
    [SerializeField] private int MaxZ = 52;         //세로 크기

    [SerializeField] private float Sell_HalfSizeX = 0.9f; //Cell의 넓이의 반
    [SerializeField] private float Sell_HalfSizeZ = 0.52f;  //Cell의 높이의 반
    public GameObject[] Cells;                                 //셀 객체
    public GameObject[] Floors;
    public GameObject FloorTrigger;

    private int CheckCell;      // Z축 으로 올라가면 지그 제그 형태니까 왼쪽으로 들어간 형태인지 아닌지 판단

    private void Awake()
    {

        for(int i = 0; i <= 3; i++)
        {
            MakeTile(firstPos);
            if (i != 3)
            {
                makeTriggerObj(firstPos);
            }
            firstPos += new Vector3(0, floorTerm, 0);
            thisFloor -= 1;
        }       
    }
    private void MakeTile(Vector3 FirstPos)
    {
        float XPos = 0; //X 위치
        int CheckBotton = NullBottonSize;//비어있는 공간 줄이기 위한 임시 변수
        bool ReturnNullCell = false; //위에 빈공간 확인용
        int countZCell = 0;         //높이 빈공간 확인용
        int cellnum = 0;

        for (int z = 0; z < MaxZ; z++)
        {
            if (z % 2 != 0)
            {
                XPos = -Sell_HalfSizeX;
                CheckCell = 0;
            }
            else
            {
                CheckCell = 1;
                XPos = 0;
            }

            for (int x = 0; x < MaxX; x++)
            {                
                if (CheckCell == 1 && countZCell ==10)
                {
                    if (((MaxX - CheckBotton) - CheckCell +1 < x) || (x < CheckBotton-1))
                    {
                        continue;
                    }
                }
                else if (((MaxX - CheckBotton) - CheckCell < x) || (x < CheckBotton))
                {
                    continue;
                }
                GameObject cells = Instantiate(Cells[thisFloor]);
                cells.transform.position = FirstPos + new Vector3(XPos + x * (Sell_HalfSizeX * 2), 0, z * Sell_HalfSizeZ);
                cells.name = thisFloor.ToString() + "_" + cellnum.ToString();
                cells.transform.SetParent(Floors[thisFloor].transform, true);
                cellnum++;
            }
            if (z % 2 != 0)
            {
                if ((CheckBotton == 0) && (ReturnNullCell == false))
                {
                    countZCell++;
                    if (countZCell >= NullBottonSize)
                    {
                        ReturnNullCell = true;
                    }
                    continue;
                }
                else
                {
                    CheckBotton = ReturnNullCell? CheckBotton+1: CheckBotton-1;
                }
            } 
        }
    }
    private void makeTriggerObj(Vector3 FirstPos)
    {
        //-10, -20, -30
        Vector3 FirstTrigerPos = FirstPos + new Vector3(0, floorTerm/5*4, 0);
        GameObject floorTrigger = Instantiate(FloorTrigger);
        floorTrigger.transform.position = FirstTrigerPos;
        floorTrigger.name = thisFloor.ToString();
        floorTrigger.transform.SetParent(this.transform, true);
    }
}
