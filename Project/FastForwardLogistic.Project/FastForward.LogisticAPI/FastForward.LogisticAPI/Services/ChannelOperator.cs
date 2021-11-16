using FF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace FastForward.LogisticAPI.Services
{
   
    public class ChannelOperator
        {
            List<ChannelFactory> chanelList = new List<ChannelFactory>();
            ISecurity securityObj;
            IGenaral generalObj;
            ICommonSearch commonSearchObj;
            ISales salesObj;

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
        }

}