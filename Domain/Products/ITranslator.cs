namespace DemoApp.Domain.Products
{
    public interface ITranslator
    {
        Task<string> TranslateAsync(string englishText, string languageCode);
    }
}
