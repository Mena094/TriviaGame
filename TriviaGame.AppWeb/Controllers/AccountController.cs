using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using TriviaGame.DTOs.Auth;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TriviaApi");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDTO request)
    {
        if (!ModelState.IsValid) return View(request);

        var response = await _httpClient.PostAsJsonAsync("auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

            HttpContext.Response.Cookies.Append("JwtToken", result.Token);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Usuario o contraseña incorrectos";
        return View(request);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequestDTO request)
    {
        if (!ModelState.IsValid)
            return View(request);

        var response = await _httpClient.PostAsJsonAsync("auth/register", request);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Login");
        }

        var error = await response.Content.ReadFromJsonAsync<LoginResponseDTO>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        ModelState.AddModelError(string.Empty, error?.Mensaje ?? "Error al registrar");

        return View(request);
    }
}