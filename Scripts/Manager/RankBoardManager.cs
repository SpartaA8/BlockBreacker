using System.Collections.Generic;
using System.IO;
using UnityEngine;
public partial class RankBoardManager : MonoBehaviour
{
    public static RankBoardManager Instance;
    private List<RankInfo> rankList = new List<RankInfo>();

    private int maxRankNum = 10;
    private const string SaveFileName = "/Json/Ranking.json";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadRankings();
        }
    }
    private void SaveRankings()
    {
        string json = JsonUtility.ToJson(new RankInfoListWrapper(rankList));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, SaveFileName), json);
    }
    private void LoadRankings()
    {
        string path = Application.dataPath + SaveFileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            RankInfoListWrapper wrapper = JsonUtility.FromJson<RankInfoListWrapper>(json);
            rankList = wrapper.rankings;
        }
        else
        {
            rankList = new List<RankInfo>();
        }
    }
    public void AddNewScore(string playerName, int score)
    {
        RankInfo newRank = new RankInfo(playerName, score);
        rankList.Add(newRank);
        rankList.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (rankList.Count > maxRankNum)
        {
            rankList.RemoveAt(rankList.Count - 1);
        }
        SaveRankings();
    }

    public int GetHighScore()
    {
        return rankList[0].Score;        
    }

    public List<RankInfo> GetRankList()
    {
        return rankList;
    }
}









