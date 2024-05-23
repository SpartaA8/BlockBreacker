[System.Serializable]
public class RankInfo
{
    public string Name;
    public int Score;
    
    public RankInfo(string name, int scroe)
    {        
        Name = name;
        Score = scroe;
    }
}