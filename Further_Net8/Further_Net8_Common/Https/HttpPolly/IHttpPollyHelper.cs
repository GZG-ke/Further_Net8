using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Model.Enums;
using Newtonsoft.Json;

namespace Further_Net8_Common.Https.HttpPolly
{
    public interface IHttpPollyHelper
    {
        Task<T> PostAsync<T, R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string> headers = null);
        Task<T> PostAsync<T>(HttpEnum httpEnum, string url, string request, Dictionary<string, string> headers = null);
        Task<string> PostAsync(HttpEnum httpEnum, string url, string request, Dictionary<string, string> headers = null);
        Task<string> PostAsync<R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string> headers = null);
        Task<T> GetAsync<T>(HttpEnum httpEnum, string url, Dictionary<string, string> headers = null);
        Task<string> GetAsync(HttpEnum httpEnum, string url, Dictionary<string, string> headers = null);
        Task<T> PutAsync<T, R>(HttpEnum httpEnum, string url, R request, Dictionary<string, string> headers = null);
        Task<T> PutAsync<T>(HttpEnum httpEnum, string url, string request, Dictionary<string, string> headers = null);
        Task<T> DeleteAsync<T>(HttpEnum httpEnum, string url, Dictionary<string, string> headers = null);
    }
}
