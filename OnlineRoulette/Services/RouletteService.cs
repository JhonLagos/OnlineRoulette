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
            Roulette roulette = new Roulette(id: GenerateRouletteId(), status: StatusRoulette.Closed);
            await _repository.SaveRouletteAsync(roulette: roulette);
            return roulette;
        }

        public async Task<IResultWrapper> OpenRouletteAsync(string rouletteId)
        {
            Roulette roulette = await _repository.GetRouletteByIdAsync(rouletteId: rouletteId);
            if (roulette != null)
            {
                UpdateStatusRouletteAsync(roulette: roulette, status: StatusRoulette.Open);
                return new ResultWrapper(error: false);
            }
            return new ResultWrapper(error: true);
        }

        public async Task<IResultWrapper> PlaceBetAsync(string rouletteId, Bet bet)
        {
            Roulette roulette = await _repository.GetRouletteByIdAsync(rouletteId: rouletteId);
            IResultWrapper isRouletteValid = IsRouletteValidToPlaceBet(roulette: roulette);
            if (isRouletteValid.Error)
                return isRouletteValid;
            IResultWrapper isBetValid = IsValidBet(bet: bet);
            if (isBetValid.Error)
                return isBetValid;
            AddBetToRoulette(roulette, bet);
            return new ResultWrapper(error: false);
        }

        public async Task<IResultGenericWrapper<ResultRoulette>> CloseRouletteAsync(string rouletteId)
        {
            Roulette roulette = await _repository.GetRouletteByIdAsync(rouletteId: rouletteId);
            IResultWrapper isRouletteValid = IsRouletteValidToClose(roulette);
            if (isRouletteValid.Error)
                return new ResultGenericWrapper<ResultRoulette>(error: isRouletteValid.Error, message: isRouletteValid.Message);
            UpdateStatusRouletteAsync(roulette: roulette, status: StatusRoulette.Closed);
            return GetResultBetsToRoulette(roulette);
        }

        public async Task<IEnumerable<Roulette>> GetAllRoulettesAsync()
        {
            return await _repository.GetAllRoulettesAsync();
        }

        private string GenerateRouletteId()
        {
            return Guid.NewGuid().ToString();
        }

        private async void UpdateStatusRouletteAsync(Roulette roulette, StatusRoulette status)
        {
            roulette.Status = status;
            await _repository.SaveRouletteAsync(roulette: roulette);
        }

        private IResultWrapper IsRouletteValidToPlaceBet(Roulette roulette)
        {
            if (roulette == null)
                return new ResultWrapper(error: true, message: $"La ruleta {roulette.Id} no existe");
            if (roulette.Status != StatusRoulette.Open)
                return new ResultWrapper(error: true, message: $"La ruleta {roulette.Id} no está abierta");
            return new ResultWrapper(error: false);
        }

        private IResultWrapper IsValidBet(Bet bet)
        {
            if (bet.Type == TypeBet.Color && (!"negro".Equals(bet.Color.ToLower()) && !"rojo".Equals(bet.Color.ToLower())))
                return new ResultWrapper(error: true, message: $"Los colores válidos para apostar son el negro o rojo");
            if (bet.Type == TypeBet.Number && (bet.Number > 36 || bet.Number < 0))
                return new ResultWrapper(error: true, message: $"Los números válidos para apostar son del 0 al 36");
            if (bet.Amount > 10000)
                return new ResultWrapper(error: true, message: $"La cantidad máxima para apostar es de 10.000 dólares");
            return new ResultWrapper(error: false);
        }

        private async void AddBetToRoulette(Roulette roulette, Bet bet)
        {
            if (roulette.Bets == null)
                roulette.Bets = new List<Bet>();
            roulette.Bets.Add(bet);
            await _repository.SaveRouletteAsync(roulette: roulette);
        }

        private IResultWrapper IsRouletteValidToClose(Roulette roulette)
        {
            if (roulette == null)
                return new ResultWrapper(error: true, message: $"La ruleta {roulette.Id} no existe");
            if (roulette.Status == StatusRoulette.Closed)
                return new ResultWrapper(error: true, message: $"La ruleta {roulette.Id} ya esta cerrada");
            return new ResultWrapper(error: false);
        }

        private IResultGenericWrapper<ResultRoulette> GetResultBetsToRoulette(Roulette roulette)
        {
            ResultRoulette resultRoulette = GetResultRoulette(roulette);
            resultRoulette.Bets = GetResultBets(roulette.Bets, resultRoulette);
            return new ResultGenericWrapper<ResultRoulette>(error: false, message: "", result: resultRoulette);
        }

        private ResultRoulette GetResultRoulette(Roulette roulette)
        {
            ResultRoulette resultRoulette = new ResultRoulette();
            resultRoulette.WinningNumber = GetWinningNumberRoulette();
            resultRoulette.WinningColor = GetWinningColorRoulette(resultRoulette.WinningNumber);
            return resultRoulette;
        }

        private int GetWinningNumberRoulette()
        {
            Random rnd = new Random();
            return rnd.Next(37);
        }

        private string GetWinningColorRoulette(int number)
        {
            return (number % 2 == 0) ? "rojo" : "negro";
        }

        private List<Bet> GetResultBets(List<Bet> bets, ResultRoulette resultRoulette)
        {
            foreach (var bet in bets)
            {
                bet.EarnedMoney = GetEarnedMoneyBet(bet, resultRoulette);
            }
            return bets;
        }

        private decimal GetEarnedMoneyBet(Bet bet, ResultRoulette resultRoulette)
        {
            if (bet.Type == TypeBet.Number && bet.Number == resultRoulette.WinningNumber)
                return bet.Amount * 5;
            if (bet.Type == TypeBet.Color && bet.Color.Equals(resultRoulette.WinningColor))
                return bet.Amount * 1.8m;
            return 0;
        }
    }
}
