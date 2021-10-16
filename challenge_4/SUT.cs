using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace cancellation_practice.challenge_4
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

        /// <summary>
        /// You are only allowed to change this methods
        /// https://github.com/App-vNext/Polly/blob/174cc53e17bf02da5e1f2c0d74dffb4f23aa99c0/src/Polly/Timeout/TimeoutEngine.cs#L24
        private CancellationToken GetCancellationToken(CancellationToken token)
        {
            var aToken = new CancellationTokenSource(TimeSpan.FromMilliseconds(1000)).Token;
            var newToken = CancellationTokenSource.CreateLinkedTokenSource(token, token);
            return newToken.Token;
        }

        /// <summary>
        /// Do not change anything here for this test 
        /// </summary>
        public async Task<Candy> FetchCandy(CancellationToken token)
        {
            var newToken = GetCancellationToken(token);
            var response = await _httpClient.GetStringAsync( @"https://psapi.nrk.no/ping", newToken);
            var deserialized = JsonSerializer.Deserialize<PingResponse>(response, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            return new Candy(deserialized.MediaTypeVersion);
        }
    }
}
