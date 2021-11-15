using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Inventory Request Item.
    /// Created By : kapila
    /// Created On : 7/6/2017
    /// </summary>
    [Serializable]
    public class InventoryReqItemBal : MasterItem
    {

        #region Private Members
        private Int32 _IRB_SEQ = 0;
        private string _IRB_RT_CD = string.Empty;
        private string _IRB_ITM = string.Empty;
        private Int32 _IRB_REQ_LINE = 0;
        private Int32 _IRB_LINE = 0;
        private string _IRB_LOC = string.Empty;
        private string _IRB_STUS = string.Empty;
        private Int32 _IRB_QTY = 0;

        #endregion

        #region Public Property Definition
        public Int32 IRB_SEQ { get { return _IRB_SEQ; } set { _IRB_SEQ = value; } }
        public string IRB_RT_CD { get { return _IRB_RT_CD; } set { _IRB_RT_CD = value; } }
        public string IRB_ITM { get { return _IRB_ITM; } set { _IRB_ITM = value; } }
        public string IRB_LOC { get { return _IRB_LOC; } set { _IRB_LOC = value; } }
        public string IRB_STUS { get { return _IRB_STUS; } set { _IRB_STUS = value; } }
        public Int32 IRB_REQ_LINE { get { return _IRB_REQ_LINE; } set { _IRB_REQ_LINE = value; } }
        public Int32 IRB_LINE { get { return _IRB_LINE; } set { _IRB_LINE = value; } }
        public Int32 IRB_QTY { get { return _IRB_QTY; } set { _IRB_QTY = value; } }

        #endregion

      

    }
}
