using OnlineRoulette.Models.Enums;

namespace OnlineRoulette.Models
{
    public class Roulette
    {
        public string Id { get; set; }
        public RouletteStatus Status { get; set; }

        public Roulette()
        {
        }

        public Roulette(string id,  RouletteStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}