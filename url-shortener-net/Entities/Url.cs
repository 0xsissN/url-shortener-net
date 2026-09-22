namespace url_shortener_net.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Redirection { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
