using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
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
            services.Add(new ServiceHost((typeof(GeneralBLL))));//port 7001
            services.Add(new ServiceHost((typeof(InventoryBLL))));//port 7002
            services.Add(new ServiceHost((typeof(CommonSearchBLL))));//port 7003
            services.Add(new ServiceHost((typeof(SalesBLL))));//port 7004
            services.Add(new ServiceHost((typeof(FinancialBLL))));//port 7005
            services.Add(new ServiceHost((typeof(CustServiceBLL))));//port 7006
            services.Add(new ServiceHost((typeof(MsgPortalBLL))));//port 7007
            services.Add(new ServiceHost((typeof(ToursBLL))));//port 7008
            services.Add(new ServiceHost((typeof(DashboardBLL))));//port 7008
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
