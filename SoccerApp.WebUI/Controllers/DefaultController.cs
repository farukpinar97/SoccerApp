using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebUI.Dtos.MatchDtos;
using SoccerApp.WebUI.Dtos.WeekDtos;
using System.Text.Json;

namespace SoccerApp.WebUI.Controllers
{
   
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index(int? week)
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");

           
            var weekResponse = await client.GetAsync("Weeks");
            var weeks = new List<ResultWeekDto>();

            if (weekResponse.IsSuccessStatusCode)
            {
                var weekJson = await weekResponse.Content.ReadAsStringAsync();
                weeks = JsonSerializer.Deserialize<List<ResultWeekDto>>(weekJson, _jsonOptions);
            }

            // Hafta verilmediyse maçı olan son haftayı göster
            int selectedWeek = week ?? 4;
            ViewBag.Weeks = weeks;
            ViewBag.SelectedWeek = selectedWeek;
            ViewBag.PreviousWeek = selectedWeek > 1 ? selectedWeek - 1 : (int?)null;
            ViewBag.NextWeek = weeks != null && selectedWeek < weeks.Count ? selectedWeek + 1 : (int?)null;


            var matchResponse = await client.GetAsync($"Matches/byweek/{selectedWeek}");
            if (!matchResponse.IsSuccessStatusCode)
                return View(new List<ResultMatchDto>());

            var matchJson = await matchResponse.Content.ReadAsStringAsync();
            var matches = JsonSerializer.Deserialize<List<ResultMatchDto>>(matchJson, _jsonOptions);

            return View(matches);
        }
    }
}