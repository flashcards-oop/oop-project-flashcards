namespace FlashcardsApi.Models
{
    public class TestDto
    {
        public string CollectionId { get; }
        public int OpenCnt { get; }
        public int ChoiceCnt { get; }
        public int MatchCnt { get; }
        
        public TestDto(string collectionId, int openCnt, int choiceCnt, int matchCnt)
        {
            CollectionId = collectionId;
            OpenCnt = openCnt;
            ChoiceCnt = choiceCnt;
            MatchCnt = matchCnt;
        }
    }
}