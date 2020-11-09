using OnlineRoulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRoulette.Services
{
    public interface IRouletteService
    {
        Task<Roulette> CreateRouletteAsync();

        Task<IResultWrapper> OpenRouletteAsync(string rouletteId);

        Task<IResultWrapper> PlaceBetAsync(string rouletteId, Bet bet);

        Task<IResultGenericWrapper<ResultRoulette>> CloseRouletteAsync(string rouletteId);

        Task<IEnumerable<Roulette>> GetAllRoulettesAsync();
    }
}
