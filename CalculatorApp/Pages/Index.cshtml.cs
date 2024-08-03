namespace CalculatorApp.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CalculatorAPI.Models;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public Dictionary<string, string> DataStore { get; set; } = new Dictionary<string, string>();

    [BindProperty]
    public string Key { get; set; }

    [BindProperty]
    public string Value { get; set; }

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task OnGet()
    {
        DataStore = await _httpClient.GetFromJsonAsync<Dictionary<string, string>>("https://localhost:7128/api/History");
    }

    public async Task<IActionResult> OnPost()
    {
        if (!string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Value))
        {
            var item = new HistoryItem { Key = Key, Value = Value };
            await _httpClient.PostAsJsonAsync("/api/history", item);
        }

        return RedirectToPage();
    }
}
