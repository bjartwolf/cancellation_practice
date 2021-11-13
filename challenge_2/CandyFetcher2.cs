using System.Threading;
using System.Threading.Tasks;

namespace cancellation_practice.challenge_2
{
   public class Candy
    {
        public readonly int Amount;

        public Candy(int amount)
        {
            Amount = amount;
        }
    }

  public static class CandyFetcher
    {
        /// <summary>
        /// You need to do something with the token in in this method to make the test pass.
        /// </summary>
        public static async Task<Candy> FetchCandy(CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            return new Candy(10);
        }
    }
}

