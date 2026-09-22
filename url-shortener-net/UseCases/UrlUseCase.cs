using System.Security.Cryptography;
using url_shortener_net.Mapper;
using url_shortener_net.Interface;

namespace url_shortener_net.UseCases
{
    public class CreateUrlUseCase
    {
        private readonly IUrl _urlRepository;
        public CreateUrlUseCase(IUrl urlRepository) => _urlRepository = urlRepository;
        public async Task<string> Execute(string redirection)
        {
            var code = GenerateRandomCode(16);

            var url = UrlMapper.ToEntity(redirection, code);

            await _urlRepository.Create(url);

            return CreateRedirection(code);
        }
        private static string GenerateRandomCode(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string code = "";

            for (int i = 0; i < length; i++)
            {
                int index = RandomNumberGenerator.GetInt32(chars.Length);
                code += chars[index];
            }

            return code;
        }

        private static string CreateRedirection(string code)
        {
            return $"https://localhost:7180/url/{code}";
        }
    }

    public class GetRedirectionUseCase
    {
        private readonly IUrl _urlRepository;
        public GetRedirectionUseCase(IUrl urlRepository) => _urlRepository = urlRepository;
        public async Task<string> Execute(string code)
        {
            var redirection = await _urlRepository.GetRedirect(code);
            return redirection;
        }
    }
}

