using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using FF.BusinessLogicLayer;
using FF.Interfaces;

namespace FF.Service.CustService
{
    public partial class SCM2ServiceCustService : ServiceBase
    {
        
        internal static ServiceHost myServiceHost = null;
        public SCM2ServiceCustService()
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
                myServiceHost = new ServiceHost(typeof(CustServiceBLL));
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


