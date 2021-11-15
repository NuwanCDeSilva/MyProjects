using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for System Role.
    /// Created By : Miginda Geeganage.
    /// Created On : 24/03/2012
    /// </summary>
    public class SystemRole
    {
        #region Private Members

        private string _companyCode = string.Empty;
        private int _roleId = 0;
        private string _roleName = string.Empty;
        private string _description = string.Empty;
        private int _isActive = 0;
        private string _createdBy = string.Empty;
        private DateTime _createdDate = DateTime.MinValue;
        private string _modifiedBy = string.Empty;
        private DateTime _modifyedDate = DateTime.MinValue;
        private string _sessionId = string.Empty;
        #endregion

        #region Public Property Definition

        public string CompanyCode
        {
            get { return _companyCode; }
            set { _companyCode = value; }
        }
        
        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }
        
        public string RoleName
        {
            get { return _roleName; }
            set { _roleName = value; }
        }
        
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        
        public int IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
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
        
        public DateTime ModifyedDate
        {
            get { return _modifyedDate; }
            set { _modifyedDate = value; }
        }
        
        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
      
        #endregion

        public static SystemRole Converter(DataRow row)
        {
            return new SystemRole
            {

                CompanyCode = ((row["SSR_COMCD"] == DBNull.Value) ? string.Empty : row["SSR_COMCD"].ToString()),
                RoleId = ((row["SSR_ROLEID"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSR_ROLEID"])),
                RoleName = ((row["SSR_ROLENAME"] == DBNull.Value) ? string.Empty : row["SSR_ROLENAME"].ToString()),
                Description = ((row["SSR_DESC"] == DBNull.Value) ? string.Empty : row["SSR_DESC"].ToString()),
                IsActive = ((row["SSR_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSR_ACT"])),
                CreatedBy = ((row["SSR_CRE_BY"] == DBNull.Value) ? string.Empty : row["SSR_CRE_BY"].ToString()),
                CreatedDate = ((row["SSR_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSR_CRE_DT"])),
                ModifiedBy = ((row["SSR_MOD_BY"] == DBNull.Value) ? string.Empty : row["SSR_MOD_BY"].ToString()),
                ModifyedDate = ((row["SSR_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSR_MOD_DT"])),
                SessionId = ((row["SSR_SESSION_ID"] == DBNull.Value) ? string.Empty : row["SSR_SESSION_ID"].ToString())

                //CompanyCode = ((row["SSRR_COMCD"] == DBNull.Value) ? string.Empty : row["SSRR_COMCD"].ToString()),
                //RoleId = ((row["SSRR_ROLEID"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSRR_ROLEID"])),
                //RoleName = ((row["SSRR_ROLENAME"] == DBNull.Value) ? string.Empty : row["SSRR_ROLENAME"].ToString()),
                //Description = ((row["SSRR_DESC"] == DBNull.Value) ? string.Empty : row["SSRR_DESC"].ToString()),
                //IsActive = ((row["SSRR_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSRR_ACT"])),
                //CreatedBy = ((row["SSRR_CRE_BY"] == DBNull.Value) ? string.Empty : row["SSRR_CRE_BY"].ToString()),
                //CreatedDate = ((row["SSRR_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSRR_CRE_DT"])),
                //ModifiedBy = ((row["SSRR_MOD_BY"] == DBNull.Value) ? string.Empty : row["SSRR_MOD_BY"].ToString()),
                //ModifyedDate = ((row["SSRR_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSRR_MOD_DT"])),
                //SessionId = ((row["SSRR_SESSION_ID"] == DBNull.Value) ? string.Empty : row["SSRR_SESSION_ID"].ToString())
            };
        }

        //created by shani 18-05-2013  (for suite new table - sec_sys_role )
        public static SystemRole ConverterNew(DataRow row)
        {
            return new SystemRole
            {

                //CompanyCode = ((row["SSR_COMCD"] == DBNull.Value) ? string.Empty : row["SSR_COMCD"].ToString()),
                //RoleId = ((row["SSR_ROLEID"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSR_ROLEID"])),
                //RoleName = ((row["SSR_ROLENAME"] == DBNull.Value) ? string.Empty : row["SSR_ROLENAME"].ToString()),
                //Description = ((row["SSR_DESC"] == DBNull.Value) ? string.Empty : row["SSR_DESC"].ToString()),
                //IsActive = ((row["SSR_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSR_ACT"])),
                //CreatedBy = ((row["SSR_CRE_BY"] == DBNull.Value) ? string.Empty : row["SSR_CRE_BY"].ToString()),
                //CreatedDate = ((row["SSR_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSR_CRE_DT"])),
                //ModifiedBy = ((row["SSR_MOD_BY"] == DBNull.Value) ? string.Empty : row["SSR_MOD_BY"].ToString()),
                //ModifyedDate = ((row["SSR_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSR_MOD_DT"])),
                //SessionId = ((row["SSR_SESSION_ID"] == DBNull.Value) ? string.Empty : row["SSR_SESSION_ID"].ToString())

                CompanyCode = ((row["SSRR_COMCD"] == DBNull.Value) ? string.Empty : row["SSRR_COMCD"].ToString()),
                RoleId = ((row["SSRR_ROLEID"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSRR_ROLEID"])),
                RoleName = ((row["SSRR_ROLENAME"] == DBNull.Value) ? string.Empty : row["SSRR_ROLENAME"].ToString()),
                Description = ((row["SSRR_DESC"] == DBNull.Value) ? string.Empty : row["SSRR_DESC"].ToString()),
                IsActive = ((row["SSRR_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SSRR_ACT"])),
                CreatedBy = ((row["SSRR_CRE_BY"] == DBNull.Value) ? string.Empty : row["SSRR_CRE_BY"].ToString()),
                CreatedDate = ((row["SSRR_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSRR_CRE_DT"])),
                ModifiedBy = ((row["SSRR_MOD_BY"] == DBNull.Value) ? string.Empty : row["SSRR_MOD_BY"].ToString()),
                ModifyedDate = ((row["SSRR_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SSRR_MOD_DT"])),
                SessionId = ((row["SSRR_SESSION_ID"] == DBNull.Value) ? string.Empty : row["SSRR_SESSION_ID"].ToString())
            };
        }

    }
}
