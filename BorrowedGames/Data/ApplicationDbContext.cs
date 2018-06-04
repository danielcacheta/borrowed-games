using BorrowedGames.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BorrowedGames.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Friend> Friend { get; set; }
        public DbSet<BorrowedGames.Models.Game> Game { get; set; }
    }
}
