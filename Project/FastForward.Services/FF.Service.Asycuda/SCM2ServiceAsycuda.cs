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
using FF.Interfaces;  

namespace FF.Service.Asycuda
{
    public partial class SCM2ServiceAsycuda : ServiceBase
    {
        internal static ServiceHost myServiceHost = null;
        public SCM2ServiceAsycuda()
        {
            InitializeComponent();
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
