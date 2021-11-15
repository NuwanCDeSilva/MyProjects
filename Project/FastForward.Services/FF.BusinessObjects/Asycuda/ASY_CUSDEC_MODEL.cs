using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_CUSDEC_MODEL
    {
        private string _doc_number = string.Empty;
        private ASY_DB_SOURCE _db_model = null;
        private ASY_DOC_GRUP _doc_grp = null;
        private ASY_DOC_TP _doc_type = null;
        private string _doc_number_to = string.Empty;
        public string Doc_Number { get { return _doc_number; } set { _doc_number = value; } }
        public ASY_DB_SOURCE DBModelList { get { return _db_model; } set { _db_model = value; } }
        public ASY_DOC_GRUP DocGrpList { get { return _doc_grp; } set { _doc_grp = value; } }
        public ASY_DOC_TP DocTypeList { get { return _doc_type; } set { _doc_type = value; } }

        //Added By Dulaj 2018/Jul/09
        public string Doc_Number_To { get { return _doc_number_to; } set { _doc_number_to = value; } }
    }
}
