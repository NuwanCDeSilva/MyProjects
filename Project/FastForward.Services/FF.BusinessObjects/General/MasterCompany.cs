using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterCompany
    {
        #region Private Members

        private int _mc_act = 0;
        private string _mc_add1 = string.Empty;
        private string _mc_add2 = string.Empty;
        private string _mc_anal1 = string.Empty;
        private string _mc_anal10 = string.Empty;
        private int _mc_anal11 = 0;
        private int _mc_anal12 = 0;
        private DateTime _mc_anal14 = DateTime.MinValue;
        private int _mc_anal13 = 0;
        private DateTime _mc_anal15 = DateTime.MinValue;
        private string _mc_anal2 = string.Empty;
        private string _mc_anal3 = string.Empty;
        private string _mc_anal4 = string.Empty;
        private string _mc_anal5 = string.Empty;
        private string _mc_anal6 = string.Empty;
        private string _mc_anal7 = string.Empty;
        private string _mc_anal8 = string.Empty;
        private string _mc_anal9 = string.Empty;
        private string _mc_cd = string.Empty;
        private string _mc_cre_by = string.Empty;
        private DateTime _mc_cre_dt = DateTime.MinValue;
        private string _mc_cur_cd = string.Empty;
        private string _mc_desc = string.Empty;
        private string _mc_email = string.Empty;
        private string _mc_fax = string.Empty;
        private string _mc_it_powered = string.Empty;
        private string _mc_mod_by = string.Empty;
        private DateTime _mc_mod_dt = DateTime.MinValue;
        private string _mc_session_id = string.Empty;
        private string _mc_tax1 = string.Empty;
        private string _mc_tax2 = string.Empty;
        private string _mc_tax3 = string.Empty;
        private string _mc_tax4 = string.Empty;
        private string _mc_tax5 = string.Empty;
        private string _mc_tel = string.Empty;
        private string _mc_val_method = string.Empty;
        private string _mc_web = string.Empty;
        private string _mc_grup = string.Empty; //Add Chamal (with Dilanda) 21/07/2012

        private string _mc_anal16 = string.Empty;
        private string _mc_anal17 = string.Empty;
        private string _mc_anal18 = string.Empty;
        private string _mc_anal19 = string.Empty;
        private string _mc_anal20 = string.Empty;
        private string _mc_anal21 = string.Empty;
        private string _mc_anal22 = string.Empty;
        private string _mc_anal23 = string.Empty;
        private string _mc_anal24 = string.Empty;
        private string _mc_anal25 = string.Empty;
        private Decimal _mc_anal26 = 0;   //kapila 20/7/2015
        private string _MC_TAX_CALC_MTD = "";   //kapila 26/8/2015
        private Int32 _MC_IS_SCM2_FMS = 0;      //kapila 25/4/2016
        private Int32 _MC_IS_ECD = 0;  //kapila

        private Int32 _mc_resmultaxinv = 0;// Nadeeka 30-12-2015
        private Int32 _MC_IS_GRAN_WO_SER = 0;  //kapila
        private Int32 _mc_alw_minus_bal = 0;   //kapila 

        #endregion

        #region Public Property Definition
        public Int32 Mc_alw_minus_bal
        {
            get { return _mc_alw_minus_bal; }
            set { _mc_alw_minus_bal = value; }
        }
        public Int32 MC_IS_GRAN_WO_SER
        {
            get { return _MC_IS_GRAN_WO_SER; }
            set { _MC_IS_GRAN_WO_SER = value; }
        }
        public Int32 MC_IS_ECD
        {
            get { return _MC_IS_ECD; }
            set { _MC_IS_ECD = value; }
        }
        public Int32 MC_IS_SCM2_FMS
        {
            get { return _MC_IS_SCM2_FMS; }
            set { _MC_IS_SCM2_FMS = value; }
        }
        public int Mc_act
        {
            get { return _mc_act; }
            set { _mc_act = value; }
        }

        public string Mc_add1
        {
            get { return _mc_add1; }
            set { _mc_add1 = value; }
        }

        public string Mc_add2
        {
            get { return _mc_add2; }
            set { _mc_add2 = value; }
        }

        public string Mc_anal1
        {
            get { return _mc_anal1; }
            set { _mc_anal1 = value; }
        }

        public string Mc_anal10
        {
            get { return _mc_anal10; }
            set { _mc_anal10 = value; }
        }

        public int Mc_anal11
        {
            get { return _mc_anal11; }
            set { _mc_anal11 = value; }
        }

        public int Mc_anal12
        {
            get { return _mc_anal12; }
            set { _mc_anal12 = value; }
        }

        public int Mc_anal13
        {
            get { return _mc_anal13; }
            set { _mc_anal13 = value; }
        }

        public DateTime Mc_anal14
        {
            get { return _mc_anal14; }
            set { _mc_anal14 = value; }
        }

        public DateTime Mc_anal15
        {
            get { return _mc_anal15; }
            set { _mc_anal15 = value; }
        }

        public string Mc_anal2
        {
            get { return _mc_anal2; }
            set { _mc_anal2 = value; }
        }

        public string Mc_anal3
        {
            get { return _mc_anal3; }
            set { _mc_anal3 = value; }
        }

        public string Mc_anal4
        {
            get { return _mc_anal4; }
            set { _mc_anal4 = value; }
        }

        public string Mc_anal5
        {
            get { return _mc_anal5; }
            set { _mc_anal5 = value; }
        }

        public string Mc_anal6
        {
            get { return _mc_anal6; }
            set { _mc_anal6 = value; }
        }

        public string Mc_anal7
        {
            get { return _mc_anal7; }
            set { _mc_anal7 = value; }
        }

        public string Mc_anal8
        {
            get { return _mc_anal8; }
            set { _mc_anal8 = value; }
        }

        public string Mc_anal9
        {
            get { return _mc_anal9; }
            set { _mc_anal9 = value; }
        }

        public string Mc_cd
        {
            get { return _mc_cd; }
            set { _mc_cd = value; }
        }

        public string Mc_cre_by
        {
            get { return _mc_cre_by; }
            set { _mc_cre_by = value; }
        }

        public DateTime Mc_cre_dt
        {
            get { return _mc_cre_dt; }
            set { _mc_cre_dt = value; }
        }

        public string Mc_cur_cd
        {
            get { return _mc_cur_cd; }
            set { _mc_cur_cd = value; }
        }

        public string Mc_desc
        {
            get { return _mc_desc; }
            set { _mc_desc = value; }
        }

        public string Mc_email
        {
            get { return _mc_email; }
            set { _mc_email = value; }
        }

        public string Mc_fax
        {
            get { return _mc_fax; }
            set { _mc_fax = value; }
        }

        public string Mc_it_powered
        {
            get { return _mc_it_powered; }
            set { _mc_it_powered = value; }
        }

        public string Mc_mod_by
        {
            get { return _mc_mod_by; }
            set { _mc_mod_by = value; }
        }

        public DateTime Mc_mod_dt
        {
            get { return _mc_mod_dt; }
            set { _mc_mod_dt = value; }
        }

        public string Mc_session_id
        {
            get { return _mc_session_id; }
            set { _mc_session_id = value; }
        }

        public string Mc_tax1
        {
            get { return _mc_tax1; }
            set { _mc_tax1 = value; }
        }

        public string Mc_tax2
        {
            get { return _mc_tax2; }
            set { _mc_tax2 = value; }
        }

        public string Mc_tax3
        {
            get { return _mc_tax3; }
            set { _mc_tax3 = value; }
        }

        public string Mc_tax4
        {
            get { return _mc_tax4; }
            set { _mc_tax4 = value; }
        }

        public string Mc_tax5
        {
            get { return _mc_tax5; }
            set { _mc_tax5 = value; }
        }

        public string Mc_tel
        {
            get { return _mc_tel; }
            set { _mc_tel = value; }
        }

        public string Mc_val_method
        {
            get { return _mc_val_method; }
            set { _mc_val_method = value; }
        }

        public string Mc_web
        {
            get { return _mc_web; }
            set { _mc_web = value; }
        }

        public string Mc_grup
        {
            get { return _mc_grup; }
            set { _mc_grup = value; }
        }


        public string Mc_anal16 { get { return _mc_anal16; } set { _mc_anal16 = value; } }
        public string Mc_anal17 { get { return _mc_anal17; } set { _mc_anal17 = value; } }
        public string Mc_anal18 { get { return _mc_anal18; } set { _mc_anal18 = value; } }
        public string Mc_anal19 { get { return _mc_anal19; } set { _mc_anal19 = value; } }
        public string Mc_anal20 { get { return _mc_anal20; } set { _mc_anal20 = value; } }
        public string Mc_anal21 { get { return _mc_anal21; } set { _mc_anal21 = value; } }
        public string Mc_anal22 { get { return _mc_anal22; } set { _mc_anal22 = value; } }
        public string Mc_anal23 { get { return _mc_anal23; } set { _mc_anal23 = value; } }
        public string Mc_anal24 { get { return _mc_anal24; } set { _mc_anal24 = value; } }
        public string Mc_anal25 { get { return _mc_anal25; } set { _mc_anal25 = value; } }

        public Decimal Mc_anal26 { get { return _mc_anal26; } set { _mc_anal26 = value; } }

        //kapila 26/8/2015
        public string MC_TAX_CALC_MTD
        {
            get { return _MC_TAX_CALC_MTD; }
            set { _MC_TAX_CALC_MTD = value; }
        }
        public Int32 Mc_resmultaxinv { get { return _mc_resmultaxinv; } set { _mc_resmultaxinv = value; } }
        public Int32 Mc_SerScn_Tp { get; set; } //Add by Akila 2107/09/09  - Define serail scan type 1 = Caps on ,0 Caps off

        public string mc_fixa_com { get; set; } 
        #endregion

        public static MasterCompany Converter(DataRow row)
        {
            return new MasterCompany
            {
                Mc_act = ((row["MC_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ACT"])),
                Mc_add1 = ((row["MC_ADD1"] == DBNull.Value) ? string.Empty : row["MC_ADD1"].ToString()),
                Mc_add2 = ((row["MC_ADD2"] == DBNull.Value) ? string.Empty : row["MC_ADD2"].ToString()),
                Mc_anal1 = ((row["MC_ANAL1"] == DBNull.Value) ? string.Empty : row["MC_ANAL1"].ToString()),
                Mc_anal10 = ((row["MC_ANAL10"] == DBNull.Value) ? string.Empty : row["MC_ANAL10"].ToString()),
                Mc_anal11 = ((row["MC_ANAL11"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ANAL11"])),
                Mc_anal12 = ((row["MC_ANAL12"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ANAL12"])),
                Mc_anal13 = ((row["MC_ANAL13"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ANAL13"])),
                Mc_anal14 = ((row["MC_ANAL14"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_ANAL14"])),
                Mc_anal15 = ((row["MC_ANAL15"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_ANAL15"])),
                Mc_anal2 = ((row["MC_ANAL2"] == DBNull.Value) ? string.Empty : row["MC_ANAL2"].ToString()),
                Mc_anal3 = ((row["MC_ANAL3"] == DBNull.Value) ? string.Empty : row["MC_ANAL3"].ToString()),
                Mc_anal4 = ((row["MC_ANAL4"] == DBNull.Value) ? string.Empty : row["MC_ANAL4"].ToString()),
                Mc_anal5 = ((row["MC_ANAL5"] == DBNull.Value) ? string.Empty : row["MC_ANAL5"].ToString()),
                Mc_anal6 = ((row["MC_ANAL6"] == DBNull.Value) ? string.Empty : row["MC_ANAL6"].ToString()),
                Mc_anal7 = ((row["MC_ANAL7"] == DBNull.Value) ? string.Empty : row["MC_ANAL7"].ToString()),
                Mc_anal8 = ((row["MC_ANAL8"] == DBNull.Value) ? string.Empty : row["MC_ANAL8"].ToString()),
                Mc_anal9 = ((row["MC_ANAL9"] == DBNull.Value) ? string.Empty : row["MC_ANAL9"].ToString()),
                Mc_cd = ((row["MC_CD"] == DBNull.Value) ? string.Empty : row["MC_CD"].ToString()),
                Mc_cre_by = ((row["MC_CRE_BY"] == DBNull.Value) ? string.Empty : row["MC_CRE_BY"].ToString()),
                Mc_cre_dt = ((row["MC_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_CRE_DT"])),
                Mc_cur_cd = ((row["MC_CUR_CD"] == DBNull.Value) ? string.Empty : row["MC_CUR_CD"].ToString()),
                Mc_desc = ((row["MC_DESC"] == DBNull.Value) ? string.Empty : row["MC_DESC"].ToString()),
                Mc_email = ((row["MC_EMAIL"] == DBNull.Value) ? string.Empty : row["MC_EMAIL"].ToString()),
                Mc_fax = ((row["MC_FAX"] == DBNull.Value) ? string.Empty : row["MC_FAX"].ToString()),
                Mc_it_powered = ((row["MC_IT_POWERED"] == DBNull.Value) ? string.Empty : row["MC_IT_POWERED"].ToString()),
                Mc_mod_by = ((row["MC_MOD_BY"] == DBNull.Value) ? string.Empty : row["MC_MOD_BY"].ToString()),
                Mc_mod_dt = ((row["MC_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_MOD_DT"])),
                Mc_session_id = ((row["MC_SESSION_ID"] == DBNull.Value) ? string.Empty : row["MC_SESSION_ID"].ToString()),
                Mc_tax1 = ((row["MC_TAX1"] == DBNull.Value) ? string.Empty : row["MC_TAX1"].ToString()),
                Mc_tax2 = ((row["MC_TAX2"] == DBNull.Value) ? string.Empty : row["MC_TAX2"].ToString()),
                Mc_tax3 = ((row["MC_TAX3"] == DBNull.Value) ? string.Empty : row["MC_TAX3"].ToString()),
                Mc_tax4 = ((row["MC_TAX4"] == DBNull.Value) ? string.Empty : row["MC_TAX4"].ToString()),
                Mc_tax5 = ((row["MC_TAX5"] == DBNull.Value) ? string.Empty : row["MC_TAX5"].ToString()),
                Mc_tel = ((row["MC_TEL"] == DBNull.Value) ? string.Empty : row["MC_TEL"].ToString()),
                Mc_val_method = ((row["MC_VAL_METHOD"] == DBNull.Value) ? string.Empty : row["MC_VAL_METHOD"].ToString()),
                Mc_web = ((row["MC_WEB"] == DBNull.Value) ? string.Empty : row["MC_WEB"].ToString()),

                Mc_anal16 = ((row["MC_ANAL16"] == DBNull.Value) ? string.Empty : row["MC_ANAL16"].ToString()),
                Mc_anal17 = ((row["MC_ANAL17"] == DBNull.Value) ? string.Empty : row["MC_ANAL17"].ToString()),
                Mc_anal18 = ((row["MC_ANAL18"] == DBNull.Value) ? string.Empty : row["MC_ANAL18"].ToString()),
                Mc_anal19 = ((row["MC_ANAL19"] == DBNull.Value) ? string.Empty : row["MC_ANAL19"].ToString()),
                Mc_anal20 = ((row["MC_ANAL20"] == DBNull.Value) ? string.Empty : row["MC_ANAL20"].ToString()),
                Mc_anal21 = ((row["MC_ANAL21"] == DBNull.Value) ? string.Empty : row["MC_ANAL21"].ToString()),
                Mc_anal22 = ((row["MC_ANAL22"] == DBNull.Value) ? string.Empty : row["MC_ANAL22"].ToString()),
                Mc_anal23 = ((row["MC_ANAL23"] == DBNull.Value) ? string.Empty : row["MC_ANAL23"].ToString()),
                Mc_anal24 = ((row["MC_ANAL24"] == DBNull.Value) ? string.Empty : row["MC_ANAL24"].ToString()),
                Mc_anal25 = ((row["MC_ANAL25"] == DBNull.Value) ? string.Empty : row["MC_ANAL25"].ToString()),
                Mc_anal26 = ((row["MC_ANAL26"] == DBNull.Value) ? 0 :Convert.ToDecimal( row["MC_ANAL26"].ToString())),
                MC_TAX_CALC_MTD = ((row["MC_TAX_CALC_MTD"] == DBNull.Value) ? string.Empty : row["MC_TAX_CALC_MTD"].ToString()),
                Mc_resmultaxinv = ((row["MC_RESMULTAXINV"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_RESMULTAXINV"])),
                MC_IS_SCM2_FMS = ((row["MC_IS_SCM2_FMS"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_IS_SCM2_FMS"])),
                MC_IS_ECD = ((row["MC_IS_ECD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_IS_ECD"])),
                Mc_alw_minus_bal = ((row["Mc_alw_minus_bal"] == DBNull.Value) ? 0 : Convert.ToInt32(row["Mc_alw_minus_bal"])),
                MC_IS_GRAN_WO_SER = ((row["MC_IS_GRAN_WO_SER"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_IS_GRAN_WO_SER"])),
                Mc_grup = ((row["MC_GRUP"] == DBNull.Value) ? string.Empty : row["MC_GRUP"].ToString()),
                mc_fixa_com = ((row["mc_fixa_com"] == DBNull.Value) ? string.Empty : row["mc_fixa_com"].ToString()),
                Mc_SerScn_Tp = row["mc_serscn_tp"] == DBNull.Value ? 0 : Convert.ToInt32(row["mc_serscn_tp"]) //Add by Akila 2107/09/09        
            
            };
        }

        public static MasterCompany ConverterTotal(DataRow row)
        {
            return new MasterCompany
            {
                Mc_act = ((row["MC_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ACT"])),
                Mc_add1 = ((row["MC_ADD1"] == DBNull.Value) ? string.Empty : row["MC_ADD1"].ToString()),
                Mc_add2 = ((row["MC_ADD2"] == DBNull.Value) ? string.Empty : row["MC_ADD2"].ToString()),
                Mc_anal1 = ((row["MC_ANAL1"] == DBNull.Value) ? string.Empty : row["MC_ANAL1"].ToString()),
                Mc_anal10 = ((row["MC_ANAL10"] == DBNull.Value) ? string.Empty : row["MC_ANAL10"].ToString()),
                Mc_anal11 = ((row["MC_ANAL11"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ANAL11"])),
                Mc_anal12 = ((row["MC_ANAL12"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ANAL12"])),
                Mc_anal13 = ((row["MC_ANAL13"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_ANAL13"])),
                Mc_anal14 = ((row["MC_ANAL14"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_ANAL14"])),
                Mc_anal15 = ((row["MC_ANAL15"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_ANAL15"])),
                Mc_anal2 = ((row["MC_ANAL2"] == DBNull.Value) ? string.Empty : row["MC_ANAL2"].ToString()),
                Mc_anal3 = ((row["MC_ANAL3"] == DBNull.Value) ? string.Empty : row["MC_ANAL3"].ToString()),
                Mc_anal4 = ((row["MC_ANAL4"] == DBNull.Value) ? string.Empty : row["MC_ANAL4"].ToString()),
                Mc_anal5 = ((row["MC_ANAL5"] == DBNull.Value) ? string.Empty : row["MC_ANAL5"].ToString()),
                Mc_anal6 = ((row["MC_ANAL6"] == DBNull.Value) ? string.Empty : row["MC_ANAL6"].ToString()),
                Mc_anal7 = ((row["MC_ANAL7"] == DBNull.Value) ? string.Empty : row["MC_ANAL7"].ToString()),
                Mc_anal8 = ((row["MC_ANAL8"] == DBNull.Value) ? string.Empty : row["MC_ANAL8"].ToString()),
                Mc_anal9 = ((row["MC_ANAL9"] == DBNull.Value) ? string.Empty : row["MC_ANAL9"].ToString()),
                Mc_cd = ((row["MC_CD"] == DBNull.Value) ? string.Empty : row["MC_CD"].ToString()),
                Mc_cre_by = ((row["MC_CRE_BY"] == DBNull.Value) ? string.Empty : row["MC_CRE_BY"].ToString()),
                Mc_cre_dt = ((row["MC_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_CRE_DT"])),
                Mc_cur_cd = ((row["MC_CUR_CD"] == DBNull.Value) ? string.Empty : row["MC_CUR_CD"].ToString()),
                Mc_desc = ((row["MC_DESC"] == DBNull.Value) ? string.Empty : row["MC_DESC"].ToString()),
                Mc_email = ((row["MC_EMAIL"] == DBNull.Value) ? string.Empty : row["MC_EMAIL"].ToString()),
                Mc_fax = ((row["MC_FAX"] == DBNull.Value) ? string.Empty : row["MC_FAX"].ToString()),
                Mc_it_powered = ((row["MC_IT_POWERED"] == DBNull.Value) ? string.Empty : row["MC_IT_POWERED"].ToString()),
                Mc_mod_by = ((row["MC_MOD_BY"] == DBNull.Value) ? string.Empty : row["MC_MOD_BY"].ToString()),
                Mc_mod_dt = ((row["MC_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["MC_MOD_DT"])),
                Mc_session_id = ((row["MC_SESSION_ID"] == DBNull.Value) ? string.Empty : row["MC_SESSION_ID"].ToString()),
                Mc_tax1 = ((row["MC_TAX1"] == DBNull.Value) ? string.Empty : row["MC_TAX1"].ToString()),
                Mc_tax2 = ((row["MC_TAX2"] == DBNull.Value) ? string.Empty : row["MC_TAX2"].ToString()),
                Mc_tax3 = ((row["MC_TAX3"] == DBNull.Value) ? string.Empty : row["MC_TAX3"].ToString()),
                Mc_tax4 = ((row["MC_TAX4"] == DBNull.Value) ? string.Empty : row["MC_TAX4"].ToString()),
                Mc_tax5 = ((row["MC_TAX5"] == DBNull.Value) ? string.Empty : row["MC_TAX5"].ToString()),
                Mc_tel = ((row["MC_TEL"] == DBNull.Value) ? string.Empty : row["MC_TEL"].ToString()),
                Mc_val_method = ((row["MC_VAL_METHOD"] == DBNull.Value) ? string.Empty : row["MC_VAL_METHOD"].ToString()),
                Mc_web = ((row["MC_WEB"] == DBNull.Value) ? string.Empty : row["MC_WEB"].ToString()),
                Mc_grup = ((row["MC_GRUP"] == DBNull.Value) ? string.Empty : row["MC_GRUP"].ToString()),

                Mc_anal16 = ((row["MC_ANAL16"] == DBNull.Value) ? string.Empty : row["MC_ANAL16"].ToString()),
                Mc_anal17 = ((row["MC_ANAL17"] == DBNull.Value) ? string.Empty : row["MC_ANAL17"].ToString()),
                Mc_anal18 = ((row["MC_ANAL18"] == DBNull.Value) ? string.Empty : row["MC_ANAL18"].ToString()),
                Mc_anal19 = ((row["MC_ANAL19"] == DBNull.Value) ? string.Empty : row["MC_ANAL19"].ToString()),
                Mc_anal20 = ((row["MC_ANAL20"] == DBNull.Value) ? string.Empty : row["MC_ANAL20"].ToString()),
                Mc_anal21 = ((row["MC_ANAL21"] == DBNull.Value) ? string.Empty : row["MC_ANAL21"].ToString()),
                Mc_anal22 = ((row["MC_ANAL22"] == DBNull.Value) ? string.Empty : row["MC_ANAL22"].ToString()),
                Mc_anal23 = ((row["MC_ANAL23"] == DBNull.Value) ? string.Empty : row["MC_ANAL23"].ToString()),
                Mc_anal24 = ((row["MC_ANAL24"] == DBNull.Value) ? string.Empty : row["MC_ANAL24"].ToString()),
                Mc_anal25 = ((row["MC_ANAL25"] == DBNull.Value) ? string.Empty : row["MC_ANAL25"].ToString()),
                MC_IS_SCM2_FMS = ((row["MC_IS_SCM2_FMS"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_IS_SCM2_FMS"])),
                MC_IS_ECD = ((row["MC_IS_ECD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_IS_ECD"])),
                Mc_alw_minus_bal = ((row["Mc_alw_minus_bal"] == DBNull.Value) ? 0 : Convert.ToInt32(row["Mc_alw_minus_bal"])),
                Mc_SerScn_Tp = row["mc_serscn_tp"] == DBNull.Value ? 0 : Convert.ToInt32(row["mc_serscn_tp"])  //Add by Akila 2107/09/09
               // MC_IS_GRAN_WO_SER = ((row["MC_IS_GRAN_WO_SER"] == DBNull.Value) ? 0 : Convert.ToInt32(row["MC_IS_GRAN_WO_SER"]))
            };
        }
    }
}
