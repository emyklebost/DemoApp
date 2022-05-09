namespace DemoApp.Domain.Products
{
    internal class ProductComparer : IComparer<Product>
    {
        public int Compare(Product? x, Product? y)
        {
            return decimal.ToInt32(Math.Ceiling((x?.Price ?? 0) - (y?.Price ?? 0)));
        }
    }
}
