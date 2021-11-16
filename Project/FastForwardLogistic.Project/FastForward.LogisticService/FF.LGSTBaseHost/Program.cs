using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using FF.BusinessLogicLayer;
using FF.Interfaces;

namespace FF.BaseHost
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ServiceHost> services = new List<ServiceHost>(); 
            #region Define Service Sections

            #region using Services
            services.Add(new ServiceHost((typeof(SecurityBLL))));//port 7000
            services.Add(new ServiceHost((typeof(GenaralBLL))));//port 7001
            services.Add(new ServiceHost((typeof(SalesBLL))));//port 7002
            services.Add(new ServiceHost((typeof(CommonSearchBLL))));//port 7003
            #endregion

            #endregion
            // Start services...
            foreach (ServiceHost host in services)
            {
                host.Open();
                Console.WriteLine(host.BaseAddresses[0] + " started.");
            }


            Console.ReadKey();
        }
    }
}
