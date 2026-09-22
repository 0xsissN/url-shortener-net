using Microsoft.AspNetCore.Mvc;
using url_shortener_net.UseCases;

namespace url_shortener_net.Controllers
{
    [Route("url")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly CreateUrlUseCase _createUrlUseCase;
        private readonly GetRedirectionUseCase _getRedirectionUseCase;
        public UrlController(CreateUrlUseCase createUrlUseCase, GetRedirectionUseCase getRedirectionUseCase)
        {
            _createUrlUseCase = createUrlUseCase;
            _getRedirectionUseCase = getRedirectionUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUrl(string redirection)
        {
            var url = await _createUrlUseCase.Execute(redirection);
            return Ok(url);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetRedirection(string code)
        {
            var redirection = await _getRedirectionUseCase.Execute(code);
            return Redirect(redirection);
        }
    }
}
