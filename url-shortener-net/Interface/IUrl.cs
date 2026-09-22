using url_shortener_net.Entities;

namespace url_shortener_net.Interface
{
    public interface IUrl
    {
        Task Create(Url url);
        Task<string> GetRedirect(string code);
    }
}

