using System.Data.Entity;

namespace ConsoleApp1
{
    public class Database : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }

        public Database() : base("DbProductShop")
        {

        }
    }
}