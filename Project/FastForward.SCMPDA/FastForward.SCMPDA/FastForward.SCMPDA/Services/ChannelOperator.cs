using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FF.Interfaces;
using System.ServiceModel;

namespace FastForward.SCMPDA
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
        ICustService CustServiceObj;
        ITours ToursObj;

        #region Service Channel
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
                //Added By Prabhath on 20/02/2013
                //FalutedChannelOpen<ISecurity>(securityObj,"SecurityEndPoint");
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
                //Added By Prabhath on 20/02/2013
                //FalutedChannelOpen<IGeneral>(generalObj,"GeneralEndPoint");
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
                //Added By Prabhath on 20/02/2013
                //FalutedChannelOpen<ICommonSearch>(commonSearchObj,"CommonSearchEndPoint");
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
                //Added By Prabhath on 20/02/2013
                //FalutedChannelOpen<IInventory>(inventoryObj,"InventoryEndPoint");
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
                //Added By Prabhath on 20/02/2013
                //FalutedChannelOpen<ISales>(salesObj,"SalesEndPoint");
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
                //Added By Prabhath on 20/02/2013
                //FalutedChannelOpen<IFinancial>(FinancialObj,"FinancialEndPoint");
                return FinancialObj;


            }
        }

        //Customer Service Chanel
        public ICustService CustService
        {
            get
            {
                if (CustServiceObj == null)
                {
                    ChannelFactory<ICustService> CustServiceChannel = new ChannelFactory<ICustService>("CustServiceEndPoint");
                    chanelList.Add(CustServiceChannel);
                    CustServiceObj = CustServiceChannel.CreateChannel();

                }
                //Added By Tharka on 30-Sep-2013
                //FalutedChannelOpen<IFinancial>(CustServiceObj,"CustServiceEndPoint");
                return CustServiceObj;
            }
        }

        //Abans Tours Chanel
        public ITours Tours
        {
            get
            {
                if (ToursObj == null)
                {
                    ChannelFactory<ITours> ToursChannel = new ChannelFactory<ITours>("ToursEndPoint");
                    chanelList.Add(ToursChannel);
                    ToursObj = ToursChannel.CreateChannel();

                }
                //Added By Tharka on 30-Sep-2013
                //FalutedChannelOpen<IFinancial>(CustServiceObj,"CustServiceEndPoint");
                return ToursObj;
            }
        }


        #endregion

        //public void CloseAllChannels()
        //{
        //    //try
        //    //{
        //    foreach (ChannelFactory chFac in chanelList)
        //    {
        //        //if (chFac.State == CommunicationState.Opened)
        //        //{
        //            chFac.Abort();
        //        //}
        //    }

        //    securityObj = null;
        //    generalObj = null;
        //    inventoryObj = null;
        //    commonSearchObj = null;
        //    salesObj = null;
        //    FinancialObj = null;

        //    GC.Collect(); 
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //}
        //}

        public void CloseAllChannels()
        {

        }


        //Written By Prabhath on 20/02/2013
        public void FalutedChannelOpen<T>(T _falutedChannel, string _endPoint) where T : class
        {
            System.ServiceModel.ICommunicationObject _communicationObj = (ICommunicationObject)_falutedChannel;
            if (_communicationObj.State == CommunicationState.Faulted)
            {
                _communicationObj.Open();
            }
        }

        public void CloseChannel()
        {
            if (generalObj != null && ((((IClientChannel)generalObj).State == CommunicationState.Faulted) || (((IClientChannel)generalObj).State == CommunicationState.Closed)))
            { generalObj = null; }
            if (inventoryObj != null && ((((IClientChannel)inventoryObj).State == CommunicationState.Faulted) || (((IClientChannel)inventoryObj).State == CommunicationState.Closed)))
            { inventoryObj = null; }
            if (salesObj != null && ((((IClientChannel)salesObj).State == CommunicationState.Faulted) || (((IClientChannel)salesObj).State == CommunicationState.Closed)))
            { salesObj = null; }
            if (commonSearchObj != null && ((((IClientChannel)commonSearchObj).State == CommunicationState.Faulted) || (((IClientChannel)commonSearchObj).State == CommunicationState.Closed)))
            { commonSearchObj = null; }
            if (FinancialObj != null && ((((IClientChannel)FinancialObj).State == CommunicationState.Faulted) || (((IClientChannel)FinancialObj).State == CommunicationState.Closed)))
            { FinancialObj = null; }
            if (securityObj != null && ((((IClientChannel)securityObj).State == CommunicationState.Faulted) || (((IClientChannel)securityObj).State == CommunicationState.Closed)))
            { securityObj = null; }
        }
    }
}
