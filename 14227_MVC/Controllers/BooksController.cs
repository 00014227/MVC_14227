using _14227_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace _14227_MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7036/"); // Replace with your actual API URL
        }

        public async Task<IActionResult> Index()
        {
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("api/books");
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"api/books/{id}");
            return book == null ? NotFound() : View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/books", book);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create book.");
            }
            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"api/books/{id}");
            return book == null ? NotFound() : View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/books/{id}", book);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to update book.");
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"api/books/{id}");
            return book == null ? NotFound() : View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/books/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to delete book.");
            return View();
        }
    }
}
