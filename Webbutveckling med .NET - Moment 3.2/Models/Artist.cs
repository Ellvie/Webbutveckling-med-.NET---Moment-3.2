namespace Webbutveckling_med_.NET___Moment_3._2.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string? Name { get; set; }
        public ICollection<Album>? Albums { get; set; }

    }
}
