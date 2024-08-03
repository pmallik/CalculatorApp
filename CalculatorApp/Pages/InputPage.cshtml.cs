using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CalculatorApp.Models;
using CalculatorApp.Services;
using CalculatorAPI.Models; // Make sure to use the correct namespace for your model

namespace CalculatorApp.Pages
{
    public class InputPageModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        private readonly HttpClient _httpClient;

        public InputPageModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void OnGet()
        {
           // This method is called on GET requests.
            Input = new InputModel(); // Initialize the model
            Input.InputString = String.Empty;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Calculate the length of the input string
                var isError = Validate.FormatErrors(Input.InputString);
                if (isError == "false")
                {
                    Input.Result = Evaluate.Calculate(Input.InputString);

                    var item = new HistoryItem { Key = Input.InputString, Value = Input.Result.ToString() };
                    _httpClient.PostAsJsonAsync("https://localhost:7128/api/History", item);


                    // Return the same page to display the result
                    return Page();
                }
                else
                {
                    Input.ErrorMessage = isError.ToString();
                    return Page();
                }
            }

            // If the model state is not valid, redisplay the form
            return Page();
        }
    }
}

