using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Further_Net8_Common.EventBus
{
    /// <summary>
    /// 集成事件处理程序
    /// 泛型接口
    /// </summary>
    /// <typeparam name="TIntegrationEvent"></typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
       where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// 集成事件处理程序
    /// 基 接口
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}
