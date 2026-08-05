using Microsoft.AspNetCore.Mvc;
using SoccerApp.WebUI.Dtos.MatchDtos;
using SoccerApp.WebUI.Dtos.StandingDtos;
using System.Text.Json;

namespace SoccerApp.WebUI.Controllers
{
  
    public class FixturesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public FixturesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");


            var response = await client.GetAsync("Matches/nextweek");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Forms = new Dictionary<int, List<string>>();
                return View(new List<ResultMatchDto>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var matches = JsonSerializer.Deserialize<List<ResultMatchDto>>(json, _jsonOptions)
                          ?? new List<ResultMatchDto>();


            var forms = new Dictionary<int, List<string>>();

            var standingResponse = await client.GetAsync("Standings");
            if (standingResponse.IsSuccessStatusCode)
            {
                var standingJson = await standingResponse.Content.ReadAsStringAsync();
                var standings = JsonSerializer.Deserialize<List<ResultStandingDto>>(standingJson, _jsonOptions);

                if (standings != null)
                    forms = standings.ToDictionary(x => x.TeamId, x => x.Form);
            }

            ViewBag.Forms = forms;

            return View(matches);
        }


        public async Task<IActionResult> LastFiveMatches(int teamId)
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");

            var response = await client.GetAsync($"Matches/lastmatches/{teamId}?count=5");
            if (!response.IsSuccessStatusCode)
                return PartialView("_LastFiveMatches", new List<ResultMatchDto>());

            var json = await response.Content.ReadAsStringAsync();
            var matches = JsonSerializer.Deserialize<List<ResultMatchDto>>(json, _jsonOptions)
                          ?? new List<ResultMatchDto>();

            ViewBag.TeamId = teamId;
            return PartialView("_LastFiveMatches", matches);
        }
    }
}
