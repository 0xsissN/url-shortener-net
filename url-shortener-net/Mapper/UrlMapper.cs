using url_shortener_net.Entities;

namespace url_shortener_net.Mapper
{
    public class UrlMapper
    {
        public static Url ToEntity(string redirection, string code)
        {
            return new Url
            {
                Code = code,
                Redirection = redirection,
                CreatedAt = DateTime.UtcNow.AddHours(-4)
            };
        }
    }
}
