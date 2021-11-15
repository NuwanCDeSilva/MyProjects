using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace FastForward.WebASYCUDA.Services
{
    public class ChannelOperator
    {
        List<ChannelFactory> chanelList = new List<ChannelFactory>();
        ISecurity securityObj;
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

    }
}