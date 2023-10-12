using System;
using System.Net;
using Xunit;

namespace cancellation_net5
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

    public class HttpCancellationAfter5
    {
        [Fact]
        public async Task GetRequest_CancelTask_ThrowTaskCancelledException()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler());
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromMilliseconds(10));
            var ct = cancellationTokenSource.Token;
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await httpClient.GetAsync("https://example.com", ct));
        }

        [Fact]
        public async Task Test2()
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