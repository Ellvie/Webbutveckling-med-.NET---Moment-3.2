namespace Webbutveckling_med_.NET___Moment_3._2.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }

        public int Tracks { get; set; }
        

        public Artist? Artist { get; set; }
    }
}
