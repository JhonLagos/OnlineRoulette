namespace OnlineRoulette.Models
{
    public class ResultWrapper
    {
        public bool Error { get; set; }

        public ResultWrapper(bool error)
        {
            Error = error;
        }
    }
}
