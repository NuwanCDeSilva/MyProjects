using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FF.BusinessLogicLayer;


namespace FF.LGSTService.Security
{
    public partial class LGSTServiceSecurity : ServiceBase
    {
        internal static ServiceHost myServiceHost = null;

        public LGSTServiceSecurity()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (myServiceHost != null)
                {
                    myServiceHost.Close();
                }
                myServiceHost = new ServiceHost(typeof(SecurityBLL));
                myServiceHost.Open();
            }
            catch (Exception ex)
            {
                this.EventLog.WriteEntry(this.ServiceName + " Failed to start " + ex.Message, EventLogEntryType.Error);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            if (myServiceHost != null)
            {
                myServiceHost.Close();
                myServiceHost = null;
            }
        }
    }
}
