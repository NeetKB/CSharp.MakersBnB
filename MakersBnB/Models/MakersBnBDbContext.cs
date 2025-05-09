namespace MakersBnB.Models;
using Microsoft.EntityFrameworkCore;

public class MakersBnBDbContext : DbContext
// DbContext is the base class that represents 
// a session with the database and allows querying and saving data.
{
    public DbSet<Space>? Spaces { get; set; }
    // DbSet<T> represents a collection of entities in a database (in this case, Space entities).
    // EF Core uses DbSet to perform CRUD operations on the corresponding database table.
    
    //Users is only public for testing purposes
    public DbSet<User>? Users { get; set; }
    internal string? DbPath { get; }

    internal string dbName = "makersbnb_aspdotnet_dev";

    // OnConfiguring is called by EF Core when the context is being set up. 
    // It’s where you configure things like the database connection string.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // The DbContextOptionsBuilder parameter (optionsBuilder) is used to build 
    // the configuration for the context (e.g., telling EF Core which database provider to use).

        => optionsBuilder.UseNpgsql(
          @"Host=localhost;Username=postgres;Password=1234;Database=" + this.dbName
        );
        //the above is what EF core uses to connect tot he PostgreSql db
}
