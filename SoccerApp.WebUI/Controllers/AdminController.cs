using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SoccerApp.WebUI.Dtos.EventTypeDtos;
using SoccerApp.WebUI.Dtos.MatchDtos;
using SoccerApp.WebUI.Dtos.MatchEventDtos;
using SoccerApp.WebUI.Dtos.MatchStatisticDtos;
using SoccerApp.WebUI.Dtos.PlayerDtos;
using SoccerApp.WebUI.Dtos.RefereeDtos;
using SoccerApp.WebUI.Dtos.StadiumDtos;
using SoccerApp.WebUI.Dtos.TeamDtos;
using SoccerApp.WebUI.Dtos.WeekDtos;
using System.Text;
using System.Text.Json;

namespace SoccerApp.WebUI.Controllers
{

    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonOptions =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index() => View();

        // ─────────────── MAC EKLEME ───────────────

        [HttpGet]
        public async Task<IActionResult> CreateMatch()
        {
            await LoadMatchDropdownsAsync();
            return View(new CreateMatchDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchDto dto)
        {
            if (dto.HomeTeamId == dto.AwayTeamId)
                ModelState.AddModelError("", "Ev sahibi ve deplasman takimi ayni olamaz.");

            if (!ModelState.IsValid)
            {
                await LoadMatchDropdownsAsync();
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("SoccerApi");
            var content = ToJsonContent(dto);

            var response = await client.PostAsync("Matches", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Maç eklenemedi ({(int)response.StatusCode}): {error}");

            await LoadMatchDropdownsAsync();
            return View(dto);
        }

        // ─────────────── MAC OLAYI EKLEME ───────────────

        [HttpGet]
        public async Task<IActionResult> CreateMatchEvent()
        {
            await LoadEventDropdownsAsync();
            return View(new CreateMatchEventDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent(CreateMatchEventDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadEventDropdownsAsync();
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("SoccerApi");
            var response = await client.PostAsync("MatchEvents", ToJsonContent(dto));

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Mac olayi eklenemedi.");
            await LoadEventDropdownsAsync();
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> GetPlayersByTeam(int teamId)
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");
            var response = await client.GetAsync($"Players/byteam/{teamId}");

            if (!response.IsSuccessStatusCode)
                return Json(new List<ResultPlayerDto>());

            var json = await response.Content.ReadAsStringAsync();
            var players = JsonSerializer.Deserialize<List<ResultPlayerDto>>(json, _jsonOptions);

            return Json(players);
        }

        // ─────────────── MAC ISTATISTIGI EKLEME ───────────────

        [HttpGet]
        public async Task<IActionResult> CreateMatchStatistic()
        {
            ViewBag.Matches = await GetMatchSelectListAsync();
            return View(new CreateMatchStatisticDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchStatistic(CreateMatchStatisticDto dto)
        {
            if (dto.HomePossession + dto.AwayPossession != 100)
                ModelState.AddModelError("", "Topa sahip olma oranlarinin toplami 100 olmalidir.");

            if (!ModelState.IsValid)
            {
                ViewBag.Matches = await GetMatchSelectListAsync();
                return View(dto);
            }

            var client = _httpClientFactory.CreateClient("SoccerApi");
            var response = await client.PostAsync("MatchStatistics", ToJsonContent(dto));

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Istatistik eklenemedi. Bu mac icin zaten girilmis olabilir.");
            ViewBag.Matches = await GetMatchSelectListAsync();
            return View(dto);
        }

        // ─────────────── YARDIMCI METOTLAR ───────────────

        private static StringContent ToJsonContent(object dto)
        {
            var json = JsonSerializer.Serialize(dto);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<List<T>> GetListAsync<T>(string url)
        {
            var client = _httpClientFactory.CreateClient("SoccerApi");
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<T>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
        }

        private async Task LoadMatchDropdownsAsync()
        {
            var teams = await GetListAsync<ResultTeamDto>("Teams");
            var weeks = await GetListAsync<ResultWeekDto>("Weeks");
            var stadiums = await GetListAsync<ResultStadiumDto>("Stadiums");
            var referees = await GetListAsync<ResultRefereeDto>("Referees");

            ViewBag.Teams = teams
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

            ViewBag.Weeks = weeks
                .Select(x => new SelectListItem { Text = $"{x.WeekNumber}. Hafta", Value = x.Id.ToString() })
                .ToList();

            ViewBag.Stadiums = stadiums
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

            ViewBag.Referees = referees
                .Select(x => new SelectListItem { Text = x.FullName, Value = x.Id.ToString() })
                .ToList();
        }

        private async Task LoadEventDropdownsAsync()
        {
            var eventTypes = await GetListAsync<ResultEventTypeDto>("EventTypes");
            var teams = await GetListAsync<ResultTeamDto>("Teams");

            ViewBag.Matches = await GetMatchSelectListAsync();

            ViewBag.EventTypes = eventTypes
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

            ViewBag.Teams = teams
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();
        }

        private async Task<List<SelectListItem>> GetMatchSelectListAsync()
        {
            var matches = await GetListAsync<ResultMatchDto>("Matches");

            return matches
                .Select(x => new SelectListItem
                {
                    Text = $"{x.HomeTeam?.Name} - {x.AwayTeam?.Name} ({x.MatchDateTime:dd.MM.yyyy})",
                    Value = x.Id.ToString()
                })
                .ToList();
        }
    }
}