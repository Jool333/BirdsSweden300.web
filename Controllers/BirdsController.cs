using System.Text.Json;
using BirdsSweden300.web.Data;
using BirdsSweden300.web.ViewModel.Birds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirdsSweden300.web.Controllers
{
    [Route("Birds")]
    public class BirdsController : Controller
    {
        private bool hideSeenChecked;
        private readonly BirdsContext _context;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;

        public BirdsController(BirdsContext context, IConfiguration config, IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
            _context = context;
            _baseUrl = config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IActionResult> Index(bool Checked = false)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/birds");
            if(Checked == true){
                response = await client.GetAsync($"{_baseUrl}/birds/hideseen");    
            }
            
            if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

            var json = await response.Content.ReadAsStreamAsync();

            var birds = JsonSerializer.Deserialize<IList<BirdListViewModel>>(json, _options);

            return View("Index", birds);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int Id)
        {

            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/birds/{Id}");

            if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

            var json = await response.Content.ReadAsStringAsync();

            var bird = JsonSerializer.Deserialize<BirdDetailsViewModel>(json, _options);

            return View("Details", bird);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var bird = new BirdPostViewModel();
            return View("Create", bird);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(BirdPostViewModel bird)
        {
            using var client = _httpClient.CreateClient();

            if (!ModelState.IsValid) return View("create", bird);
            var response = await client.PostAsJsonAsync($"{_baseUrl}/birds/create", bird);

            if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

            //var json = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("markasseen/{id}")]
        public async Task<IActionResult> MarkAsSeen(int id)
        {
            using var client = _httpClient.CreateClient();

            var seenBird = await _context.Birds.SingleOrDefaultAsync(c => c.Id == id);

            var response = await client.PatchAsJsonAsync($"{_baseUrl}/birds/seen/{id}", seenBird);

            if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

            //var json = await response.Content.ReadAsStringAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet("toggleSeen")]
        public IActionResult ToggleSeenBirds()
        {
            hideSeenChecked = !hideSeenChecked;
            return RedirectToAction(nameof(Index));
        }
    }
}