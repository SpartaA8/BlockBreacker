using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager : MonoBehaviour
{
    private static StageDataManager Instance;
    private Dictionary<int, int[,]> StageDictionary;
    private int[,] stage1;
    private int[,] stage2;
    private int[,] stage3;
    private int[,] stage4;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        StageDictionary = new Dictionary<int, int[,]>();
        SetStageDictionary();
    }

    private void SetStageDictionary() //스테이지 데이터 설정
    {
        stage1 = new int[,]{
            { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1 },
            { 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2 },
            { 3, 4, 5, 1, 2, 3, 4, 5, 1, 2, 3 },
            { 4, 5, 1, 2, 3, 4, 5, 1, 2, 3, 4 },
            { 5, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 },
            { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 1 },
            { 2, 3, 4, 5, 1, 2, 3, 4, 5, 1, 2 }};
        StageDictionary.Add(1, stage1);

        stage2 = new int[,]{
            { 1, 0, 0, 0, 0, 6, 1, 2, 3, 4, 5 },
            { 1, 2, 0, 0, 0, 0, 6, 2, 3, 4, 5 },
            { 1, 2, 3, 0, 0, 0, 0, 6, 3, 4, 5 },
            { 1, 2, 3, 4, 0, 0, 0, 0, 6, 4, 5 },
            { 1, 2, 3, 4, 5, 0, 0, 0, 0, 6, 5 },
            { 1, 2, 3, 4, 5, 1, 0, 0, 0, 0, 6 },
            { 1, 2, 3, 4, 5, 1, 2, 0, 0, 0, 0 },
            { 1, 2, 3, 4, 5, 1, 2, 3, 0, 0, 0 },
            { 1, 2, 3, 4, 5, 1, 2, 3, 4, 0, 0 },
            { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 0 },
            { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 1 }};
        StageDictionary.Add(2, stage2);

        stage3 = new int[,]{
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 6, 6, 6, 6, 7, 7, 7, 6, 6, 6, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0 },
            { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 0 }};
        StageDictionary.Add(3, stage3);

        stage4 = new int[,]{
            { 0, 1, 2, 7, 3, 0, 6, 5, 1, 7, 0 },
            { 0, 2, 7, 3, 4, 0, 5, 1, 7, 2, 0 },
            { 0, 7, 3, 4, 6, 0, 1, 7, 2, 3, 0 },
            { 0, 3, 4, 6, 5, 0, 7, 2, 3, 6, 0 },
            { 0, 4, 6, 5, 1, 0, 2, 3, 6, 4, 0 },
            { 0, 6, 5, 1, 7, 0, 3, 6, 4, 5, 0 },
            { 0, 5, 1, 7, 2, 0, 6, 4, 5, 7, 0 },
            { 0, 1, 7, 2, 3, 0, 4, 5, 7, 1, 0 },
            { 0, 7, 2, 3, 6, 0, 5, 7, 1, 2, 0 },
            { 0, 2, 3, 6, 4, 0, 7, 1, 2, 6, 0 },
            { 0, 3, 6, 4, 5, 0, 1, 2, 6, 3, 0 },
            { 0, 6, 4, 5, 7, 0, 2, 6, 3, 4, 0 },
            { 0, 4, 5, 7, 1, 0, 6, 3, 4, 7, 0 }};
        StageDictionary.Add(4, stage4);
    }


    public static StageDataManager GetInstance()
    {
        return Instance;
    }

    public int[,] GetStageMaps(int stageNum) //스테이지 번호에 해당하는 맵 데이터를 반환
    {
        return StageDictionary[stageNum];
    }
}
