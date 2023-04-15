using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateMapManager : MonoBehaviour
{
    private int thisFloor=0; //현재 층 
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

    private int CheckCell;      // Z축 으로 올라가면 지그 제그 형태니까 왼쪽으로 들어간 형태인지 아닌지 판단

    private void Awake()
    {

        for(int i = 0; i <= 3; i++)
        {
            MakeTile(firstPos);
            firstPos += new Vector3(0, floorTerm, 0);
            thisFloor += 1;
        }
        
    }

    private void MakeTile(Vector3 FirstPos)
    {
        float XPos = 0; //X 위치
        int CheckBotton = NullBottonSize;//비어있는 공간 줄이기 위한 임시 변수
        bool ReturnNullCell = false; //위에 빈공간 확인용
        int countZCell = 0;         //높이 빈공간 확인용

        //사각형으로 올라감
        for (int z = 0; z < MaxZ; z++)
        {
            //X위치 잡기
            //짝수 혹은 홀수 일 때 헥사타일 특성상 한쪽이 튀어 나오니
            //그 나온 위치 잡아줌
            if (z % 2 != 0) //홀
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
                //비어 있어야 하는 부분 (위에 0으로 되어있는 부분을 뜻함) 이면 패스
                else if (((MaxX - CheckBotton) - CheckCell < x) || (x < CheckBotton))
                {
                    continue;
                }
                // CheckBotton <= x <= MaxX - CheckBotton - CheckCell
                //생성
                GameObject cells = Instantiate(Cells[thisFloor]);
                //위치 설정
                cells.transform.position = FirstPos + new Vector3(XPos + x * (Sell_HalfSizeX * 2), 0, z * Sell_HalfSizeZ);
                cells.name = z.ToString() + "_" + x.ToString() + "_";
                //Debug.Log(z.ToString() + "_" + x.ToString() + "_" + (MaxX - CheckBotton - CheckCell)+"_"+CheckBotton + "_" + CheckCell +"_"+ countZCell);
            }
            //나온 부분일 경우
            if (z % 2 != 0)
            {
                //빈공간 -1 (역 피라미드 느낌으로 따른 알고리즘 써도 됨 어디까지나 임시로 한거)
                //만약 빈공간이 없어도 되는 부분이면
                if ((CheckBotton == 0) && (ReturnNullCell == false))
                {
                    //꽉차있는 부분이 총 몇칸인지 체크해줌 (여기선 임시로 NULLBottonSize로 해줬음 다른 변수 넣어줘도 무방)
                    countZCell++;
                    if (countZCell >= NullBottonSize)
                    {
                        ReturnNullCell = true;
                    }
                    continue;
                }
                else
                {
                    //여기선 역피라미드 형태인지 피라미드 형태로 가야하는지 판단해줘서
                    //그에 맞게 변화
                    if (ReturnNullCell)
                    {
                        CheckBotton++;
                    }
                    else
                    {
                        CheckBotton--;
                    }
                }
            } 

        }
    }
}
