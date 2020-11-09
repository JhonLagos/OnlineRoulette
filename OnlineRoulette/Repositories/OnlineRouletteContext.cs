using StackExchange.Redis;

namespace OnlineRoulette.Repositories
{
    public class OnlineRouletteContext : IOnlineRouletteContext
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public OnlineRouletteContext(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}
