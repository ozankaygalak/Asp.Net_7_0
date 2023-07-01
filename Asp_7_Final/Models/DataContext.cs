namespace Asp_7_Final.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Eğer gerekliyse, Users tablosu için ekstra yapılandırmaları burada yapabilirsiniz.
        // Örneğin, indeksler, ilişkiler vb.
    }
}
