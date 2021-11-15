using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using FF.BusinessLogicLayer;
using FF.Interfaces;
using System.ServiceModel;

namespace FF.Service.General
{
    public partial class SCM2ServiceGeneral : ServiceBase
    {
        internal static ServiceHost myServiceHost = null;
        public SCM2ServiceGeneral()
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
                myServiceHost = new ServiceHost(typeof(GeneralBLL));
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
