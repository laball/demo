using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;

namespace AbpSignalrApi.Signalr
{
    public interface IDispatcher : ISingletonDependency
    {
        void SendMessage(string message);
    }
}
