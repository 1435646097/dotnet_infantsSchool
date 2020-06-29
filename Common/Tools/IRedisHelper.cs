using System;
using System.Threading.Tasks;

namespace Common.Tools
{
    public interface IRedisHelper
    {
        Task<bool> StringSetAsync(string key, string value, TimeSpan expireTime);

        Task<bool> StringSetAsync(string key, string value);

        Task<bool> KeyExistsAsync(string key);

        Task<bool> KeyDeleteAsync(string key);

        Task<string> StringGetAsync(string key);
    }
}