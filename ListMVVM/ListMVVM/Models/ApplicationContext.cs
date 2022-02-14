using ListMVVM.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Phone> Phones => Set<Phone>();
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)  => Database.EnsureCreated();

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite("Data Source=helloapp.db");
    //}
}