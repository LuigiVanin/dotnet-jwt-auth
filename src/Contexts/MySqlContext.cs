using Microsoft.EntityFrameworkCore;
using UserJwt.Models;

namespace UserJwt.Context
{
  public class MySqlContext : DbContext
  {

    public DbSet<User> Users { get; set; }

    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

  }
}