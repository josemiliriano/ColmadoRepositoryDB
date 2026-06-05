using ApiColmadoDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiColmadoDB
{
    public class MyDataContext:DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
