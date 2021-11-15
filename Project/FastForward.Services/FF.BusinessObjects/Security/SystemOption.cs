using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    public class SystemOption
    {
        //
        // Function            - System Option Registration
        // Function Wriiten By - P.Wijetunge
        // Date                - 29/02/2012
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members

        private Int32? _ssp_act;
        private string _ssp_desc;
        private Int32? _ssp_isenabled;
        private Int32? _ssp_ishide;
        private Int32 _ssp_optid;
        private Int32? _ssp_orgposition;
        private Int32 _ssp_parentid;
        private string _ssp_title;
        private string _ssp_url;
        private Int32 _is_allowbackdt;
        private string _opttp;
        private Int32 _needdayend;
       
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        #region Definition - Properties

        public Int32? Ssp_act
        {

            get { return _ssp_act; }
            set { _ssp_act = value; }
        }
        public string Ssp_desc
        {
            get { return _ssp_desc; }
            set {_ssp_desc = value; }
        }
        public Int32? Ssp_isenabled
        {
            get { return _ssp_isenabled; }
            set { _ssp_isenabled = value; }
        }
        public Int32? Ssp_ishide
        {
            get { return _ssp_ishide; }
            set { if (value == null) value = 0; else _ssp_ishide = value; }
        }
        public Int32 Ssp_optid
        {
            get { return _ssp_optid; }
            set { _ssp_optid = value; }
        }
        public Int32? Ssp_orgposition
        {
            get { return _ssp_orgposition; }
            set { _ssp_orgposition = value; }
        }
        public Int32 Ssp_parentid
        {
            get { return _ssp_parentid; }
            set { _ssp_parentid = value; }
        }
        public string Ssp_title
        {
            get { return _ssp_title; }
            set { _ssp_title = value; }
        }
        public string Ssp_url
        {
            get { return _ssp_url; }
            set { _ssp_url = value; }
        }
        public Int32 Is_allowbackdt
        {
            get { return _is_allowbackdt; }
            set { _is_allowbackdt = value; }
        }
        public string Opttp
        {
            get { return _opttp; }
            set { _opttp = value; }
        }
        public Int32 Needdayend
        {
            get { return _needdayend; }
            set { _needdayend = value; }
        }

        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped system options</returns>
        #region Converter
        public static SystemOption ConvertTotal(DataRow row)
        {
            return new SystemOption
            {
                Ssp_act = row["SSP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSP_ACT"]),
                Ssp_desc = row["SSP_DESC"] == DBNull.Value ? string.Empty : row["SSP_DESC"].ToString (),
                Ssp_isenabled = row["SSP_ISENABLED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSP_ISENABLED"]),
                Ssp_ishide =  row["SSP_ISHIDE"]==DBNull.Value?  0: Convert.ToInt32 (row["SSP_ISHIDE"]),
                Ssp_optid = row["SSP_OPTID"]==DBNull.Value ?0:Convert.ToInt32 ( row["SSP_OPTID"]),
                Ssp_orgposition = row["SSP_ORGPOSITION"]==DBNull.Value ?0:Convert.ToInt32 ( row["SSP_ORGPOSITION"]),
                Ssp_parentid = row["SSP_PARENTID"]==DBNull.Value ?0:Convert.ToInt32 ( row["SSP_PARENTID"]),
                Ssp_title = row["SSP_TITLE"] == DBNull.Value ? string.Empty : row["SSP_TITLE"].ToString (),
                Ssp_url = row["SSP_URL"] == DBNull.Value ? string.Empty : row["SSP_URL"].ToString (),
                Is_allowbackdt = row["SSP_ISALLOWBACKDT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSP_ISALLOWBACKDT"]),
                Opttp = row["SSP_OPTTP"] == DBNull.Value ? string.Empty : row["SSP_OPTTP"].ToString(),
                Needdayend = row["SSP_NEEDDAYEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSP_NEEDDAYEND"])
            };
        }
        #endregion
    }
}

