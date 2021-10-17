using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace cancellation_practice.challenge_3_2
{
    public class UnitTest32
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest32(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Test3_2()
        {
            var timer = Task.Delay(1000);
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token; 
            cancellationTokenSource.Cancel(); 

            var candyFetcher = new CandyFetcher();
            var candy = candyFetcher.FetchCandy(cancellationToken);
            
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                var result = await candy;
                _testOutputHelper.WriteLine("You got candy:" + result.Amount);
            });
            Assert.False(timer.IsCompleted);
        }
    }
}
