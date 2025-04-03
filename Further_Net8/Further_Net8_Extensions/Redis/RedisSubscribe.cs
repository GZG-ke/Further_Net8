using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Common;
using InitQ.Abstractions;
using InitQ.Attributes;

namespace Further_Net8_Extensions.Redis
{
    public class RedisSubscribe : IRedisSubscribe
    {
        public RedisSubscribe()
        {
        }

        [Subscribe(RedisMqKey.Loging)]
        private async Task SubRedisLoging(string msg)
        {
            Console.WriteLine($"订阅者 1 从 队列{RedisMqKey.Loging} 消费到/接受到 消息:{msg}");

            await Task.CompletedTask;
        }
    }
}
