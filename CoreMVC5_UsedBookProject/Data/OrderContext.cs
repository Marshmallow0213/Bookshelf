using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreMVC5_UsedBookProject.Models;

namespace CoreMVC5_UsedBookProject.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext (DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<CoreMVC5_UsedBookProject.Models.MoneyOrder> MoneyOrder { get; set; }

        public DbSet<CoreMVC5_UsedBookProject.Models.ChangeOrder> ChangeOrder { get; set; }
    }
}
