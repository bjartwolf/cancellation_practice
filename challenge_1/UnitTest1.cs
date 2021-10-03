using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var timer = Task.Delay(TimeSpan.FromMilliseconds(20)); 
            var candy = CandyFetcher.FetchCandy();
            Assert.Equal(10, (await candy).Amount);
            Assert.False(timer.IsCompleted);
        }
    }
}
