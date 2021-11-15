using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class TechAllocation
    {
        #region private members
        private string _jobNo;
        private string _engineNo;
        private string _chassisNo;
        private string _empName;
        private DateTime _fromDate;
        private DateTime _toDate;
        private int _status;
        private string _company;
        private string _location;
        private int _lineNo;

        private string _sessionCreateBy;
        private DateTime _sessionCreateDate;
        private string _sessionModBy;
        private DateTime _sessionModDate;
        private string _sessionId;
        private string _techCode;
        private string _sth_TP;
        private string _alocNo;
        private string _townNo;
        #endregion

        public string JobNo
        {
            get { return _jobNo; }
            set { _jobNo = value; }
        }
        public string EngineNo
        {
            get { return _engineNo; }
            set { _engineNo = value; }
        }
        public string ChassisNo
        {
            get { return _chassisNo; }
            set { _chassisNo = value; }
        }
        public string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
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
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public int LineNo
        {
            get { return _lineNo; }
            set { _lineNo = value; }
        }
        public string sessionCreateBy
        {
            get { return _sessionCreateBy; }
            set { _sessionCreateBy = value; }
        }
        public DateTime sessionCreateDate
        {
            get { return _sessionCreateDate; }
            set { _sessionCreateDate = value; }
        }
        public string sessionModBy
        {
            get { return _sessionModBy; }
            set { _sessionModBy = value; }
        }
        public DateTime sessionModDate
        {
            get { return _sessionModDate; }
            set { _sessionModDate = value; }
        }
        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
        public string TechCode
        {
            get { return _techCode; }
            set { _techCode = value; }
        }
        public string STH_TP
        {
            get { return _sth_TP; }
            set { _sth_TP = value; }
        }
        public string AlcoNo
        {
            get { return _alocNo; }
            set { _alocNo = value; }
        }
        public string TownNo
        {
            get { return _townNo; }
            set { _townNo = value; }
        }
    }
}
