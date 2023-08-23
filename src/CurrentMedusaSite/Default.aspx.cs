using CurrentMedusaSite.Domain;
using IntegrationContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CurrentMedusaSite
{
    public partial class _Default : Page
    {
        // This property will be set for you by the PropertyInjectionModule.
        public IEventBus EventBus { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected async void Unnamed_ServerClick(object sender, EventArgs e)
        {
            var domain = new WorkorderDomain(EventBus);
            await domain.SubmitChanges();

        }
    }
}
