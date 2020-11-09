using OnlineRoulette.Models;
using OnlineRoulette.Models.Enums;
using OnlineRoulette.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRoulette.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IRouletteRepository _repository;

        public RouletteService(IRouletteRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Roulette> CreateRouletteAsync()
        {
            Roulette roulette = new Roulette(id: GenerateRouletteId(), status: RouletteStatus.Closed);
            await _repository.AddOrUpdateRouletteAsync(roulette: roulette);
            return roulette;
        }

        public async Task<ResultWrapper> OpenRouletteAsync(string rouletteId)
        {
            Roulette roulette = await _repository.GetRouletteByIdAsync(rouletteId: rouletteId);
            if (roulette != null)
            {
                roulette.Status = RouletteStatus.Open;
                await _repository.AddOrUpdateRouletteAsync(roulette: roulette);
                return new ResultWrapper(error: false);
            }
            return new ResultWrapper(error: true);
        }

        public async Task<IEnumerable<Roulette>> GetAllRoulettesAsync()
        {
            return await _repository.GetAllRoulettesAsync();
        }

        private string GenerateRouletteId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
