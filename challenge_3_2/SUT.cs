using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace cancellation_practice.challenge_3_2
{
    public class PingResponse
    {
        public int MediaTypeVersion { get; set; }
    }
    public class Candy
    {
        public readonly int Amount;

        public Candy(int amount)
        {
            Amount = amount;
        }
    }

    /// <summary>
    /// Update the method so that the test is passing
    /// It is not a trick, it is just to understand the workflow
    /// </summary>
    public class CandyFetcher
    {
        private readonly HttpClient _httpClient;

        public CandyFetcher()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<Candy> FetchCandy(CancellationToken cancellationToken)
        {
            for(var i=0; i<2; i++)
            {
                await SomeSlowNonCancellableWork();
            }
            
            var response = await _httpClient.GetStringAsync("https://psapi.nrk.no/ping",cancellationToken);
            
            var deserialized = JsonSerializer.Deserialize<PingResponse>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new Candy(deserialized.MediaTypeVersion);
        }

        /// <summary>
        /// This should not be changed
        /// </summary>
        private static async Task SomeSlowNonCancellableWork()
        {
            await Task.Delay(1000);
        }
    }
}
