using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_7
{
    public class UnitTest7
    {
        /// <summary>
        /// Now we will look at the difference between throwing or just returning from
        /// async sequences... Getting a bit detailed perhaps. 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test7()
        {
            var cancellationTokenSource = new CancellationTokenSource(100);
            var candyFetcher = new CandyFetcher();
            var cancellationToken = cancellationTokenSource.Token; 
            var candy = candyFetcher.FetchCandy(cancellationToken);
            await Assert.ThrowsAsync<OperationCanceledException>(async () => await candy);
        }
    }
}
