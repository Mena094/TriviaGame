using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using TriviaGame.DTOs.Categorias;
using TriviaGame.DTOs.Trivia;

public class TriviaController : Controller
{
    private readonly HttpClient _httpClient;

    public TriviaController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("TriviaApi");
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetAsync("categorias/activas");

            if (response.IsSuccessStatusCode)
            {
                var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaResponseDTO>>();
                return View(categorias ?? new List<CategoriaResponseDTO>());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.ErrorMessage = "No se pudieron cargar las categorías.";
            return View(new List<CategoriaResponseDTO>());
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error al obtener las categorías: {ex.Message}";
            return View(new List<CategoriaResponseDTO>());
        }
    }
    [HttpPost]
    public async Task<IActionResult> IniciarJuego(int categoriaId)
    {

        var respPartida = await _httpClient.PostAsJsonAsync("trivia/crear-partida", new { CategoriaId = categoriaId });
        if (respPartida.IsSuccessStatusCode)
        {
            var jsonDoc = await respPartida.Content.ReadFromJsonAsync<JsonElement>();

            if (jsonDoc.TryGetProperty("partidaId", out var prop))
            {
                int partidaId = prop.GetInt32();

                var respPreguntas = await _httpClient.GetFromJsonAsync<List<QuestionResponseDTO>>($"trivia/preguntas/{categoriaId}");

                HttpContext.Session.SetInt32("PartidaId", partidaId);
                HttpContext.Session.SetString("Preguntas", JsonSerializer.Serialize(respPreguntas));

                return RedirectToAction("Jugar", new { indice = 0 });
            }
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Jugar(int indice)
    {
        var preguntasJson = HttpContext.Session.GetString("Preguntas");
        if (string.IsNullOrEmpty(preguntasJson)) return RedirectToAction("Index");

        var preguntas = JsonSerializer.Deserialize<List<QuestionResponseDTO>>(preguntasJson);

        if (indice >= preguntas.Count)
            return RedirectToAction("FinalizarPartida");

        ViewBag.Indice = indice;
        ViewBag.PartidaId = HttpContext.Session.GetInt32("PartidaId");

        return View(preguntas[indice]);
    }

    [HttpPost]
    public async Task<IActionResult> EnviarRespuesta(int preguntaId, int respuestaId, int siguienteIndice)
    {
        var idPartida = HttpContext.Session.GetInt32("PartidaId") ?? 0;

        var detalle = new DetallePartidaDTO { PartidaId = idPartida, PreguntaId = preguntaId, RespuestaId = respuestaId };
        await _httpClient.PostAsJsonAsync("trivia/responder", detalle);

        return RedirectToAction("Jugar", new { indice = siguienteIndice });
    }
    public async Task<IActionResult> FinalizarPartida()
    {
        var partidaId = HttpContext.Session.GetInt32("PartidaId");


        var response = await _httpClient.PostAsync($"trivia/finalizar/{partidaId}", null);

        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (resultado.TryGetProperty("puntaje", out var pProp))
            {
                ViewBag.Puntaje = pProp.GetDecimal();
            }
            else
            {
                ViewBag.Puntaje = 0;
            }

            return View("Resultado");
        }

        return RedirectToAction("Index");
    }
}