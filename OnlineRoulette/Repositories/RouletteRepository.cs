using Newtonsoft.Json;
using OnlineRoulette.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRoulette.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private readonly IOnlineRouletteContext _context;
        private readonly string _keyHashRoulettes = "Roulettes";

        public RouletteRepository(IOnlineRouletteContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SaveRouletteAsync(Roulette roulette)
        {
            await _context.Redis.HashSetAsync(_keyHashRoulettes, roulette.Id, JsonConvert.SerializeObject(roulette));
        }

        public async Task<Roulette> GetRouletteByIdAsync(string rouletteId)
        {
            RedisValue value = await _context.Redis.HashGetAsync(_keyHashRoulettes, rouletteId);
            if (string.IsNullOrEmpty(value)) 
                return null;
            return JsonConvert.DeserializeObject<Roulette>(value);
        }

        public async Task<IEnumerable<Roulette>> GetAllRoulettesAsync()
        {
            HashEntry[] entries = await _context.Redis.HashGetAllAsync(_keyHashRoulettes);
            if (entries == null)
                return new List<Roulette>();
            return entries
                .Select(e => JsonConvert.DeserializeObject<Roulette>(e.Value))
                .ToList();
        }
    }
}
