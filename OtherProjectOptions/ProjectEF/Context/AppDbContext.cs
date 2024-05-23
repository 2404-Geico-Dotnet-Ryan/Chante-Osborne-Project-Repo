using Microsoft.EntityFrameworkCore;

class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Plant> Plants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = File.ReadAllText(@"C:\Users\U88ABJ\Revature\ChantePlantShopSQLdb.txt");
    }

}