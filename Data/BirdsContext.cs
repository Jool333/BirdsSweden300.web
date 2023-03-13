using BirdsSweden300.web.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdsSweden300.web.Data
{
    public class BirdsContext : DbContext
    {
        public DbSet<BirdModel> Birds { get; set; }
        public BirdsContext(DbContextOptions options) : base(options){}
    }
}