using System.Collections.Generic;

namespace OnlineRoulette.Models
{
    public class ResultRoulette
    {
        public string RouletteId { get; set; }
        public int WinningNumber { get; set; }
        public string WinningColor  { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
