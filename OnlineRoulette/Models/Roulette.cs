using OnlineRoulette.Models.Enums;
using System.Collections.Generic;

namespace OnlineRoulette.Models
{
    public class Roulette
    {
        public string Id { get; set; }
        public StatusRoulette Status { get; set; }
        public List<Bet> Bets { get; set; }

        public Roulette()
        {
        }

        public Roulette(string id,  StatusRoulette status)
        {
            Id = id;
            Status = status;
        }
    }
}