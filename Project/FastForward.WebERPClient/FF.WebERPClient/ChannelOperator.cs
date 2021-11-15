using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WebERPClient
{
    public class ChannelOperator
    {  
        List<ChannelFactory> chanelList = new List<ChannelFactory>();
        ISecurity securityObj;
        IGeneral generalObj;
        IInventory inventoryObj;
        ICommonSearch commonSearchObj;
        ISales salesObj;
        IFinancial FinancialObj;


        // Security Service Channel
        public ISecurity Security
        {
            get
            {
                if (securityObj == null)
                {
                    ChannelFactory<ISecurity> securitychannel = new ChannelFactory<ISecurity>("SecurityEndPoint");
                    chanelList.Add(securitychannel);
                    securityObj = securitychannel.CreateChannel();

                }
                return securityObj;
            }
        }

        // General Service Channel
        public IGeneral General
        {
            get
            {
                if (generalObj == null)
                {
                    ChannelFactory<IGeneral> generalchannel = new ChannelFactory<IGeneral>("GeneralEndPoint");
                    chanelList.Add(generalchannel);
                    generalObj = generalchannel.CreateChannel();

                }
                return generalObj;
            }
        }


        // CommonSearch Service Channel
        public ICommonSearch CommonSearch
        {
            get
            {
                if (commonSearchObj == null)
                {
                    ChannelFactory<ICommonSearch> commonsearchchannel = new ChannelFactory<ICommonSearch>("CommonSearchEndPoint");
                    chanelList.Add(commonsearchchannel);
                    commonSearchObj = commonsearchchannel.CreateChannel();

                }
                return commonSearchObj;
            }
        }


        // Inventory Service Channel
        public IInventory Inventory
        {
            get
            {
                if (inventoryObj == null)
                {
                    ChannelFactory<IInventory> inventorychannel = new ChannelFactory<IInventory>("InventoryEndPoint");
                    chanelList.Add(inventorychannel);
                    inventoryObj = inventorychannel.CreateChannel();

                }
                return inventoryObj;
            }
        }

        //Sales Service Channel
        public ISales Sales
        {
            get
            {
                if (salesObj == null)
                {
                    ChannelFactory<ISales> saleschannel = new ChannelFactory<ISales>("SalesEndPoint");
                    chanelList.Add(saleschannel);
                    salesObj = saleschannel.CreateChannel();

                }
                return salesObj;
            }
        }

        //Financial Service Channel
        public IFinancial Financial
        {
            get
            {
                if (FinancialObj == null)
                {
                    ChannelFactory<IFinancial> financialchannel = new ChannelFactory<IFinancial>("FinancialEndPoint");
                    chanelList.Add(financialchannel);
                    FinancialObj = financialchannel.CreateChannel();

                }
                return FinancialObj;
               

            }
        }

        public void CloseAllChannels()
        {
            try
            {
                foreach (ChannelFactory chFac in chanelList)
                {
                    if (chFac.State == CommunicationState.Opened)
                        chFac.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
