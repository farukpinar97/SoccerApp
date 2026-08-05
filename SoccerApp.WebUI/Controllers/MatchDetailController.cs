using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebUI.Dtos.MatchDtos;
using SoccerApp.WebUI.Dtos.MatchEventDtos;
using SoccerApp.WebUI.Dtos.MatchStatisticDtos;
using System.Text.Json;

namespace SoccerApp.WebUI.Controllers
{
    
    public class MatchDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public MatchDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");

            
            var matchResponse = await client.GetAsync($"Matches/{id}");
            if (!matchResponse.IsSuccessStatusCode)
                return NotFound();

            var matchJson = await matchResponse.Content.ReadAsStringAsync();
            var match = JsonSerializer.Deserialize<ResultMatchDto>(matchJson, _jsonOptions);

            
            var eventResponse = await client.GetAsync($"MatchEvents/bymatch/{id}");
            var events = new List<ResultMatchEventDto>();

            if (eventResponse.IsSuccessStatusCode)
            {
                var eventJson = await eventResponse.Content.ReadAsStringAsync();
                events = JsonSerializer.Deserialize<List<ResultMatchEventDto>>(eventJson, _jsonOptions);
            }

           
            var statResponse = await client.GetAsync($"MatchStatistics/bymatch/{id}");
            ResultMatchStatisticDto statistic = null;

            if (statResponse.IsSuccessStatusCode)
            {
                var statJson = await statResponse.Content.ReadAsStringAsync();
                statistic = JsonSerializer.Deserialize<ResultMatchStatisticDto>(statJson, _jsonOptions);
            }

            ViewBag.Events = events;
            ViewBag.Statistic = statistic;

            return View(match);
        }
    }
}