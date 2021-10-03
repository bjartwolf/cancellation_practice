using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_6
{
    public class UnitTest6
    {
        /// <summary>
        /// The idea here is to show some ideas that might make it a bit more clear
        /// How the polly cancellation tokens are working
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Test6()
        {
            Assert.False(Somestuff.GetCancellationToken().CanBeCanceled);
        }
    }
}
