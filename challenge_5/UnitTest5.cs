using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_5
{
    public class UnitTest5
    {
        /// <summary>
        /// In this test, we have a cancellationtoken we can not cancel
        /// WHAT TO DO? 
        /// The idea here is to show some ideas that might make it a bit more clear
        /// How the polly cancellation tokens are working
        /// </summary>
        [Fact]
        public async Task Test5()
        {
            var candyFetcher = new CandyFetcher();
            var candy = candyFetcher.FetchCandy(CancellationToken.None);
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await candy);
        }
    }
}
