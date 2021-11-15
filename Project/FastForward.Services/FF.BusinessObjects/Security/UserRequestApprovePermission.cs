using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for User/User Location approval permission.
    /// Created By : Miginda Geeganage.
    /// Created On : 07/04/2012 
    /// </summary>
    public class UserRequestApprovePermission
    {
        #region Private Members

        string _userId = string.Empty;
        string _type = string.Empty;      
        string _typeCode = string.Empty;  
        string _userPermissionCode = string.Empty;
        int _userPermissionLevel = 0;
        string _requestMainType = string.Empty;
        string _requestSubType = string.Empty;
        string _description = string.Empty;
        int _requestApproveLevel = 0;
        int _requestLevel = 0;
        int _maxApproveLimit = 0;
        decimal _valueLimit = 0;

        #endregion

        #region Public Property Definition
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        public string UserPermissionCode
        {
            get { return _userPermissionCode; }
            set { _userPermissionCode = value; }
        }

        public int UserPermissionLevel
        {
            get { return _userPermissionLevel; }
            set { _userPermissionLevel = value; }
        }
       
        public string RequestMainType
        {
            get { return _requestMainType; }
            set { _requestMainType = value; }
        }

        public string RequestSubType
        {
            get { return _requestSubType; }
            set { _requestSubType = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
       
        public int RequestApproveLevel
        {
            get { return _requestApproveLevel; }
            set { _requestApproveLevel = value; }
        }      

        public int RequestLevel
        {
            get { return _requestLevel; }
            set { _requestLevel = value; }
        }

        public int MaxApproveLimit
        {
            get { return _maxApproveLimit; }
            set { _maxApproveLimit = value; }
        }

        public decimal ValueLimit
        {
            get { return _valueLimit; }
            set { _valueLimit = value; }
        }

        //Business logic propertise.
        //Can approve request,users those who have permission greater request level and less than to RequestApproveLevel.
        public bool IsApprovalUser
        {           
            get { return ((RequestLevel < UserPermissionLevel) && (UserPermissionLevel < RequestApproveLevel)) ? true : false; }
        }

        //Can generate request,users those who have permission equal to Request_level.
        public bool IsRequestGenerateUser
        {
            get { return (UserPermissionLevel <= RequestLevel) ? true : false; }
        }

        //If the UserPermissionLevel is greater than or equal to RequestApproveLevel, that user is final approval user.
        public bool IsFinalApprovalUser
        {
            get { return (UserPermissionLevel >= RequestApproveLevel) ? true : false; }
        }

        #endregion


       

    }
}
