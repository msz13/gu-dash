using GuDash.Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuDash.Competences.Service.Ports_Adapters.Repositories
{
    public interface IEventStore
    {
        void Connect();

        void AppendEvents();

        string GetEventStream();

    }
}
