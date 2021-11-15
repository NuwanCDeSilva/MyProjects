using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace FastForward.WebAbansTours.Services
{
    public class ChannelOperator
    {
        List<ChannelFactory> chanelList = new List<ChannelFactory>();
        ISecurity securityObj;
        IGeneral genaralObj;
        ITours toursObj;
        ISales salesObj;
        ICommonSearch comSearchObj;
        IInventory inventoryObj;
        ICustService CustServiceObj;

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
                return securityObj;
            }
        }

        // Security Service Channel
        public IGeneral General
        {
            get
            {
                if (genaralObj == null)
                {
                    ChannelFactory<IGeneral> genaralchannel = new ChannelFactory<IGeneral>("GeneralEndPoint");
                    chanelList.Add(genaralchannel);
                    genaralObj = genaralchannel.CreateChannel();

                }
                return genaralObj;
            }
        }
        // Tours Service Channel
        public ITours Tours
        {
            get
            {
                if (toursObj == null)
                {
                    ChannelFactory<ITours> tourschannel = new ChannelFactory<ITours>("ToursEndPoint");
                    chanelList.Add(tourschannel);
                    toursObj = tourschannel.CreateChannel();

                }
                return toursObj;
            }
        }
        // sales Service Channel
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
        // CommonSearch Service Channel
        public ICommonSearch ComSearch
        {
            get
            {
                if (comSearchObj == null)
                {
                    ChannelFactory<ICommonSearch> comsearchchannel = new ChannelFactory<ICommonSearch>("CommonSearchEndPoint");
                    chanelList.Add(comsearchchannel);
                    comSearchObj = comsearchchannel.CreateChannel();

                }
                return comSearchObj;
            }
        }
        //Inventory Service Chanel
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
        #endregion

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

            if (securityObj != null && ((((IClientChannel)securityObj).State == CommunicationState.Faulted) || (((IClientChannel)securityObj).State == CommunicationState.Closed)))
            { securityObj = null; }

            if (genaralObj != null && ((((IClientChannel)genaralObj).State == CommunicationState.Faulted) || (((IClientChannel)genaralObj).State == CommunicationState.Closed)))
            { genaralObj = null; }

            if (salesObj != null && ((((IClientChannel)salesObj).State == CommunicationState.Faulted) || (((IClientChannel)salesObj).State == CommunicationState.Closed)))
            { salesObj = null; }

            if (toursObj != null && ((((IClientChannel)toursObj).State == CommunicationState.Faulted) || (((IClientChannel)toursObj).State == CommunicationState.Closed)))
            { toursObj = null; }

            if (comSearchObj != null && ((((IClientChannel)comSearchObj).State == CommunicationState.Faulted) || (((IClientChannel)comSearchObj).State == CommunicationState.Closed)))
            { comSearchObj = null; }

            if (inventoryObj != null && ((((IClientChannel)inventoryObj).State == CommunicationState.Faulted) || (((IClientChannel)inventoryObj).State == CommunicationState.Closed)))
            { inventoryObj = null; }

            if (CustServiceObj != null && ((((IClientChannel)CustServiceObj).State == CommunicationState.Faulted) || (((IClientChannel)CustServiceObj).State == CommunicationState.Closed)))
            { CustServiceObj = null; }
        }
    }
}