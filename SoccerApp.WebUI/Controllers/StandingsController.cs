using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebUI.Dtos.StandingDtos;
using System.Text.Json;

namespace SoccerApp.WebUI.Controllers
{
    
    public class StandingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public StandingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");

            var response = await client.GetAsync("Standings");
            if (!response.IsSuccessStatusCode)
                return View(new List<ResultStandingDto>());

            var json = await response.Content.ReadAsStringAsync();
            var standings = JsonSerializer.Deserialize<List<ResultStandingDto>>(json, _jsonOptions);

            return View(standings);
        }
    }
}