using AcmeWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeWebAPI
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
