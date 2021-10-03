using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_4
{
    public class UnitTest4
    {
        [Fact]
        public async Task Test4()
        {
            var timer = Task.Delay(100);
            var cancellationTokenSource = new CancellationTokenSource();
            var candyFetcher = new CandyFetcher();
            var cancellationToken = cancellationTokenSource.Token; 
            var candy = candyFetcher.FetchCandy(cancellationToken);
            cancellationTokenSource.Cancel(); 
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await candy);
            Assert.False(timer.IsCompleted);
        }
    }
}
