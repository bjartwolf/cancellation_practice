using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace cancellation_practice.challenge_4
{
    public class UnitTest4
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest4(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Test4()
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
