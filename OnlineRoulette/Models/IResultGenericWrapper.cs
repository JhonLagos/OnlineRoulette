namespace OnlineRoulette.Models
{
    public interface IResultGenericWrapper<T> : IResultWrapper
    {
        T Result { get; set; }
    }
}
