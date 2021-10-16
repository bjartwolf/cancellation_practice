using System;
using System.Threading;
using System.Threading.Tasks;

namespace cancellation_practice.challenge_5
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
        /// This is the methods you are allowed to change the body of
        /// </summary>
        private CancellationToken GetCancellationToken(CancellationToken token)
        {
            var newToken = new CancellationTokenSource(TimeSpan.FromMilliseconds(10)).Token;
            var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(newToken, token);
            return combinedToken.Token;
        }

        /// <summary>
        /// Not allowed to touch this
        /// </summary>
        public async Task<Candy> FetchCandy(CancellationToken token)
        {
            await Task.Delay(100, GetCancellationToken(token));
            return new Candy(10);
        }
    }
}
