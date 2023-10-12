using System.Net;
using Xunit;

namespace cancellation_practice.cancellation_net5andnewer
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.Ambiguous);
        }
    }

    /// <summary>
    /// Starting with net core 5 we can tell the difference
    /// https://github.com/dotnet/runtime/pull/2281
    /// </summary>
    public class HttpCancellationAfter5
    {
        [Fact]
        public async Task GetRequest_CancelTask_ThrowTaskCancelledException()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler());
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));
            var ct = cancellationTokenSource.Token;
            var exception = await Assert.ThrowsAsync<TaskCanceledException>(async () => await httpClient.GetAsync("https://example.com", ct));
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public async Task Get_HttpTimeout100msOnSlowResponse_CanDifferentiateFromCancellations()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler())
            {
                Timeout = TimeSpan.FromMilliseconds(1)
            };

            var exception = await Assert.ThrowsAsync<TaskCanceledException>(async () => await httpClient.GetAsync("https://example.com", CancellationToken.None));

            Assert.IsType<TimeoutException>(exception.InnerException);
        }
    }
}
