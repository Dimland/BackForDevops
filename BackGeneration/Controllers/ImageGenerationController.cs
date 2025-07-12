using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ImageGenerationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGenerationController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        // Внедряем HttpClient через конструктор
        public ImageGenerationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateImage([FromQuery] string prompt, [FromQuery] int width = 1024, [FromQuery] int height = 1024, [FromQuery] string model = "flux")
        {
            // URL-энкодируем prompt
            var encodedPrompt = HttpUtility.UrlEncode(prompt + ". Aim for a high-resolution image (at least 300 DPI) that captures every texture and nuance, conveying a mood of anticipation and artistic potential. Don't add separate text to pictures :7 parameters | rule of thirds, golden ratio, assymetric composition, hyper- maximalist, photorealism, cinematic realism, unreal engine, 8k ::7 --ar 16:9 --s 1000");

            // Строим URL с параметрами
            var url = $"https://image.pollinations.ai/prompt/{encodedPrompt}?width={width}&height={height}&model={model}&nologo=true&enhance=true";

            // Выполняем GET-запрос
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error during generation image.");
            }

            var responseContent = await response.Content.ReadAsByteArrayAsync();
            return File(responseContent, "image/jpeg"); // Возвращаем сгенерированное изображение
        }
    }
}
