using System;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_0
{
   public class Candy
    {
        public readonly int Amount;

        public Candy(int amount)
        {
            Amount = amount;
        }
    }

    /// <summary>
    /// Update the method so that the test is passing
    /// It is not a trick, it is just to understand the workflow
    /// </summary>
   public static class CandyFetcher
    {
        public static async Task<Candy> FetchCandy()
        {
            await Task.Delay(1);
            return new Candy(9);
        } 
    }
}
