using Microsoft.EntityFrameworkCore;

public class FurnitureContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); optionsBuilder.UseNpgsql(@"Host=localhost;Database=furniture;Username=furniture;Password=password")
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(p => p.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>().HasOne(or => or.User).WithMany(us => us.Orders);
        modelBuilder.Entity<Order>().HasOne(or => or.Product).WithMany(us => us.Orders);
        modelBuilder.Entity<Product>().HasMany(pr => pr.Orders).WithOne(or => or.Product);
        modelBuilder.Entity<Product>().HasOne(pr => pr.Category).WithMany(or => or.Products);

    }

}
