using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class PromoVouRedeemPB
    {
        #region Private Members
        private string _SPRPB_USER;
        private string _SPRPB_COMP;
        private string _SPRPB_PB;
        private string _SPRPB_LVL;
        private Int32 _SPRPB_PERIOD;


        #endregion

        public string SPRPB_USER
        {
            get { return _SPRPB_USER; }
            set { _SPRPB_USER = value; }
        }
        public string SPRPB_COMP
        {
            get { return _SPRPB_COMP; }
            set { _SPRPB_COMP = value; }
        }
        public string SPRPB_PB
        {
            get { return _SPRPB_PB; }
            set { _SPRPB_PB = value; }
        }
        public string SPRPB_LVL
        {
            get { return _SPRPB_LVL; }
            set { _SPRPB_LVL = value; }
        }
        public Int32 SPRPB_PERIOD
        {
            get { return _SPRPB_PERIOD; }
            set { _SPRPB_PERIOD = value; }
        }


    }
}

