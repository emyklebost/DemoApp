namespace DemoApp.Domain.Products
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        private readonly ITranslator _translator;

        public ProductService(IProductRepository repo, ITranslator translator)
        {
            _repo = repo;
            _translator = translator;
        }

        public async Task<List<Product>> GetAllAsync(string languageCode)
        {
            var products = await _repo.GetAllAsync();
            products.Sort(new ProductComparer());

            var tasks = products.Select(async x => x.Name = await _translator.TranslateAsync(x.Name, languageCode));
            await Task.WhenAll(tasks);

            return products;
        }
    }
}
