using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace cancellation_practice.challenge_7
{
    public class Candy
    {
        public readonly int Amount;

        public Candy(int amount)
        {
            Amount = amount;
        }
    }

    public class CandyFetcher
    {
        /// <summary>
        /// This is the method you are allowed to change the body of
        /// </summary>
        private static async IAsyncEnumerable<Candy> Candies(long amount, [EnumeratorCancellation] CancellationToken ct)
        {
            var i = 0;
            while (i < amount && !ct.IsCancellationRequested)
            {
                i++;
                await Task.Delay(10);
                yield return  new Candy(1);
            }
        }

        /// <summary>
        /// Not allowed to touch this
        /// </summary>
        public async Task<Candy> FetchCandy(CancellationToken token)
        {
            await Task.CompletedTask;
            var aggregatedCandy = Candies(10, token);
            var sum  = await aggregatedCandy.SumAsync(foo => foo.Amount, cancellationToken: token);
            return new Candy(sum);
        }
    }
}
