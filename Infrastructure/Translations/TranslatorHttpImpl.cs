using DemoApp.Domain.Products;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

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

            var request = new HttpRequestMessage(HttpMethod.Post, "/v3/12345:translateText")
            {
                Content = JsonContent.Create(requestBody)
            };

            // This shall be flagged as a security issue by static analyzer
            var authzValue = Convert.ToBase64String(Encoding.ASCII.GetBytes("eirik:safepassword123"));
            request.Headers.Authorization = AuthenticationHeaderValue.Parse($"Basic {authzValue}");

            var response = await _httpClient.SendAsync(request);
            
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
