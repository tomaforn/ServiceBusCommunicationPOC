using IntegrationContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CurrentMedusaSite.Domain
{
    public class WorkorderDomain
    {
        private readonly IEventBus _eventBus;
        public WorkorderDomain(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task SubmitChanges()
        {
            await _eventBus.PublishAsync(
                new WorkOrderCreatedIntegrationEvent()
                {
                    Id = 45,
                    Name = "My new workorder"
                });
        }
    }
}
