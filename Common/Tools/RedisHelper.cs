using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Common.Tools
{
    public class RedisHelper : IRedisHelper
    {
        public IDatabase _redis { get; set; }

        public RedisHelper(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> StringSetAsync(string key, string value, TimeSpan expireTime)
        {
            return await _redis.StringSetAsync(key, value, expireTime);
        }

        public async Task<bool> StringSetAsync(string key, string value)
        {
            return await _redis.StringSetAsync(key, value);
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _redis.KeyExistsAsync(key);
        }

        public async Task<bool> KeyDeleteAsync(string key)
        {
            return await _redis.KeyDeleteAsync(key);
        }

        public async Task<string> StringGetAsync(string key)
        {
            return await _redis.StringGetAsync(key);
        }
    }
}