using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace FastForward.Logistic.Services
{
    public class ChannelOperator
    {
        List<ChannelFactory> chanelList = new List<ChannelFactory>();
        ISecurity securityObj;
        IGenaral generalObj;
        ISales salesObj;
        ICommonSearch CommonSearchObj;

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

        public IGenaral General
        {
            get
            {
                if (generalObj == null)
                {
                    ChannelFactory<IGenaral> generalchannel = new ChannelFactory<IGenaral>("GenaralEndPoint");
                    chanelList.Add(generalchannel);
                    generalObj = generalchannel.CreateChannel();

                }
                return generalObj;
            }
        }

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

        public ICommonSearch CommonSearch
        {
            get
            {
                if (CommonSearchObj == null)
                {
                    ChannelFactory<ICommonSearch> CommonSearchchannel = new ChannelFactory<ICommonSearch>("CommonSearchEndPoint");
                    chanelList.Add(CommonSearchchannel);
                    CommonSearchObj = CommonSearchchannel.CreateChannel();

                }
                return CommonSearchObj;
            }
        }
        public void CloseChannel()
        {

            if (securityObj != null && ((((IClientChannel)securityObj).State == CommunicationState.Faulted) || (((IClientChannel)securityObj).State == CommunicationState.Closed)))
            { securityObj = null; }

           
            if (salesObj != null && ((((IClientChannel)salesObj).State == CommunicationState.Faulted) || (((IClientChannel)salesObj).State == CommunicationState.Closed)))
            { salesObj = null; }

            if (CommonSearchObj != null && ((((IClientChannel)CommonSearchObj).State == CommunicationState.Faulted) || (((IClientChannel)CommonSearchObj).State == CommunicationState.Closed)))
            { CommonSearchObj = null; }

            if (generalObj != null && ((((IClientChannel)generalObj).State == CommunicationState.Faulted) || (((IClientChannel)generalObj).State == CommunicationState.Closed)))
            { generalObj = null; }

        }
    
    }
}