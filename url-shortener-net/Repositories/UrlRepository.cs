using Microsoft.EntityFrameworkCore;

using url_shortener_net.Data;
using url_shortener_net.Entities;
using url_shortener_net.Interface;

namespace url_shortener_net.Repositories
{
    public class UrlRepository : IUrl
    {
        private readonly DataContext _context;
        public UrlRepository(DataContext context) => _context = context;
        public async Task Create(Url url)
        {
            await _context.Url.AddAsync(url);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetRedirect(string code)
        {
            var url = await _context.Url
                .Where(u => u.Code == code)
                .FirstOrDefaultAsync();

            if (url is null) throw new Exception("Codigo no encontrado");

            return url.Redirection;
        }
    }
}

