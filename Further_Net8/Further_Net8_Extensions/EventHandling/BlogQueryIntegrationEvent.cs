using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Further_Net8_Common.EventBus;

namespace Further_Net8_Extensions.EventHandling
{
    public class BlogQueryIntegrationEvent : IntegrationEvent
    {
        public string BlogId { get; private set; }

        public BlogQueryIntegrationEvent(string blogid)
            => BlogId = blogid;
    }
}
