using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ServiceCostSheet
    {

        /// <summary>
        /// Written By Shani on 07/01/2013
        /// Table: sev_costsheet 
        /// </summary>
        #region Private Members
        private Boolean _scs_act;
        private string _scs_anal1;
        private string _scs_anal2;
        private string _scs_anal3;
        private decimal _scs_anal4;
        private decimal _scs_anal5;
        private decimal _scs_anal6;
        private decimal _scs_balqty;
        private string _scs_com;
        private string _scs_consumablecd;
        private string _scs_cuscd;
        private string _scs_cusname;
        private string _scs_direc;
        private DateTime _scs_docdt;
        private string _scs_docno;
        private decimal _scs_docqty;
        private string _scs_doctp;
        private string _scs_invoiceno;
        private Boolean _scs_isfoc;
        private Boolean _scs_isinvoice;
        private Boolean _scs_isrevitm;
        private string _scs_itmcd;
        private string _scs_itmdesc;
        private string _scs_itmstus;
        private string _scs_itmtp;
        private Boolean _scs_jobclose;
        private string _scs_jobconfno;
        private string _scs_jobitmcd;
        private string _scs_jobitmser;
        private string _scs_jobitmwarr;
        private Int32 _scs_joblineno;
        private string _scs_jobno;
        private Int32 _scs_line;
        private string _scs_loc;
        private Int32 _scs_movelineno;
        private Int32 _scs_oldline;
        private Int32 _scs_outdocline;
        private string _scs_outdocno;
        private decimal _scs_qty;
        private decimal _scs_totunitcost;
        private decimal _scs_unitcost;
        private string _scs_uom;
        #endregion

        public Boolean Scs_act { get { return _scs_act; } set { _scs_act = value; } }
        public string Scs_anal1 { get { return _scs_anal1; } set { _scs_anal1 = value; } }
        public string Scs_anal2 { get { return _scs_anal2; } set { _scs_anal2 = value; } }
        public string Scs_anal3 { get { return _scs_anal3; } set { _scs_anal3 = value; } }
        public decimal Scs_anal4 { get { return _scs_anal4; } set { _scs_anal4 = value; } }
        public decimal Scs_anal5 { get { return _scs_anal5; } set { _scs_anal5 = value; } }
        public decimal Scs_anal6 { get { return _scs_anal6; } set { _scs_anal6 = value; } }
        public decimal Scs_balqty { get { return _scs_balqty; } set { _scs_balqty = value; } }
        public string Scs_com { get { return _scs_com; } set { _scs_com = value; } }
        public string Scs_consumablecd { get { return _scs_consumablecd; } set { _scs_consumablecd = value; } }
        public string Scs_cuscd { get { return _scs_cuscd; } set { _scs_cuscd = value; } }
        public string Scs_cusname { get { return _scs_cusname; } set { _scs_cusname = value; } }
        public string Scs_direc { get { return _scs_direc; } set { _scs_direc = value; } }
        public DateTime Scs_docdt { get { return _scs_docdt; } set { _scs_docdt = value; } }
        public string Scs_docno { get { return _scs_docno; } set { _scs_docno = value; } }
        public decimal Scs_docqty { get { return _scs_docqty; } set { _scs_docqty = value; } }
        public string Scs_doctp { get { return _scs_doctp; } set { _scs_doctp = value; } }
        public string Scs_invoiceno { get { return _scs_invoiceno; } set { _scs_invoiceno = value; } }
        public Boolean Scs_isfoc { get { return _scs_isfoc; } set { _scs_isfoc = value; } }
        public Boolean Scs_isinvoice { get { return _scs_isinvoice; } set { _scs_isinvoice = value; } }
        public Boolean Scs_isrevitm { get { return _scs_isrevitm; } set { _scs_isrevitm = value; } }
        public string Scs_itmcd { get { return _scs_itmcd; } set { _scs_itmcd = value; } }
        public string Scs_itmdesc { get { return _scs_itmdesc; } set { _scs_itmdesc = value; } }
        public string Scs_itmstus { get { return _scs_itmstus; } set { _scs_itmstus = value; } }
        public string Scs_itmtp { get { return _scs_itmtp; } set { _scs_itmtp = value; } }
        public Boolean Scs_jobclose { get { return _scs_jobclose; } set { _scs_jobclose = value; } }
        public string Scs_jobconfno { get { return _scs_jobconfno; } set { _scs_jobconfno = value; } }
        public string Scs_jobitmcd { get { return _scs_jobitmcd; } set { _scs_jobitmcd = value; } }
        public string Scs_jobitmser { get { return _scs_jobitmser; } set { _scs_jobitmser = value; } }
        public string Scs_jobitmwarr { get { return _scs_jobitmwarr; } set { _scs_jobitmwarr = value; } }
        public Int32 Scs_joblineno { get { return _scs_joblineno; } set { _scs_joblineno = value; } }
        public string Scs_jobno { get { return _scs_jobno; } set { _scs_jobno = value; } }
        public Int32 Scs_line { get { return _scs_line; } set { _scs_line = value; } }
        public string Scs_loc { get { return _scs_loc; } set { _scs_loc = value; } }
        public Int32 Scs_movelineno { get { return _scs_movelineno; } set { _scs_movelineno = value; } }
        public Int32 Scs_oldline { get { return _scs_oldline; } set { _scs_oldline = value; } }
        public Int32 Scs_outdocline { get { return _scs_outdocline; } set { _scs_outdocline = value; } }
        public string Scs_outdocno { get { return _scs_outdocno; } set { _scs_outdocno = value; } }
        public decimal Scs_qty { get { return _scs_qty; } set { _scs_qty = value; } }
        public decimal Scs_totunitcost { get { return _scs_totunitcost; } set { _scs_totunitcost = value; } }
        public decimal Scs_unitcost { get { return _scs_unitcost; } set { _scs_unitcost = value; } }
        public string Scs_uom { get { return _scs_uom; } set { _scs_uom = value; } }

        public static ServiceCostSheet Converter(DataRow row)
        {
            return new ServiceCostSheet
            {
                Scs_act = row["SCS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SCS_ACT"]),
                Scs_anal1 = row["SCS_ANAL1"] == DBNull.Value ? string.Empty : row["SCS_ANAL1"].ToString(),
                Scs_anal2 = row["SCS_ANAL2"] == DBNull.Value ? string.Empty : row["SCS_ANAL2"].ToString(),
                Scs_anal3 = row["SCS_ANAL3"] == DBNull.Value ? string.Empty : row["SCS_ANAL3"].ToString(),
                Scs_anal4 = row["SCS_ANAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_ANAL4"]),
                Scs_anal5 = row["SCS_ANAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_ANAL5"]),
                Scs_anal6 = row["SCS_ANAL6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_ANAL6"]),
                Scs_balqty = row["SCS_BALQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_BALQTY"]),
                Scs_com = row["SCS_COM"] == DBNull.Value ? string.Empty : row["SCS_COM"].ToString(),
                Scs_consumablecd = row["SCS_CONSUMABLECD"] == DBNull.Value ? string.Empty : row["SCS_CONSUMABLECD"].ToString(),
                Scs_cuscd = row["SCS_CUSCD"] == DBNull.Value ? string.Empty : row["SCS_CUSCD"].ToString(),
                Scs_cusname = row["SCS_CUSNAME"] == DBNull.Value ? string.Empty : row["SCS_CUSNAME"].ToString(),
                Scs_direc = row["SCS_DIREC"] == DBNull.Value ? string.Empty : row["SCS_DIREC"].ToString(),
                Scs_docdt = row["SCS_DOCDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCS_DOCDT"]),
                Scs_docno = row["SCS_DOCNO"] == DBNull.Value ? string.Empty : row["SCS_DOCNO"].ToString(),
                Scs_docqty = row["SCS_DOCQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_DOCQTY"]),
                Scs_doctp = row["SCS_DOCTP"] == DBNull.Value ? string.Empty : row["SCS_DOCTP"].ToString(),
                Scs_invoiceno = row["SCS_INVOICENO"] == DBNull.Value ? string.Empty : row["SCS_INVOICENO"].ToString(),
                Scs_isfoc = row["SCS_ISFOC"] == DBNull.Value ? false : Convert.ToBoolean(row["SCS_ISFOC"]),
                Scs_isinvoice = row["SCS_ISINVOICE"] == DBNull.Value ? false : Convert.ToBoolean(row["SCS_ISINVOICE"]),
                Scs_isrevitm = row["SCS_ISREVITM"] == DBNull.Value ? false : Convert.ToBoolean(row["SCS_ISREVITM"]),
                Scs_itmcd = row["SCS_ITMCD"] == DBNull.Value ? string.Empty : row["SCS_ITMCD"].ToString(),
                Scs_itmdesc = row["SCS_ITMDESC"] == DBNull.Value ? string.Empty : row["SCS_ITMDESC"].ToString(),
                Scs_itmstus = row["SCS_ITMSTUS"] == DBNull.Value ? string.Empty : row["SCS_ITMSTUS"].ToString(),
                Scs_itmtp = row["SCS_ITMTP"] == DBNull.Value ? string.Empty : row["SCS_ITMTP"].ToString(),
                Scs_jobclose = row["SCS_JOBCLOSE"] == DBNull.Value ? false : Convert.ToBoolean(row["SCS_JOBCLOSE"]),
                Scs_jobconfno = row["SCS_JOBCONFNO"] == DBNull.Value ? string.Empty : row["SCS_JOBCONFNO"].ToString(),
                Scs_jobitmcd = row["SCS_JOBITMCD"] == DBNull.Value ? string.Empty : row["SCS_JOBITMCD"].ToString(),
                Scs_jobitmser = row["SCS_JOBITMSER"] == DBNull.Value ? string.Empty : row["SCS_JOBITMSER"].ToString(),
                Scs_jobitmwarr = row["SCS_JOBITMWARR"] == DBNull.Value ? string.Empty : row["SCS_JOBITMWARR"].ToString(),
                Scs_joblineno = row["SCS_JOBLINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCS_JOBLINENO"]),
                Scs_jobno = row["SCS_JOBNO"] == DBNull.Value ? string.Empty : row["SCS_JOBNO"].ToString(),
                Scs_line = row["SCS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCS_LINE"]),
                Scs_loc = row["SCS_LOC"] == DBNull.Value ? string.Empty : row["SCS_LOC"].ToString(),
                Scs_movelineno = row["SCS_MOVELINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCS_MOVELINENO"]),
                Scs_oldline = row["SCS_OLDLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCS_OLDLINE"]),
                Scs_outdocline = row["SCS_OUTDOCLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCS_OUTDOCLINE"]),
                Scs_outdocno = row["SCS_OUTDOCNO"] == DBNull.Value ? string.Empty : row["SCS_OUTDOCNO"].ToString(),
                Scs_qty = row["SCS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_QTY"]),
                Scs_totunitcost = row["SCS_TOTUNITCOST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_TOTUNITCOST"]),
                Scs_unitcost = row["SCS_UNITCOST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCS_UNITCOST"]),
                Scs_uom = row["SCS_UOM"] == DBNull.Value ? string.Empty : row["SCS_UOM"].ToString()

            };
        }

 

    }
}
