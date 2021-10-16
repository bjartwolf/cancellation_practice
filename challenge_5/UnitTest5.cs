using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_5
{
    public class UnitTest5
    {
        /// <summary>
        /// The idea here is to show some ideas that might make it a bit more clear
        /// How the polly cancellation tokens are working
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test5()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var candyFetcher = new CandyFetcher();
            var cancellationToken = cancellationTokenSource.Token; 
            var candy = candyFetcher.FetchCandy(cancellationToken);
            cancellationTokenSource.Cancel(); 
            Assert.Equal(10, (await candy).Amount);
        }
    }
}
