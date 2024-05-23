using UnityEngine;

public class Stage //2차원 배열을 사용하기위해서 StageSO대신 stage클래스를 사용
{
    private int[,] map;
    public Stage(int w, int h) //너비(w)와 높이(h)를 사용하여 맵 초기화
    {
        map = new int[w, h];
    }

    public int[,] SetMap() //맵 데이터 반환
    {
        return map;
    }
}
