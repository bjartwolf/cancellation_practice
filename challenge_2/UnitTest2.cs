using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_2
{
    public class UnitTest2
    {
        [Fact]
        public async Task Test2()
        {
            var timer = Task.Delay(40);
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(10));
            var cancellationToken = cancellationTokenSource.Token; 
            var candy = CandyFetcher.FetchCandy(cancellationToken);
            await Assert.ThrowsAsync<TaskCanceledException>(async () => await candy);
            Assert.False(timer.IsCompleted);
        }
    }
}
