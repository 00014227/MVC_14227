using _14227_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace _14227_MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthorsController(HttpClient httpClient)
        {
            // Set the base address to your API URL
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7036/"); // Replace with your actual API URL
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _httpClient.GetFromJsonAsync<List<Author>>("api/authors");
            return View(authors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _httpClient.GetFromJsonAsync<Author>($"api/authors/{id}");
            return author == null ? NotFound() : View(author);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/authors", author);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create author.");
            }
            return View(author);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _httpClient.GetFromJsonAsync<Author>($"api/authors/{id}");
            return author == null ? NotFound() : View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/authors/{id}", author);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update author.");
            }
            return View(author);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var author = await _httpClient.GetFromJsonAsync<Author>($"api/authors/{id}");
            return author == null ? NotFound() : View(author);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/authors/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to delete author.");
            return View();
        }
    }
}
