using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace cancellation_practice.challenge_3_1
{
    public class UnitTest31
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest31(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Test31()
        {
            var timer = Task.Delay(1000);
            var cancellationTokenSource = new CancellationTokenSource();
            var candyFetcher = new CandyFetcher();
            var cancellationToken = cancellationTokenSource.Token; 
            var candy = candyFetcher.FetchCandy(cancellationToken);
            cancellationTokenSource.Cancel(); 
            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                var result = await candy;
                _testOutputHelper.WriteLine("You got candy:" + result.Amount);
            });
            Assert.False(timer.IsCompleted);
        }
    }
}
