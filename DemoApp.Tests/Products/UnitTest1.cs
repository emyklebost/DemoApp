using DemoApp.Domain.Products;

namespace DemoApp.Tests.Products
{
    public class ProductComparerTests
    {
        [Fact]
        public void ProductComparer_ShallOrderByPrice()
        {
            var products = new List<Product>
            {
                new Product { Name = "product1", Price = 44321 },
                new Product { Name = "product2", Price = 553 },
                new Product { Name = "product3", Price = 0 },
                new Product { Name = "product4", Price = -2 },
                new Product { Name = "product5", Price = 213 },
            };

            var sut = new ProductComparer();
            products.Sort(sut);

            var prices = products.Select(x => x.Price).ToList();
            var expected = prices.ToList();
            expected.Sort();

            Assert.True(expected.SequenceEqual(products.Select(x => x.Price)));
            Assert.True(prices.First() < prices.Last());
        }

        [Fact]
        public void ProductComparer_FailingTest()
        {
            Assert.Equal(3, 1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void IsLessThan5(int value)
        {
            var result = value < 5;

            Assert.True(result, $"{value} is not less than 5.");
        }
    }
}