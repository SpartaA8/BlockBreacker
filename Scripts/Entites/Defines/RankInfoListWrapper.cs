using System.Collections.Generic;
public partial class RankBoardManager
{
    [System.Serializable]
    public class RankInfoListWrapper
    {
        public List<RankInfo> rankings;
        public RankInfoListWrapper(List<RankInfo> rankings)
        {
            this.rankings = rankings;
        }
    }
}









