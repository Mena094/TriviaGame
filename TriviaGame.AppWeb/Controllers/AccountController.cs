using Microsoft.AspNetCore.Mvc;
using TriviaGame.DTOs.Auth;
using System.Net.Http.Json;

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
        var response = await _httpClient.PostAsJsonAsync("auth/register", request);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Login");
        }

        var error = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
        ViewBag.Error = error?.Mensaje ?? "Error al registrar";
        return View(request);
    }
}