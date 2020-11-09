namespace OnlineRoulette.Models
{
    public class ResultGenericWrapper<T> : IResultGenericWrapper<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public ResultGenericWrapper(bool error, string message)
        {
            Error = error;
            Message = message;
        }

        public ResultGenericWrapper(bool error, string message, T result)
        {
            Error = error;
            Message = message;
            Result = result;
        }
    }
}