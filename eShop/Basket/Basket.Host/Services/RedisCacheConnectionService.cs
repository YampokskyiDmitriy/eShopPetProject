using Basket.Host.Configurations;
using Basket.Host.Services.Interfaces;
using StackExchange.Redis;

namespace Basket.Host.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public RedisCacheConnectionService(IOptions<RedisConfig> config)
        {
            var redisConfigOptions = ConfigurationOptions.Parse(config.Value.Host);
            redisConfigOptions.AsyncTimeout = 60000;
            redisConfigOptions.ConnectTimeout = 60000;
            redisConfigOptions.SyncTimeout = 60000;
            redisConfigOptions.AbortOnConnectFail = false;
            _connectionLazy = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(redisConfigOptions));
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;
        public void Dispose()
        {
            if (!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}