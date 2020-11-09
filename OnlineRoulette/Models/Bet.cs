using OnlineRoulette.Models.Enums;

namespace OnlineRoulette.Models
{
    public class Bet
    {
        public string UserId { get; set; }
        public TypeBet Type { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public decimal Amount { get; set; }
        public decimal EarnedMoney { get; set; }
    }
}
