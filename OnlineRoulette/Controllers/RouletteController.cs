using Microsoft.AspNetCore.Mvc;
using OnlineRoulette.Models;
using OnlineRoulette.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRoulette.Controllers
{
    [Route("api/[controller]")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService _service;

        public RouletteController(IRouletteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<Roulette> CreateRouletteAsync()
        {
            return await _service.CreateRouletteAsync();
        }

        [Route("{rouletteId}/open")]
        [HttpPut]
        public async Task<IResultWrapper> OpenRouletteAsync([FromRoute]string rouletteId)
        {
            try
            {
                return await _service.OpenRouletteAsync(rouletteId: rouletteId);
            }
            catch (Exception)
            {
                return new ResultWrapper(error: true);
            }
        }

        [Route("{rouletteId}/bet")]
        [HttpPost]
        public async Task<IResultWrapper> PlaceBetAsync([FromHeader(Name = "user-id")] string userId, [FromRoute]string rouletteId, [FromBody]Bet bet)
        {
            try
            {
                bet.UserId = userId;
                return await _service.PlaceBetAsync(rouletteId: rouletteId, bet: bet);
            }
            catch (Exception)
            {
                return new ResultWrapper(error: true);
            }
        }

        [Route("{rouletteId}/close")]
        [HttpPut]
        public async Task<IResultGenericWrapper<ResultRoulette>> CloseRouletteAsync([FromRoute]string rouletteId)
        {
            try
            {
                return await _service.CloseRouletteAsync(rouletteId: rouletteId);
            }
            catch (Exception)
            {
                return new ResultGenericWrapper<ResultRoulette>(error: true, message: "Error");
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Roulette>> GetAllRoulettesAsync()
        {
            return await _service.GetAllRoulettesAsync();
        }
    }
}