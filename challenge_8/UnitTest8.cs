using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_8
{
    public class UnitTest8
    {
        /// <summary>
        /// Now we will look at the difference between throwing or just returning from
        /// async sequences... Getting a bit detailed perhaps. 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test8()
        {
            var cancellationTokenSource = new CancellationTokenSource(100);
            var candyFetcher = new CandyFetcher();
            var cancellationToken = cancellationTokenSource.Token; 
            var candy = await candyFetcher.FetchCandy(cancellationToken);
            Assert.InRange(candy.Amount, 3,9);
        }
    }
}
