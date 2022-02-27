namespace Webbutveckling_med_.NET___Moment_3._2.Models
{
    public class Borrow
    {
        public int BorrowId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public Album? Album { get; set; }
        public string FullName => Firstname + " " + Lastname;
    }
}
