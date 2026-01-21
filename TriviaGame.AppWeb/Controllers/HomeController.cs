using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TriviaGame.AppWeb.Filters;
using TriviaGame.AppWeb.Models;
using TriviaGame.DTOs.Trivia;

namespace TriviaGame.AppWeb.Controllers;

[JwtAuthorize]
public class HomeController : Controller
{

    private readonly HttpClient _httpClient;
   

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TriviaApi"); 
    }

    public async Task<IActionResult> Index()
    {
        var estadisticas = await _httpClient.GetFromJsonAsync<UsuarioEstadisticasDTO>("trivia/mis-estadisticas");
        ViewBag.Estadisticas = estadisticas;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
