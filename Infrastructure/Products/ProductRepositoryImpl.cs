using DemoApp.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Products
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly DemoContext _dbContext;

        public ProductRepositoryImpl(DemoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _dbContext.Products.Select(x => new Product { Name = x.Name, Price = x.Price}).ToListAsync();
        }
    }
}
