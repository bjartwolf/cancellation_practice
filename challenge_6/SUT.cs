using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace cancellation_practice.challenge_6
{
    public class Somestuff{

        /// <summary>
        /// Can you find a property that is something that just can not be cancelled?
        /// https://github.com/App-vNext/Polly/blob/master/src/Polly/PolicyBase.cs#L19
        /// </summary>
        /// <returns></returns>
       public static CancellationToken GetCancellationToken()
       {
            return new CancellationTokenSource().Token;
       }
       }
}
