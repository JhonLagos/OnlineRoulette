using StackExchange.Redis;

namespace OnlineRoulette.Repositories
{
    public interface IOnlineRouletteContext
    {
        IDatabase Redis { get; }
    }
}
