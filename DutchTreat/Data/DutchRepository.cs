using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                                .Include(order => order.Items)
                                .ThenInclude(item => item.Product)
                                .ToList();
            }
            else
            {
                return _context.Orders.ToList();

            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                                .Where(order => order.User.UserName == username)
                                .Include(order => order.Items)
                                .ThenInclude(item => item.Product)
                                .ToList();
            }
            else
            {
                return _context.Orders
                                .Where(order => order.User.UserName == username)
                                .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Products.OrderBy(product => product.Title).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to get all products: {exception}");
                return null;
            }

        }

        public Order GetOrderById(string username, int id)
        {
            return _context.Orders
                .Include(order => order.Items)
                .ThenInclude(item => item.Product)
                .Where(order => order.Id == id && order.User.UserName == username)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(product => product.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
