using DemoApp.Domain.Products;
using System.Net.Http.Json;

namespace DemoApp.Infrastructure.Translations
{
    public class TranslatorHttpImpl : ITranslator
    {
        private readonly HttpClient _httpClient;

        public TranslatorHttpImpl(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> TranslateAsync(string englishText, string languageCode)
        {
            // Uses google translate API: https://cloud.google.com/translate/docs/reference/rest/v3/projects/translateText
            var requestBody = new RequestBody
            {
                Contents = new List<string> { englishText },
                TargetLanguageCode = languageCode,
            };

            var response = await _httpClient.PostAsJsonAsync("/v3/12345:translateText", requestBody);
            
            try
            {
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadFromJsonAsync<ResponseBody>();
                return responseBody.FeedbackTranslateTextResponse.Single().TranslatedText;
            }
            catch (HttpRequestException)
            {
                return englishText;
            }
        }

        private class RequestBody
        {
            public List<string> Contents { get; set; }

            public string SourceLanguageCode => "en-US";

            public string TargetLanguageCode { get; set; }
        }

        private class ResponseBody
        {
            public List<Translation> FeedbackTranslateTextResponse { get; set; }

            public class Translation
            {
                public string TranslatedText { get; set; }
            }
        }
    }
}
