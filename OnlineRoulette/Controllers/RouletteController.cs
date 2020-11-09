using Microsoft.AspNetCore.Mvc;
using OnlineRoulette.Models;
using OnlineRoulette.Services;
using System;
using System.Collections;
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
        public async Task<ResultWrapper> OpenRouletteAsync(string rouletteId)
        {
            try
            {
                return await _service.OpenRouletteAsync(rouletteId: rouletteId);
            }
            catch (Exception)
            {
                return new ResultWrapper(true);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Roulette>> GetAllRoulettesAsync()
        {
            return await _service.GetAllRoulettesAsync();
        }
    }
}