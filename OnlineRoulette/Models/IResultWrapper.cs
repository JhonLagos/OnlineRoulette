namespace OnlineRoulette.Models
{
    public interface IResultWrapper
    {
        bool Error { get; set; }
        string Message { get; set; }
    }
}
