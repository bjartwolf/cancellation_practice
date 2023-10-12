using System.Net;
using Xunit;

namespace cancellation_net3._1
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.Ambiguous);
        }
    }

    /// <summary>
    /// In net core 3.1 we can not see the difference
    /// fixed in net core 5 https://github.com/dotnet/runtime/pull/2281
    /// </summary>
    public class HttpCancellation31
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
        public async Task Get_Timeout100msOnSlowResponse_MakesMeConfusedBecauseBothAreTrue()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler())
            {
                Timeout = TimeSpan.FromMilliseconds(100)
            };

            var exception = await Assert.ThrowsAsync<TaskCanceledException>(async () => await httpClient.GetAsync("https://example.com", CancellationToken.None));

            Assert.Null(exception.InnerException);
        }
    }
}
