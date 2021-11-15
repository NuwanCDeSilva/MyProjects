using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebASYCUDA.Models
{
        public class DocumentModel
        {
        
        
            private string _doc_number = string.Empty;
            private DatabaseModel _db_model = null;
            private DocGroup _doc_grp = null;
            private DocType _doc_type = null;
            public string Doc_Number { get { return _doc_number; } set { _doc_number = value; } }
            public DatabaseModel DBModelList { get { return _db_model; } set { _db_model = value; } }
            public DocGroup DocGrpList { get { return _doc_grp; } set { _doc_grp = value; } }
            public DocType DocTypeList { get { return _doc_type; } set { _doc_type = value; } } 
       
        }
        public class DatabaseModel 
        {
            private string _doc_database_id = string.Empty;
            private string _doc_database_name = string.Empty;
            public string Doc_Database_Id { get { return _doc_database_id; } set { _doc_database_id = value; } }
            public string Doc_Database_Name { get { return _doc_database_name; } set { _doc_database_name = value; } }
        }
        public class DocGroup 
        {
            private string _doc_grp_name = string.Empty;
            private string _doc_grp_name_id = string.Empty;
            public string Doc_Grp_Name { get { return _doc_grp_name; } set { _doc_grp_name = value; } }
            public string Doc_Grp_Name_Id { get { return _doc_grp_name_id; } set { _doc_grp_name_id = value; } }
        }

        public class DocType 
        {
            private string _doc_type_name = string.Empty;
            private string _doc_type_id = string.Empty;
            private string _doc_type_description = string.Empty;
            public string Doc_Type_Name { get { return _doc_type_name; } set { _doc_type_name = value; } }
            public string Doc_Type_Id { get { return _doc_type_id; } set { _doc_type_id = value; } }
            public string Doc_Type_Description { get { return _doc_type_description; } set { _doc_type_description = value; } }
        }
    
}