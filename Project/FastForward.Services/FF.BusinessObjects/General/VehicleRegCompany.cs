using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class VehicleRegCompany
    {
        #region members
        private string _svfc_chasis;
        private string _svfc_com;
        private string _svfc_cre_by;
        private DateTime _svfc_cre_when;
        private string _svfc_cus_name;
        private DateTime _svfc_dt;
        private string _svfc_engine;
        private string _svfc_fin_comp;
        private string _svfc_inv_no;
        private string _svfc_pc;
        private string _svfc_pod;
        private string _svfc_rec_no;
        private string _svfc_rem;
        private Int32 _svfc_seq;

        #endregion

        #region properties

        public string Svfc_chasis
        {
            get { return _svfc_chasis; }
            set { _svfc_chasis = value; }
        }
        public string Svfc_com
        {
            get { return _svfc_com; }
            set { _svfc_com = value; }
        }
        public string Svfc_cre_by
        {
            get { return _svfc_cre_by; }
            set { _svfc_cre_by = value; }
        }
        public DateTime Svfc_cre_when
        {
            get { return _svfc_cre_when; }
            set { _svfc_cre_when = value; }
        }
        public string Svfc_cus_name
        {
            get { return _svfc_cus_name; }
            set { _svfc_cus_name = value; }
        }
        public DateTime Svfc_dt
        {
            get { return _svfc_dt; }
            set { _svfc_dt = value; }
        }
        public string Svfc_engine
        {
            get { return _svfc_engine; }
            set { _svfc_engine = value; }
        }
        public string Svfc_fin_comp
        {
            get { return _svfc_fin_comp; }
            set { _svfc_fin_comp = value; }
        }
        public string Svfc_inv_no
        {
            get { return _svfc_inv_no; }
            set { _svfc_inv_no = value; }
        }
        public string Svfc_pc
        {
            get { return _svfc_pc; }
            set { _svfc_pc = value; }
        }
        public string Svfc_pod
        {
            get { return _svfc_pod; }
            set { _svfc_pod = value; }
        }
        public string Svfc_rec_no
        {
            get { return _svfc_rec_no; }
            set { _svfc_rec_no = value; }
        }
        public string Svfc_rem
        {
            get { return _svfc_rem; }
            set { _svfc_rem = value; }
        }
        public Int32 Svfc_seq
        {
            get { return _svfc_seq; }
            set { _svfc_seq = value; }
        }

        #endregion

        public static VehicleRegCompany Converter(DataRow row)
        {
            return new VehicleRegCompany
            {
                Svfc_chasis = row["SVFC_CHASIS"] == DBNull.Value ? string.Empty : row["SVFC_CHASIS"].ToString(),
                Svfc_com = row["SVFC_COM"] == DBNull.Value ? string.Empty : row["SVFC_COM"].ToString(),
                Svfc_cre_by = row["SVFC_CRE_BY"] == DBNull.Value ? string.Empty : row["SVFC_CRE_BY"].ToString(),
                Svfc_cre_when = row["SVFC_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVFC_CRE_WHEN"]),
                Svfc_cus_name = row["SVFC_CUS_NAME"] == DBNull.Value ? string.Empty : row["SVFC_CUS_NAME"].ToString(),
                Svfc_dt = row["SVFC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVFC_DT"]),
                Svfc_engine = row["SVFC_ENGINE"] == DBNull.Value ? string.Empty : row["SVFC_ENGINE"].ToString(),
                Svfc_fin_comp = row["SVFC_FIN_COMP"] == DBNull.Value ? string.Empty : row["SVFC_FIN_COMP"].ToString(),
                Svfc_inv_no = row["SVFC_INV_NO"] == DBNull.Value ? string.Empty : row["SVFC_INV_NO"].ToString(),
                Svfc_pc = row["SVFC_PC"] == DBNull.Value ? string.Empty : row["SVFC_PC"].ToString(),
                Svfc_pod = row["SVFC_POD"] == DBNull.Value ? string.Empty : row["SVFC_POD"].ToString(),
                Svfc_rec_no = row["SVFC_REC_NO"] == DBNull.Value ? string.Empty : row["SVFC_REC_NO"].ToString(),
                Svfc_rem = row["SVFC_REM"] == DBNull.Value ? string.Empty : row["SVFC_REM"].ToString(),
                Svfc_seq = row["SVFC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVFC_SEQ"])
            };
        }
    }
}
