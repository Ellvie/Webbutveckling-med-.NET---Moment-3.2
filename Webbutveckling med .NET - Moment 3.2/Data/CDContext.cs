using Microsoft.EntityFrameworkCore;

namespace Webbutveckling_med_.NET___Moment_3._2.Data
{
    public class CDContext : DbContext
    {
        public CDContext(DbContextOptions<CDContext> options) : base(options)
        {
        }

        public DbSet<Models.Album> Album { get; set; }
        public DbSet<Models.Artist> Artist { get; set; }
    }
}
