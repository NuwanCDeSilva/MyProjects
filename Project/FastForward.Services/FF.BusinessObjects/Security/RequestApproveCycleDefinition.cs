using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Approve Cycle Definition.
    /// Created By : Miginda Geeganage.
    /// Created On : 16/04/2012 
    /// </summary>
    public class RequestApproveCycleDefinition
    {
        #region Private Members

        int _seqNo = 0;    
        string _sourceType = string.Empty;
        string _sourceTypeCode = string.Empty;
        string _reqSubTypeCode = string.Empty;     
        string _destinationType = null;
        string _destinationTypeCode = null;
        bool _isApprovalNeeded = true;
        DateTime _fromDate = DateTime.MinValue;     
        DateTime _toDate = DateTime.MinValue;
        string _createdBy = string.Empty;     
        DateTime _createdDate = DateTime.MinValue;
        string _modifiedBy = string.Empty;
        DateTime _modifiedDate = DateTime.MinValue;
        string _sessionId = string.Empty;

        //UI specific propertise.
        UserRequestApprovePermission _userRequestApprovePermission = null;
        DateTime _transactionDate = DateTime.MinValue;     

        #endregion

        #region Public Property Definition

        public int SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        public string SourceType
        {
            get { return _sourceType; }
            set { _sourceType = value; }
        }

        public string SourceTypeCode
        {
            get { return _sourceTypeCode; }
            set { _sourceTypeCode = value; }
        }

        public string ReqSubTypeCode
        {
            get { return _reqSubTypeCode; }
            set { _reqSubTypeCode = value; }
        }

        public string DestinationType
        {
            get { return _destinationType; }
            set { _destinationType = value; }
        }

        public string DestinationTypeCode
        {
            get { return _destinationTypeCode; }
            set { _destinationTypeCode = value; }
        }

        public bool IsApprovalNeeded
        {
            get { return _isApprovalNeeded; }
            set { _isApprovalNeeded = value; }
        }

        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set { _transactionDate = value; }
        }

        public UserRequestApprovePermission UserRequestApprovePermission
        {
            get { return _userRequestApprovePermission; }
            set { _userRequestApprovePermission = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        #endregion

    }
}
