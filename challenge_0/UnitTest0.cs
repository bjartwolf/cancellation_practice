using System;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_0
{
    public class UnitTest0
    {
        /// <summary>
        /// This is just to illustrate how the kata runs 
        /// Look in the CandyFetcher class and see where you are allowed to make changes
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test0()
        {
            var candy = CandyFetcher.FetchCandy();
            Assert.Equal(10, (await candy).Amount);
        }
    }
}
