using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private int width = 10;            //그리드 가로 크기
    [SerializeField] private int height = 10;           //그리드 세로 크기
    [SerializeField] private float cellSize = 1;        //각 샐의 크기

    private Grid grid;                                  //그리드 선언 후 받아온다. 
    void Start()
    {
        
    }

   
}
