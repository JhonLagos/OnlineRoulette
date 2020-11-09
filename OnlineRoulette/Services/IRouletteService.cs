using OnlineRoulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRoulette.Services
{
    public interface IRouletteService
    {
        Task<Roulette> CreateRouletteAsync();

        Task<ResultWrapper> OpenRouletteAsync(string rouletteId);

        Task<IEnumerable<Roulette>> GetAllRoulettesAsync();
    }
}
