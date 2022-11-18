using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TestMasivAPI.Model
{
    public class DataContext : DbContext
    {
        public DataContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persson { get; set; }
    }
}
