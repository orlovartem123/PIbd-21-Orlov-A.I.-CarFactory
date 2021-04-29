using CarFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFactoryDatabaseImplement
{
    public class CarFactoryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"data source='LAPTOP-4UI9Q996\SQLEXPRESS'; initial catalog='CarFactoryDatabaseHARD'; user id='sa'; password='123123'; Persist Security Info='True'; Connect Timeout='60';");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }

        public virtual DbSet<Car> Cars { set; get; }

        public virtual DbSet<CarComponent> CarComponents { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<Client> Clients { set; get; }

        public virtual DbSet<Warehouse> Warehouses { set; get; }

        public virtual DbSet<WarehouseComponent> WarehouseComponents { set; get; }
    }
}
