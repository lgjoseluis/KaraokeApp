using System.Threading.Tasks;

namespace KaraokeApp.Core.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);
    }
}
