using OnlineRoulette.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRoulette.Repositories
{
    public interface IRouletteRepository
    {
        Task SaveRouletteAsync(Roulette roulette);

        Task<Roulette> GetRouletteByIdAsync(string rouletteId);

        Task<IEnumerable<Roulette>> GetAllRoulettesAsync();
    }
}
