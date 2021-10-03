using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace cancellation_practice.challenge_1
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
    /// The way to fix this test will feel hacky, it
    /// is the simplest way you can think of by changing this method. 
    /// </summary>
   public static class CandyFetcher
    {
        public static async Task<Candy> FetchCandy()
        {
            await Task.Delay(100);
            return new Candy(10);
        }
    }
}
