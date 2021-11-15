using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Priority Hierarchy.
    /// Created By : Miginda Geeganage.
    /// Created On : 23/04/2012 
    /// </summary>
    public class PriorityHierarchy
    {
        #region Private Members

        string _companyCode = string.Empty;
        string _locationCode = string.Empty;
        string _hierarchyType = string.Empty;
        string _hierarchyItemName = string.Empty;
        string _hierarchyItemValue = string.Empty;
        bool _hierarchyItemIsActive = false;
        int _hierarchyItemPriorityLevel = 0;

        #endregion

        #region Public Property Definition

        public string CompanyCode
        {
            get { return _companyCode; }
            set { _companyCode = value; }
        }
        
        public string LocationCode
        {
            get { return _locationCode; }
            set { _locationCode = value; }
        }
        
        public string HierarchyType
        {
            get { return _hierarchyType; }
            set { _hierarchyType = value; }
        }       

        public string HierarchyItemName
        {
            get { return _hierarchyItemName; }
            set { _hierarchyItemName = value; }
        }
        
        public string HierarchyItemValue
        {
            get { return _hierarchyItemValue; }
            set { _hierarchyItemValue = value; }
        }
        
        public bool HierarchyItemIsActive
        {
            get { return _hierarchyItemIsActive; }
            set { _hierarchyItemIsActive = value; }
        }
        
        public int HierarchyItemPriorityLevel
        {
            get { return _hierarchyItemPriorityLevel; }
            set { _hierarchyItemPriorityLevel = value; }
        }

        #endregion

    }
}
