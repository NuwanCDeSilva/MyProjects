using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class LoadingBay
    {
        #region Private Members
        private string _userId;
        private string _user;
        private string _locationID;
        private string _location;
        private string _loadingBayID;
        private string _loadingBay;
        private int _Default;
        private int _Active;
        private Int32 _loadBay_line;

        private string _companyCode;
        private string _warehouseCom;
        private string _warehouseLoc;
        private string _sessionCreateBy;
        private DateTime _sessionCreateDate;
        private string _createSession;
        private string _sessionModBy;
        private DateTime _sessionModDate;
        private string _modSession;
        #endregion

        public string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string user1
        {
            get { return _user; }
            set { _user = value; }
        }
        public string location
        {
            get { return _location; }
            set { _location = value; }
        }
        public string locationID
        {
            get { return _locationID; }
            set { _locationID = value; }
        }
        public string loadingBayID
        {
            get { return _loadingBayID; }
            set { _loadingBayID = value; }
        }
        public string loadingBay
        {
            get { return _loadingBay; }
            set { _loadingBay = value; }
        }
        public int Default1
        {
            get { return _Default; }
            set { _Default = value; }
        }
        public int Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        public Int32 LoadBayline
        {
            get { return _loadBay_line; }
            set { _loadBay_line = value; }
        }
        public string companyCode
        {
            get { return _companyCode; }
            set { _companyCode = value; }
        }
        public string warehouseCom
        {
            get { return _warehouseCom; }
            set { _warehouseCom = value; }
        }
        public string warehouseLoc
        {
            get { return _warehouseLoc; }
            set { _warehouseLoc = value; }
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
        public string createSession
        {
            get { return _createSession; }
            set { _createSession = value; }
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
        public string modSession
        {
            get { return _modSession; }
            set { _modSession = value; }
        }

        public static LoadingBay Converter(DataRow row)
        {
            return new LoadingBay
            {
                userId = row["USERID"] == DBNull.Value ? string.Empty : row["USERID"].ToString(),
                user1 = row["USER1"] == DBNull.Value ? string.Empty : row["USER1"].ToString(),
                locationID = row["LOCATIONID"] == DBNull.Value ? string.Empty : row["LOCATIONID"].ToString(),
                location = row["LOCATION"] == DBNull.Value ? string.Empty : row["LOCATION"].ToString(),
                loadingBayID = row["LOADINGBAYID"] == DBNull.Value ? string.Empty : row["LOADINGBAYID"].ToString(),
                loadingBay = row["LOADINGBAY"] == DBNull.Value ? string.Empty : row["LOADINGBAY"].ToString(),
                Default1 = row["DEFAULT1"] == DBNull.Value ? 0 : Convert.ToInt16(row["DEFAULT1"].ToString()),//row["DEFAULT"] == DBNull.Value ? false : Convert.ToBoolean(row["DEFAULT"]),
                Active = row["ACTIVE"] == DBNull.Value ? 0 : Convert.ToInt16(row["ACTIVE"].ToString()),//row["ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(row["ACTIVE"]),
                LoadBayline = row["LOADBAYLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["LOADBAYLINE"]),

                //companyCode = row["COMPANYCODE"] == DBNull.Value ? string.Empty : row["COMPANYCODE"].ToString(),
                //warehouseCom = row["WAREHOUSECOM"] == DBNull.Value ? string.Empty : row["WAREHOUSECOM"].ToString(),
                //warehouseLoc = row["WAREHOUSELOC"] == DBNull.Value ? string.Empty : row["WAREHOUSELOC"].ToString(),
                //sessionCreateBy = row["SESSIONCREATEBY"] == DBNull.Value ? string.Empty : row["SESSIONCREATEBY"].ToString(),
                //sessionCreateDate = row["SESSIONCREATEDATE"]  == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SESSIONCREATEDATE"]),
                //createSession = row["CREATESESSION"] == DBNull.Value ? string.Empty : row["CREATESESSION"].ToString(),
                //sessionModBy = row["SESSIONMODBY"] == DBNull.Value ? string.Empty : row["SESSIONMODBY"].ToString(),
                //sessionModDate = row["SESSIONMODDATE"]  == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SESSIONMODDATE"]),
                //modSession = row["MODSESSION"] == DBNull.Value ? string.Empty : row["MODSESSION"].ToString(),
            };
        }
    }
}
