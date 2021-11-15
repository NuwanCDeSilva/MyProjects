using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;

namespace FF.WindowsERPClient.CommonSearch
{
    public partial class RegistrationCollection : FF.WindowsERPClient.Base
    {

    /// <summary>
    /// Written By Prabhath on 15/07/2013
    /// to provide automated registration charge collection entry at the following module as at 15/07/2013
    ///     1. Invoice
    ///     2. Hire Sale
    /// </summary>

        #region Variable - Public
        public List<ReptPickSerials> SerialListForRegistrationAllowItem = null;
        public List<ReptPickSerials> SerialListForRemainItem = null;
        public List<RecieptItem> ReceitItemList = null;
        public bool IsInvalidStatus = false;
        #endregion

        #region Variable - private

        private List<ReptPickSerials> _serialItemLst = null;
        public List<ReptPickSerials> SerialItemLst
        {
            set { _serialItemLst = value; }
        }
        #endregion

        public RegistrationCollection()
        {
            InitializeComponent();
        }
        private List<ReptPickSerials> FilterRegistrationAllowItem()
        {
            if (_serialItemLst == null && _serialItemLst.Count <= 0) { IsInvalidStatus = true; return null; }
            return _serialItemLst.Where(X => CHNLSVC.Inventory.CheckAllocationItemRagistration(X.Tus_itm_cd)).ToList();
        }
        private List<ReptPickSerials> FilterRegistrationDisAllowItem()
        {
            if (_serialItemLst == null && _serialItemLst.Count <= 0) { IsInvalidStatus = true; return null; }
            return _serialItemLst.Where(X => CHNLSVC.Inventory.CheckAllocationItemRagistration(X.Tus_itm_cd)==false).ToList();
        }
        private void BindAllowItem()
        {
            var _bLst = new BindingList<ReptPickSerials>(SerialListForRegistrationAllowItem);
            gvItem.DataSource = _bLst;
        }
        private void AssignRegistrationCharge()
        {
            if (SerialListForRegistrationAllowItem == null || SerialListForRegistrationAllowItem.Count <= 0) { IsInvalidStatus = true; return ; }

        }


    }
}
