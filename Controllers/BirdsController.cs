using System.Text.Json;
using BirdsSweden300.web.Data;
using BirdsSweden300.web.ViewModel.Birds;
using Microsoft.AspNetCore.Mvc;

namespace BirdsSweden300.web.Controllers
{
    [Route("Birds")]
    public class BirdsController : Controller
    {
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

        public async Task<IActionResult> Index()
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/birds");

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
        public async Task<IActionResult> Create(BirdPostViewModel bird, IFormFile file)
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

            var response = await client.PatchAsJsonAsync($"{_baseUrl}/birds/seen/{id}", id);

            if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

            return RedirectToAction(nameof(Index));
        }
        /*
        [HttpGet("upload/{id}")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file is null && file.Length <= 0)
            {
                return Content("Oops det gick fel");
            }
            using var client = _httpClient.CreateClient();

            var response = await client.PatchAsJsonAsync($"{_baseUrl}/birds/seen/{id}", id);

            if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

            return RedirectToAction(nameof(Index));
        }
        */
    }
}