using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Bank.Models
{
    public class BankContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Card> Cards { get; set; }
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {

            // Console.WriteLine($"HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            // Database.EnsureDeleted();

            if (Database.EnsureCreated())
            {
                Cards.Add(new Card { CardId = 1, Score = 50000m });
                Cards.Add(new Card { CardId = 2, Score = 45000m });
                Cards.Add(new Card { CardId = 3, Score = 30000m });
                Cards.Add(new Card { CardId = 4, Score = 80000m });

                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasKey(card => card.CardId);
            modelBuilder.Entity<Order>().HasKey(order => order.OrderId);

            modelBuilder.Entity<Card>()
                .HasMany(card => card.Order)
                .WithOne(order => order.Card)
                .HasForeignKey(order => order.CardId);
        }
    }
}