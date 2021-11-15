using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class SystemRoleOption
    {
        #region Private Members

        SystemRole _systemRole = null;
        SystemOption _systemOption = null;
        List<SystemOption> _systemOptionList = null;
        private int _isActive = 0;
        private string _createdBy = string.Empty;
        private string _title = string.Empty;
        private string _url = string.Empty;
        #endregion

        #region Public Property Definition

        public SystemRole SystemRole
        {
            get { return _systemRole; }
            set { _systemRole = value; }
        }

        public SystemOption SystemOption
        {
            get { return _systemOption; }
            set { _systemOption = value; }
        }

        public List<SystemOption> SystemOptionList
        {
            get { return _systemOptionList; }
            set { _systemOptionList = value; }
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


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        #endregion
    }
}
