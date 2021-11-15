using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ReceiptAddDetails
    {


        #region Private Members
        private string _SRA_REC_NO;
        private string _SRA_REF_NO;
        private string _SRA_TP;
        private string _SRA_OTH_TP;
        private string _SRA_RMKS;
        private string _SRA_ANAL1;
        private string _SRA_ANAL2;
        private string _SRA_ANAL3;
        private string _SRA_CRE_BY;
        private Int32 _SRA_SEQ;
        private Int32 _SRA_LINE_NO;
        private Int32 _SRA_UNITS;
        private decimal _SRA_AMT;
        private Int32 _SRA_ANAL5;
        private DateTime _SRA_REQ_DT;
        private DateTime _SRA_ANAL4;
        private DateTime _SRA_CRE_DT;
        #endregion

        public DateTime SRA_CRE_DT { get { return _SRA_CRE_DT; } set { _SRA_CRE_DT = value; } }
        public DateTime SRA_REQ_DT { get { return _SRA_REQ_DT; } set { _SRA_REQ_DT = value; } }
        public DateTime SRA_ANAL4 { get { return _SRA_ANAL4; } set { _SRA_ANAL4 = value; } }
        public Int32 SRA_SEQ { get { return _SRA_SEQ; } set { _SRA_SEQ = value; } }
        public Int32 SRA_LINE_NO { get { return _SRA_LINE_NO; } set { _SRA_LINE_NO = value; } }
        public Int32 SRA_UNITS { get { return _SRA_UNITS; } set { _SRA_UNITS = value; } }
        public decimal SRA_AMT { get { return _SRA_AMT; } set { _SRA_AMT = value; } }
        public Int32 SRA_ANAL5 { get { return _SRA_ANAL5; } set { _SRA_ANAL5 = value; } }

        public string SRA_REC_NO { get { return _SRA_REC_NO; } set { _SRA_REC_NO = value; } }
        public string SRA_REF_NO { get { return _SRA_REF_NO; } set { _SRA_REF_NO = value; } }
        public string SRA_TP { get { return _SRA_TP; } set { _SRA_TP = value; } }
        public string SRA_OTH_TP { get { return _SRA_OTH_TP; } set { _SRA_OTH_TP = value; } }
        public string SRA_RMKS { get { return _SRA_RMKS; } set { _SRA_RMKS = value; } }
        public string SRA_ANAL1 { get { return _SRA_ANAL1; } set { _SRA_ANAL1 = value; } }
        public string SRA_ANAL2 { get { return _SRA_ANAL2; } set { _SRA_ANAL2 = value; } }
        public string SRA_ANAL3 { get { return _SRA_ANAL3; } set { _SRA_ANAL3 = value; } }
        public string SRA_CRE_BY { get { return _SRA_CRE_BY; } set { _SRA_CRE_BY = value; } }
      

    
    }
}

