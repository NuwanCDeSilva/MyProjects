using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : This is a UI specific BO class for Common Approval Cycle.
    /// Created By : Miginda Geeganage.
    /// Created On : 20/04/2012
    /// </summary>
    public class ApprovalStatus
    {
        #region Private Members

        private string _documentNo = string.Empty;
        private string _documentType = string.Empty;
        private string _remarks = string.Empty;
        
        #endregion

        #region Public Property Definition

        public string DocumentNo
        {
            get { return _documentNo; }
            set { _documentNo = value; }
        }

        public string DocumentType
        {
            get { return _documentType; }
            set { _documentType = value; }
        }

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        #endregion
    }
}
