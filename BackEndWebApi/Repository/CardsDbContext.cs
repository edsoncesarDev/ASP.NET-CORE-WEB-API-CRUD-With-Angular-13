using BackEndWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndWebApi.Repository
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet
        public DbSet<Card> Cards { get; set; }
    }
}
