using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using FF.BusinessObjects;
using FF.BusinessObjects.Services;
using FF.BusinessObjects.Asycuda;
using System.Globalization;

namespace FF.DataAccessLayer
{
    public class AsycudaDAL : AsyBaseDAL
    {
        public AsycudaDAL(ASY_DB_SOURCE DbSrc = null)
            : base(DbSrc)
        {
            
        }
        
        /// <summary>
        /// get asycuda data related database list
        /// </summary>
        /// <returns>List<ASY_DB_SOURCE></returns>
       public List<ASY_DB_SOURCE> GetSystemDatabaseList()
       {
           try
           {
               List<ASY_DB_SOURCE> _dbList = null;
               DataTable _dtResults = QueryDataTable("tblAsyDbSource", "pkg_asycuda.SP_GET_ASYDBSOURCE", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));

               if (_dtResults.Rows.Count > 0)
               {
                   _dbList = DataTableExtensions.ToGenericList<ASY_DB_SOURCE>(_dtResults, ASY_DB_SOURCE.Converter);
               }
               return _dbList;
           }
           catch (Exception ex) {
               throw ex;
           }
       }
       
        /// <summary>
        /// get asycuda xml group list
        /// </summary>
        /// <returns>List<ASY_DOC_GRUP></returns>
       public List<ASY_DOC_GRUP> GetAsycudaGrpList() 
       {
           try
           {
               List<ASY_DOC_GRUP> _grpList = null;
               DataTable _dtResults = QueryDataTable("tblAsyGroup", "pkg_asycuda.SP_GET_ASYGROUP", CommandType.StoredProcedure, false, new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output));

               if (_dtResults.Rows.Count > 0)
               {
                   _grpList = DataTableExtensions.ToGenericList<ASY_DOC_GRUP>(_dtResults, ASY_DOC_GRUP.Converter);
               }
               return _grpList;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// get asycuda xml type list
        /// </summary>
        /// <param name="_grp">group id</param>
        /// <param name="_database">database source id</param>
        /// <returns>List<ASY_DOC_TP></returns>
       public List<ASY_DOC_TP> GetAsycudaTypeList(string _grp,string _database) 
       {
           try
           {
               List<ASY_DOC_TP> _typList = null;


               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_grpid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = int.Parse(_grp);
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblAsyTypes", "pkg_asycuda.SP_GET_ASYTYPES", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _typList = DataTableExtensions.ToGenericList<ASY_DOC_TP>(_dtResults, ASY_DOC_TP.Converter);
               }
               return _typList;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
      
        /// <summary>
        /// get asycuda header details
        /// </summary>
        /// <param name="docno">document number</param>
        /// <returns>ASY_HEADER_DTLS</returns>
       public ASY_HEADER_DTLS GetAsycudaHederDetails(string docno)
       {
           try
           {
               ASY_HEADER_DTLS _datasrc = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblHeaderData", "pkg_asycudarep.SP_GET_ASYHEDERDETLS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _datasrc = DataTableExtensions.ToGenericList<ASY_HEADER_DTLS>(_dtResults, ASY_HEADER_DTLS.Converter)[0];
               }
               return _datasrc;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// get the related database source details
        /// </summary>
        /// <param name="sourceId">database source id</param>
        /// <returns>ASY_DB_SOURCE</returns>
       public ASY_DB_SOURCE GetDataSourceDetails(int sourceId)
       {
           try
           {
               ASY_DB_SOURCE _datasrc = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_srcid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sourceId;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblSourceData", "pkg_asycuda.SP_GET_ASYDBSOURCE_BY_ID", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _datasrc = DataTableExtensions.ToGenericList<ASY_DB_SOURCE>(_dtResults, ASY_DB_SOURCE.Converter)[0];
               }
               return _datasrc;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// get the office of entry description
        /// </summary>
        /// <param name="OfficeOfEntryCode">office of entry code</param>
        /// <returns>string</returns>
       public string getOfficeOfEntryDescription(string OfficeOfEntryCode)
       {
           try
           {
               string _datasrc = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_ofccde", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OfficeOfEntryCode;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblOfzDesc", "pkg_asycuda.SP_GET_CUSDECOFCENTRYDESC", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _datasrc = _dtResults.Rows[0]["OFC_DESC"].ToString();
               }
               return _datasrc;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get the item locations
        /// </summary>
        /// <param name="locationcode">location code</param>
        /// <returns>string</returns>
       public string getLocationofGoods(string locationcode)
       {
           try
           {
               string _datasrc = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_loccde", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = locationcode;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblLocOfGods", "pkg_asycuda.SP_GET_LOCOFGODS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _datasrc = _dtResults.Rows[0]["LOC_DESC"].ToString();
               }
               else
               {
                   _datasrc = "OTHER";
               }
               return _datasrc;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// get name of the bank using bank code
        /// </summary>
        /// <param name="bnkcode">bank code</param>
        /// <returns>string</returns>
       public string GetBankName(string bnkcode)
       {
           try
           {
               string _datasrc = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_bnkcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bnkcode;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblBank", "pkg_asycudarep.SP_GET_BANKNAME", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _datasrc = _dtResults.Rows[0]["MST_BNK_NAME"].ToString();
               }
               return _datasrc;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        
        /// <summary>
        /// get air freight amount price
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalfreightAmount(string docnum)
       {
           try
           {
               decimal _freightAmt = 0;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblFreight", "pkg_asycudarep.SP_GET_FREIGHTAMNT", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["FREIGHTAMNT"].ToString() != "")
                       _freightAmt = decimal.Parse(_dtResults.Rows[0]["FREIGHTAMNT"].ToString());
               }
               return _freightAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

        /// <summary>
        /// get insuarance amount price
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalInsuaranceAmount(string docnum)
       {
           try
           {
               decimal _insAmt = 0;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblInsAmnt", "pkg_asycudarep.SP_GET_INSUARANCEAMNT", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["INSUARACE"].ToString() != "")
                       _insAmt = decimal.Parse(_dtResults.Rows[0]["INSUARACE"].ToString());
               }
               return _insAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       
        /// <summary>
        /// get other amount price
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalOthereAmount(string docnum)
       {
           try
           {
               decimal _otherAmt = 0;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblOtrAmnt", "pkg_asycudarep.SP_GET_OTHERAMNT", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["OTHER"].ToString() != "")
                       _otherAmt = decimal.Parse(_dtResults.Rows[0]["OTHER"].ToString());
               }
               return _otherAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// get cost amount price
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalCostAmount(string docnum)
       {
           try
           {
               decimal _costAmt = 0;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblCstAmnt", "pkg_asycudarep.SP_GET_COSTAMNT", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["COST"].ToString() != "")
                       _costAmt = decimal.Parse(_dtResults.Rows[0]["COST"].ToString());
               }
               return _costAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }

       }
       
        /// <summary>
        /// get total amount price
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalAmount(string docnum,int num)
       {
           try
           {
               decimal _totlAmt = 0;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               (param[1] = new OracleParameter("p_type", OracleDbType.Int32, null, ParameterDirection.Input)).Value = num;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblTotalAmnt", "pkg_asycudarep.SP_GET_TOTLAMNT", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["TOTALAMNT"].ToString() != "")
                       _totlAmt = decimal.Parse(_dtResults.Rows[0]["TOTALAMNT"].ToString());
               }
               return _totlAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       
        /// <summary>
        /// get FOB amount price
        /// </summary>
        /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalFOBAmount(string docnum)
       {
           try
           {
               decimal _totlFobAmt = 0;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblTotalFobAmnt", "pkg_asycudarep.SP_GET_TOTALFOB", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["TOTALVALUE"].ToString() != "")
                       _totlFobAmt = decimal.Parse(_dtResults.Rows[0]["TOTALVALUE"].ToString());
               }
               return _totlFobAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        
        /// <summary>
        /// get total gross mass for document number
        /// </summary>
       /// <param name="docnum">document number</param>
        /// <returns>decimal</returns>
       public decimal GetTotalGrossMass(string docnum)
       {
           try
           {
               decimal _totlGrossAmt = 0;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docnum;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblTotalGrossMassAmnt", "pkg_asycudarep.SP_GET_GROSSMASS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["TOTALMAS"].ToString() != "")
                       _totlGrossAmt = decimal.Parse(_dtResults.Rows[0]["TOTALMAS"].ToString());
               }
               return _totlGrossAmt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get document item list details
        /// </summary>
       /// <param name="docno">document number</param>
        /// <returns>List<ASY_CUSDEC_ITEM_DTLS_MODEL></returns>
       public List<ASY_CUSDEC_ITEM_DTLS_MODEL> GetAsycudaItemListDetails(string docno)
       {
           try
           {
               List<ASY_CUSDEC_ITEM_DTLS_MODEL> _itemList = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblItemList", "SP_GET_ASYITMDETLS_NEW", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _itemList = DataTableExtensions.ToGenericList<ASY_CUSDEC_ITEM_DTLS_MODEL>(_dtResults, ASY_CUSDEC_ITEM_DTLS_MODEL.Converter);
               }
               return _itemList;
           }
           catch (Exception ex) {
               throw ex;
           }
       }
        /// <summary>
        /// get asycuda document type from id
        /// </summary>
        /// <param name="typeid">type id</param>
        /// <param name="grpId">group id</param>
        /// <returns>string</returns>
       public string getAsyTypeCodefromId(int typeid, int grpId)
       {
           try
           {
               string _typeName = null;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_typid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = typeid;
               (param[1] = new OracleParameter("p_grpid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = grpId;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblTypeName", "pkg_asycuda.SP_GET_ASYTYPCD_BYID", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _typeName = _dtResults.Rows[0]["ADT_TP_CD"].ToString();
               }
               return _typeName;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get asycuda xml tags
        /// </summary>
        /// <param name="parentid">parent tag id</param>
        /// <returns>List<ASY_XML_TAG></returns>
       public List<ASY_XML_TAG> getXmlTagList(decimal parentid)
       {
           try
           {
               List<ASY_XML_TAG> _tagList = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_parid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = parentid;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblXmlTagList", "pkg_asycuda.SP_GET_XMLTAGS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _tagList = DataTableExtensions.ToGenericList<ASY_XML_TAG>(_dtResults, ASY_XML_TAG.Converter);
               }
               return _tagList;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get asycuda tag list for item
        /// </summary>
        /// <param name="parentid">parent tag id</param>
        /// <returns>List<ASY_XML_TAG></returns>
       public List<ASY_XML_TAG> getXmlTagListForItems(decimal parentid)
       {
           try
           {
               List<ASY_XML_TAG> _tagList = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_parid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = parentid;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblXmlTagListForItm", "pkg_asycuda.SP_GET_XMLTAGSFORITM", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _tagList = DataTableExtensions.ToGenericList<ASY_XML_TAG>(_dtResults, ASY_XML_TAG.Converter);
               }
               return _tagList;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// check asycuda document available
        /// </summary>
        /// <param name="docno">document number</param>
        /// <returns>boolean</returns>
       public bool documentIsAvailable(string docno)
       {
           try
           {
               bool _exists = false;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblDocCount", "pkg_asycuda.SP_CHECK_ASYDOC", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _exists = (int.Parse(_dtResults.Rows[0]["DOCCOUNT"].ToString()) > 0) ? true : false;
               }
               return _exists;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    
        /// <summary>
        /// get related database header details
        /// </summary>
        /// <param name="docno">document number</param>
        /// <returns>ASY_ALT_HEADER_DTLS</returns>
       public ASY_ALT_HEADER_DTLS GetAsycudaAlterHederDetails(string docno)
       {
           try
           {
               ASY_ALT_HEADER_DTLS _altHedDet = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblDocCount", "pkg_asycuda.SP_GET_ASYALTERHEADERDET", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _altHedDet = DataTableExtensions.ToGenericList<ASY_ALT_HEADER_DTLS>(_dtResults, ASY_ALT_HEADER_DTLS.Converter)[0];
               }
               return _altHedDet;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// check document is available
        /// </summary>
        /// <param name="documentNo">document number</param>
        /// <returns>boolean</returns>
       public bool CheckDocumentAvailability(string documentNo, string docType)
       {
           try
           {
               bool _available = false;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = documentNo;
               (param[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docType;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblDocCount", "pkg_asycudarep.SP_CHECK_AVAILABLEDOC", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _available = (int.Parse(_dtResults.Rows[0]["DOCCOUNT"].ToString()) > 0) ? true : false;
               }
               return _available;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// add alter header details
        /// </summary>
        /// <param name="_alterHedDet">ASY_ALT_HEADER_DTLS</param>
        /// <returns>boolean</returns>
       public bool AddAlterDetails(ASY_ALT_HEADER_DTLS _alterHedDet)
       {
           try
           {
               bool success = false;
               OracleParameter[] param = new OracleParameter[82];

               //(param[0] = new OracleParameter("p_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_seq_no;
               (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_doc_no;
               (param[1] = new OracleParameter("p_oth_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_oth_doc_no;
               (param[2] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_doc_dt.Date;
               (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_com;
               (param[4] = new OracleParameter("p_db", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_db;
               (param[5] = new OracleParameter("p_proccd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_proc_cd;
               (param[6] = new OracleParameter("p_decl_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_decl_1;
               (param[7] = new OracleParameter("p_decl_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_decl_2;
               (param[8] = new OracleParameter("p_decl_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_decl_3;
               (param[9] = new OracleParameter("p_decl_1n2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_decl_1n2;
               (param[10] = new OracleParameter("p_exp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_exp_cd;
               (param[11] = new OracleParameter("p_exp_tin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_exp_tin;
               (param[12] = new OracleParameter("p_exp_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_exp_name;
               (param[13] = new OracleParameter("p_exp_add", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_exp_add;
               (param[14] = new OracleParameter("p_cons_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cons_cd;
               (param[15] = new OracleParameter("p_cons_tin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cons_tin;
               (param[16] = new OracleParameter("p_cons_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cons_name;
               (param[17] = new OracleParameter("p_cons_add", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cons_add;
               (param[18] = new OracleParameter("p_dec_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_dec_cd;
               (param[19] = new OracleParameter("p_dec_tin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_dec_tin;
               (param[20] = new OracleParameter("p_dec_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_dec_name;
               (param[21] = new OracleParameter("p_dec_add", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_dec_add;
               (param[22] = new OracleParameter("p_items_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_items_qty;
               (param[23] = new OracleParameter("p_tot_pkg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_tot_pkg;
               (param[24] = new OracleParameter("p_exp_cnty_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_exp_cnty_cd;
               (param[25] = new OracleParameter("p_exp_cnty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_exp_cnty;
               (param[26] = new OracleParameter("p_dest_cnty_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_dest_cnty_cd;
               (param[27] = new OracleParameter("p_dest_cnty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_dest_cnty;
               (param[28] = new OracleParameter("p_orig_cnty_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_orig_cnty_cd;
               (param[29] = new OracleParameter("p_orig_cnty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_orig_cnty;
               (param[30] = new OracleParameter("p_load_cnty_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_load_cnty_cd;
               (param[31] = new OracleParameter("p_load_cnty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_load_cnty;
               (param[32] = new OracleParameter("p_vessel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_vessel;
               (param[33] = new OracleParameter("p_voyage", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_voyage;
               (param[34] = new OracleParameter("p_voyage_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_voyage_dt;
               (param[35] = new OracleParameter("p_fcl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_fcl;
               (param[36] = new OracleParameter("p_marks_and_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_marks_and_no;
               (param[37] = new OracleParameter("p_delivery_terms", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_delivery_terms;
               (param[38] = new OracleParameter("p_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cur_cd;
               (param[39] = new OracleParameter("p_tot_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_tot_amt;
               (param[40] = new OracleParameter("p_ex_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_ex_rt;
               (param[41] = new OracleParameter("p_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_bank_cd;
               (param[42] = new OracleParameter("p_bank_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_bank_name;
               (param[43] = new OracleParameter("p_bank_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_bank_branch;
               (param[44] = new OracleParameter("p_terms_of_payment", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_terms_of_payment;
               (param[45] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_acc_no;
               (param[46] = new OracleParameter("p_wh_and_period", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_wh_and_period;
               (param[47] = new OracleParameter("p_off_entry_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_off_entry_cd;
               (param[48] = new OracleParameter("p_off_entry_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_off_entry_desc;
               (param[49] = new OracleParameter("p_loc_goods_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_loc_goods_cd;
               (param[50] = new OracleParameter("p_loc_goods_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_loc_goods_desc;
               (param[51] = new OracleParameter("p_tot_pkg_unit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_tot_pkg_unit;
               (param[52] = new OracleParameter("p_proc_id_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_proc_id_1;
               (param[53] = new OracleParameter("p_proc_id_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_proc_id_2;
               (param[54] = new OracleParameter("p_lision_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_lision_no;
               (param[55] = new OracleParameter("p_trading_cnty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_trading_cnty;
               (param[56] = new OracleParameter("p_main_hs", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_main_hs;
               (param[57] = new OracleParameter("p_noof_forms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_noof_forms;
               (param[58] = new OracleParameter("p_tot_noof_forms", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_tot_noof_forms;
               (param[59] = new OracleParameter("p_reg_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_reg_dt;
               (param[60] = new OracleParameter("p_wh_delay", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_wh_delay;
               (param[61] = new OracleParameter("p_cost_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cost_amt;
               (param[62] = new OracleParameter("p_fre_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_fre_amt;
               (param[63] = new OracleParameter("p_insu_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_insu_amt;
               (param[64] = new OracleParameter("p_oth_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_oth_amt;
               (param[65] = new OracleParameter("p_gross_mass", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_gross_mass;
               (param[66] = new OracleParameter("p_net_mass", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_net_mass;
               (param[67] = new OracleParameter("p_sad_flow", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_sad_flow;
               (param[68] = new OracleParameter("p_selected_page", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_selected_page;
               (param[69] = new OracleParameter("p_val_det", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_val_det;
               (param[70] = new OracleParameter("p_fin_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_fin_code;
               (param[71] = new OracleParameter("p_fin_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_fin_name;
               (param[72] = new OracleParameter("p_border_info_mode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_border_info_mode;
               (param[73] = new OracleParameter("p_cal_working_mode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cal_working_mode;
               (param[74] = new OracleParameter("p_manifest_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_manifest_ref_no;
               (param[75] = new OracleParameter("p_tot_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_tot_cost;
               (param[76] = new OracleParameter("p_terms_of_payment_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_terms_of_payment_desc;
               (param[77] = new OracleParameter("p_garentee_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_garentee_amt;
               (param[78] = new OracleParameter("p_cur_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_cur_name;
               (param[79] = new OracleParameter("p_entry_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_entry_desc;
               (param[80] = new OracleParameter("p_remark", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _alterHedDet.Ach_remark;
               param[81] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
               ConnectionOpen();
               Int32 ret = UpdateRecords("pkg_asycuda.SP_ADD_ALTHEDDET", CommandType.StoredProcedure, param);
               if (ret >= 1)
               {
                   success = true;
               }
               ConnectionClose();
               return success;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get total package for boi
        /// </summary>
        /// <param name="docno">document number</param>
        /// <returns>string</returns>
       public string getTotalPkgForBoi(string docno)
       {
           try
           {
               string _totlPkg = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblTotalPkg", "pkg_asycudarep.SP_GET_ASYBOIITEMQTY", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["TOTALITEM_QTY"].ToString() != "")
                       _totlPkg = _dtResults.Rows[0]["TOTALITEM_QTY"].ToString();
               }
               return _totlPkg;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get alter items
        /// </summary>
        /// <param name="docno">document number</param>
        /// <returns>List<ASY_CUSDEC_ITM></returns>
       public List<ASY_CUSDEC_ITM> GetAltItems(string docno)
       {
           try
           {
               List<ASY_CUSDEC_ITM> _altItmDet = null;
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblItmList", "pkg_asycuda.SP_GET_ALTITEMS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _altItmDet = DataTableExtensions.ToGenericList<ASY_CUSDEC_ITM>(_dtResults, ASY_CUSDEC_ITM.Converter);
               }
               return _altItmDet;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// add xml items
        /// </summary>
        /// <param name="newItem">new items</param>
        /// <returns>boolean</returns>
       public bool AddXmlItems(ASY_CUSDEC_ITM newItem)
       {
           try
           {
               bool success = false;
               OracleParameter[] param = new OracleParameter[50];

               (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_doc_no;
               (param[1] = new OracleParameter("p_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = newItem.Aci_line;
               (param[2] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_itm_cd;
               (param[3] = new OracleParameter("p_hs_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_hs_cd;
               (param[4] = new OracleParameter("p_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_model;
               (param[5] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_itm_desc;
               (param[6] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_qty;
               (param[7] = new OracleParameter("p_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_uom;
               (param[8] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_unit_cost;
               (param[9] = new OracleParameter("p_item_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_item_price;
               (param[10] = new OracleParameter("p_gross_mass", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_gross_mass;
               (param[11] = new OracleParameter("p_net_mass", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_net_mass;
               (param[12] = new OracleParameter("p_preferance", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_preferance;
               (param[13] = new OracleParameter("p_quota", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_quota;
               (param[14] = new OracleParameter("p_bl_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_bl_no;
               (param[15] = new OracleParameter("p_othdoc1_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_othdoc1_no;
               (param[16] = new OracleParameter("p_othdoc1_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = newItem.Ach_othdoc1_line;
               (param[17] = new OracleParameter("p_othdoc2_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_othdoc2_no;
               (param[18] = new OracleParameter("p_othdoc2_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = newItem.Ach_othdoc2_line;
               (param[19] = new OracleParameter("p_hscddesc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_hs_cd_desc;

               (param[20] = new OracleParameter("p_ach_num_of_pkg", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Ach_num_of_pkg;
               (param[21] = new OracleParameter("p_ach_mrk1_pkg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_mrk1_pkg;
               (param[22] = new OracleParameter("p_ach_knd_of_pkg", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_knd_of_pkg;
               (param[23] = new OracleParameter("p_ach_knd_of_pkg_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_knd_of_pkg_name;
               (param[24] = new OracleParameter("p_comd_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_comd_cd;
               (param[25] = new OracleParameter("p_prec_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_prec_1;
               (param[26] = new OracleParameter("p_prec_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_prec_4;
               (param[27] = new OracleParameter("p_ach_ext_cust_proc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_ext_cust_proc;
               (param[28] = new OracleParameter("p_ach_nat_cust_proc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_nat_cust_proc;
               (param[29] = new OracleParameter("p_ach_val_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_val_itm;
               (param[30] = new OracleParameter("p_ach_cnty_of_oregn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_cnty_of_oregn;
               (param[31] = new OracleParameter("p_desc_of_goods", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_desc_of_goods;
               (param[32] = new OracleParameter("p_itm_stat_val", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_itm_stat_val;
               (param[33] = new OracleParameter("p_tot_cost_itm", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_tot_cost_itm;
               (param[34] = new OracleParameter("p_rte_of_adj", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Aci_rte_of_adj;
               (param[35] = new OracleParameter("p_ach_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newItem.Ach_cur_cd;
               (param[36] = new OracleParameter("p_inv_nat_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_inv_nat_curr;
               (param[37] = new OracleParameter("p_inv_forgn_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_inv_forgn_curr;
               (param[38] = new OracleParameter("p_int_fre_nat_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_int_fre_nat_curr;
               (param[39] = new OracleParameter("p_int_fre_forgn_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_int_fre_forgn_curr;
               (param[40] = new OracleParameter("p_ins_nat_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_ins_nat_curr;
               (param[41] = new OracleParameter("p_ins_forgn_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_ins_forgn_curr;
               (param[42] = new OracleParameter("p_oth_cst_nat_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_oth_cst_nat_curr;
               (param[43] = new OracleParameter("p_oth_cst_forgn_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_oth_cst_forgn_curr;
               (param[44] = new OracleParameter("p_dedu_nat_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_dedu_nat_curr;
               (param[45] = new OracleParameter("p_dedu_forgn_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_dedu_forgn_curr;
               (param[46] = new OracleParameter("p_curr_amnt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_curr_amnt;
               (param[47] = new OracleParameter("p_ext_fre_nat_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_ext_fre_nat_curr;
               (param[48] = new OracleParameter("p_ext_fre_forgn_curr", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newItem.Aci_ext_fre_forgn_curr;
               param[49] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);

               Int32 ret = UpdateRecords("pkg_asycuda.SP_ADD_ALTITEMS", CommandType.StoredProcedure, param);
               if (ret > 1)
               {
                   success = true;
               }

               return success;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get group items
        /// </summary>
        /// <param name="docno">document number</param>
        /// <param name="mainhs">main hs number</param>
       /// <returns>List<ASY_CUSDEC_HS_ITM></returns>
       public List<ASY_CUSDEC_HS_ITM> getHsGrpItems(string docno, string mainhs)
       {
           try
           {
               List<ASY_CUSDEC_HS_ITM> _altItmDet = null;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_mainhs", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mainhs;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblItmList", "pkg_asycuda.SP_GET_GRPITMDETLS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _altItmDet = DataTableExtensions.ToGenericList<ASY_CUSDEC_HS_ITM>(_dtResults, ASY_CUSDEC_HS_ITM.Converter);
               }
               return _altItmDet;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get hs group item details
        /// </summary>
        /// <param name="docno">document number</param>
        /// <param name="hscode">hs code</param>
       /// <returns>List<ASY_CUSDEC_ITM_DET></returns>
       public List<ASY_CUSDEC_ITM_DET> getHsGrpItemsDet(string docno, string hscode)
       {
           try
           {
               List<ASY_CUSDEC_ITM_DET> _altItmDet = null;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_hscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscode;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblItmList", "pkg_asycuda.SP_GET_HSGRPITMOTHERDET", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   _altItmDet = DataTableExtensions.ToGenericList<ASY_CUSDEC_ITM_DET>(_dtResults, ASY_CUSDEC_ITM_DET.Converter);
               }
               return _altItmDet;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public decimal getNumOfPackgesHsGroup(string hscode, string docno)
       {
           try
           {
               decimal _totlPkg = 0;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_hscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscode;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblTotalPkg", "pkg_asycuda.SP_GET_ASYHSCODEPKG", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["NUM_OF_PKGS"].ToString() != "")
                       _totlPkg = Convert.ToDecimal(_dtResults.Rows[0]["NUM_OF_PKGS"].ToString());
               }
               return _totlPkg;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// get item price for hs group
       /// </summary>
       /// <param name="docno">document number</param>
       /// <param name="hscode">hs code</param>
       /// <returns>decimal</returns>
       public decimal getHsItemsPrice(string hscode, string docno)
       {
           try
           {
               decimal _totPrce = 0;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_hscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscode;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblTotPrce", "pkg_asycuda.SP_GET_HSITMPRICE", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["ITMS_PRICE"].ToString() != "")
                       _totPrce = Convert.ToDecimal(_dtResults.Rows[0]["ITMS_PRICE"].ToString());
               }
               return _totPrce;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// get statistical value for hs code
       /// </summary>
       /// <param name="hscode">hs code</param>
       /// <param name="docno">document no</param>
       /// <returns>decimal</returns>
       public decimal getSatissticalValueforHsCode(string hscode,string docno)
       {
           try
           {
               decimal _statVal = 0;
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_hscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscode;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblStatVal", "pkg_asycuda.SP_GET_STATVALFORHSCD", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   if (_dtResults.Rows[0]["STAT_VAL"].ToString() != "")
                       _statVal = Convert.ToDecimal(_dtResults.Rows[0]["STAT_VAL"].ToString());
               }
               return _statVal;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// get asycuda bl no
       /// </summary>
       /// <param name="hscode">hs code</param>
       /// <param name="docno">document no</param>
       /// <returns>string</returns>
       public string getAsyBlNo(string hscode, string docno)
       {
           try
           {
               string blno = "";
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_hscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscode;
               param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblBlNo", "pkg_asycuda.SP_GET_ASYBLNO", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {

                   blno = _dtResults.Rows[0]["ACI_BL_NO"].ToString();
               }
               return blno;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// get hs group description
       /// </summary>
       /// <param name="hscode">hs code</param>
       /// <returns>string</returns>
       public string getHsgrpDesc(string hscode)
       {
           try
           {
               string hscodedesc = "";
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_hscode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscode;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = CommonQueryDataTable("tblHsDesc", "pkg_asycudarep.SP_GET_ASYHSCDDESC", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {

                   hscodedesc = _dtResults.Rows[0]["DESCRIPTION"].ToString();
               }
               return hscodedesc;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// get xml tag details
       /// </summary>
       /// <param name="type">type of tag list HED or ITM</param>
       /// <returns>string</returns>
       public string getDocumentXml(string type)
       {
           try
           {
               string itmXml = "";
               OracleParameter[] param = new OracleParameter[2];
               (param[0] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
               param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tbldocXml", "pkg_asycuda.SP_GET_DOCUMENTXML", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   byte[] byteArray = (Byte[])_dtResults.Rows[0]["XML_DET"];
                   itmXml = System.Text.Encoding.UTF8.GetString(byteArray);
               }
               return itmXml;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       /// <summary>
       /// get document details for search
       /// </summary>
       /// <param name="pgeNum">page number</param>
       /// <param name="pgeSize">page size</param>
       /// <param name="searchFld">search field</param>
       /// <param name="searchVal">search value</param>
       /// <returns>List<ASY_DOC_SEARCH_HEAD></returns>
       public List<ASY_DOC_SEARCH_HEAD> getDocumentDetails(string pgeNum,string pgeSize,string searchFld,string searchVal)
       {
           try
           {
               string search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal.ToUpper() + "%" : "";
               switch (searchFld)
               {
                   case "Document No":
                       searchFld = "DOCUMENT_NO";
                       break;
                   case "Document Date":
                       searchFld = "DOCUMENT_DATE";
                       if (!string.IsNullOrEmpty(searchVal))
                       {
                           DateTime dt = DateTime.ParseExact(searchVal, "MM/dd/yyyy",
                                           System.Globalization.CultureInfo.InvariantCulture);
                           searchVal = dt.ToString("dd/MMM/yyyy");
                           search = searchVal;
                       }
                       break;
                   case "Document Type":
                       searchFld = "DOCUMENT_TYPE";
                       break;
                   case "Location Of Gods":
                       searchFld = "LOCATION_OF_GOODS";
                       break;
                   case "Place of Loading":
                       searchFld = "PLACE_OF_LOADING";
                       break;
                   case "Procedure Code":
                       searchFld = "PROCEDUER_CODE";
                       break;
                   default:
                       searchFld = "";
                       break;
               }
               List<ASY_DOC_SEARCH_HEAD> doclist = null;
               OracleParameter[] param = new OracleParameter[5];
               (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
               (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
               (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
               (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
               param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               //DataTable _dtResults = CommonQueryDataTable("tblDocs", "SP_SEARCH_DOCUMENTDETAILS", CommandType.StoredProcedure, false, param);
               DataTable _dtResults = CommonQueryDataTable("tblDocs", "pkg_asycudarep.SP_SEARCH_DOCUMENTDETAILS", CommandType.StoredProcedure, false, param);

               if (_dtResults.Rows.Count > 0)
               {
                   doclist = DataTableExtensions.ToGenericList<ASY_DOC_SEARCH_HEAD>(_dtResults, ASY_DOC_SEARCH_HEAD.Converter);
               }
               return doclist;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
        /// <summary>
        /// get totle document count
        /// </summary>
        /// <returns>int</returns>
       //public int getDocumentDetailscCount(string searchFld,string searchVal)
       //{
       //    int docCount = 0;
       //    OracleParameter[] param = new OracleParameter[1];
       //    param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
       //    (param[1] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
       //    (param[2] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchVal;
       //    DataTable _dtResults = CommonQueryDataTable("tblDocs", "SP_GET_TOTALDOCCOUNT", CommandType.StoredProcedure, false, param);
       //    if (_dtResults.Rows.Count > 0)
       //    {
       //        docCount =Convert.ToInt32(_dtResults.Rows[0]["DOCCOUNT"].ToString());
       //    }
       //    return docCount;
       //}
       public ASY_IMP_CUSDEC_HDR GetCusdecHdrDetails(string docNo) {
               try
               {
                   ASY_IMP_CUSDEC_HDR _hedItmDet = new ASY_IMP_CUSDEC_HDR();
                   OracleParameter[] param = new OracleParameter[2];
                   (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
                   param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                   DataTable _dtResults = QueryDataTable("tblHed", "pkg_asycudarep.SP_GET_IMPCUSDECHDR", CommandType.StoredProcedure, false, param);
                   if (_dtResults.Rows.Count > 0)
                   {
                       _hedItmDet = DataTableExtensions.ToGenericList<ASY_IMP_CUSDEC_HDR>(_dtResults, ASY_IMP_CUSDEC_HDR.Converter)[0];
                   }
                   return _hedItmDet;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
       }

       public List<ASY_UOM_DET> getUOMDetails()
       {
           try
           {
               List<ASY_UOM_DET> oResult = new List<ASY_UOM_DET>();
               OracleParameter[] param = new OracleParameter[1];
               param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblHSClaim", "SP_GET_PKG_UOM", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   oResult = DataTableExtensions.ToGenericList<ASY_UOM_DET>(_dtResults, ASY_UOM_DET.Converter);
               }
               return oResult;
           }
           catch (Exception ex) {
               throw ex;
           }
       }

       public MST_UOM_CATE GetPkgDetails(string pkgCd)
       {
           try
           {
               MST_UOM_CATE pkgDtl = new MST_UOM_CATE();
               OracleParameter[] param = new OracleParameter[3];
               (param[0] = new OracleParameter("P_PKGCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pkgCd;
               (param[1] = new OracleParameter("P_PKGCATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "PKG";
               param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblHed", "pkg_asycudarep.SP_GET_PKGDETAILS", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   pkgDtl = DataTableExtensions.ToGenericList<MST_UOM_CATE>(_dtResults, MST_UOM_CATE.Converter)[0];
               }
               return pkgDtl;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public decimal getDocItemPrice(string docno, decimal itmline, string itemCode)
       {
           try
           {
               decimal itmPce = 0;
               OracleParameter[] param = new OracleParameter[4];
               (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
               (param[1] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCode;
               (param[2] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = itmline;
               param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
               DataTable _dtResults = QueryDataTable("tblHed", "pkg_asycudarep.SP_GET_ITEMPRICENEW", CommandType.StoredProcedure, false, param);
               if (_dtResults.Rows.Count > 0)
               {
                   itmPce = Math.Round(Convert.ToDecimal(_dtResults.Rows[0]["ITEMPRICE"].ToString()),2);
               }
               return itmPce;
           }
           catch (Exception ex) {
               throw ex;
           }
       }
       public List<ImpCusdecCost> GET_CUSDEC_COST_BY_DOC(String docno)
       {
           List<ImpCusdecCost> oResult = null;
           OracleParameter[] param = new OracleParameter[2];
           (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
           param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
           DataTable dtTemp = QueryDataTable("tbl", "pkg_asycudarep.sp_get_cusdec_cost", CommandType.StoredProcedure, false, param);
           if (dtTemp.Rows.Count > 0) oResult = DataTableExtensions.ToGenericList<ImpCusdecCost>(dtTemp, ImpCusdecCost.Converter);
           return oResult;
       }

       public decimal GET_CUSDEC_FRGHTBYHS(String docno,string hscd)
       {
           decimal cost = 0;
           OracleParameter[] param = new OracleParameter[3];
           (param[0] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
           (param[1] = new OracleParameter("p_hscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hscd;
           param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
           DataTable dtTemp = QueryDataTable("tbl", "pkg_asycudarep.sp_get_cusdec_costforhs", CommandType.StoredProcedure, false, param);
           if (dtTemp.Rows.Count > 0) cost = Convert.ToDecimal(dtTemp.Rows[0]["HS_FRGNT_AMT"].ToString());
           return cost;
       }

       public string getTermsOfPaymentFirst(string termsofpay)
       {
           string paymentDesc = "";
           OracleParameter[] param = new OracleParameter[2];
           (param[0] = new OracleParameter("p_docnum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = termsofpay;
           param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
           DataTable _dtResults = QueryDataTable("tblHed", "pkg_asycuda.GET_ASY_TERMSOFPAYMMENT", CommandType.StoredProcedure, false, param);
           if (_dtResults.Rows.Count > 0)
           {
               paymentDesc = _dtResults.Rows[0]["TMS_DESCRIP"].ToString();
           }
           return paymentDesc;
       }

       public Int32 deleteTempDocument(string docno)
       {
           Int32 effects = 0;
           ConnectionOpen();
           OracleParameter[] param = new OracleParameter[2];
           (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
           param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
           effects = (Int32)UpdateRecords("pkg_asycuda.SP_DELETE_TEMPASYDOCS", CommandType.StoredProcedure, param);
           ConnectionClose();
           return effects;
       }

       public DataTable getCurrencyBreakDown(string docno)
       {
           OracleParameter[] param = new OracleParameter[2];
           (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
           param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
           DataTable dtTemp = QueryDataTable("tbl", "pkg_asycuda.SP_GET_CUSDECELECOST", CommandType.StoredProcedure, false, param);
           return dtTemp;
       }
       public DataTable getDocNoListForAsyCuda(string docno01, string docno02, string type)
       {
           OracleParameter[] param = new OracleParameter[4];
           (param[0] = new OracleParameter("p_docno01", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno01;
           (param[1] = new OracleParameter("p_docno02", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno02;
           (param[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
           param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
           DataTable dtTemp = QueryDataTable("tbl", "getcusdocnumbersbyrange", CommandType.StoredProcedure, false, param);
           return dtTemp;
       }
    }
}
