using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.Entities
{
    public class DutchContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        /*
         * Don't need OrderItems becasuse Orders has a relationship to OrderItems. We only need OrderItems
         * as a child or Orders. Only create a DbSet for things you want to query directly against.
         * Because we will only be getting OrderItems when we get the containing Order, there isn't need
         * for it to have a DbSet.
         */
    }
}
