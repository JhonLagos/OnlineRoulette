namespace OnlineRoulette.Models
{
    public class ResultWrapper : IResultWrapper
    {
        public bool Error { get; set; }
        public string Message { get; set; }

        public ResultWrapper(bool error)
        {
            Error = error;
        }

        public ResultWrapper(bool error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}
