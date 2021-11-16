using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{

    public class MST_COM
    {
        public String MC_CD { get; set; }
        public String MC_DESC { get; set; }
        public String MC_ADD1 { get; set; }
        public String MC_ADD2 { get; set; }
        public String MC_TEL { get; set; }
        public String MC_FAX { get; set; }
        public String MC_EMAIL { get; set; }
        public String MC_WEB { get; set; }
        public String MC_TAX1 { get; set; }
        public String MC_TAX2 { get; set; }
        public String MC_TAX3 { get; set; }
        public String MC_TAX4 { get; set; }
        public String MC_TAX5 { get; set; }
        public String MC_CUR_CD { get; set; }
        public String MC_VAL_METHOD { get; set; }
        public String MC_IT_POWERED { get; set; }
        public Int32 MC_ACT { get; set; }
        public String MC_CRE_BY { get; set; }
        public DateTime MC_CRE_DT { get; set; }
        public String MC_MOD_BY { get; set; }
        public DateTime MC_MOD_DT { get; set; }
        public String MC_SESSION_ID { get; set; }
        public String MC_ANAL1 { get; set; }
        public String MC_ANAL2 { get; set; }
        public String MC_ANAL3 { get; set; }
        public String MC_ANAL4 { get; set; }
        public String MC_ANAL5 { get; set; }
        public String MC_ANAL6 { get; set; }
        public String MC_ANAL7 { get; set; }
        public String MC_ANAL8 { get; set; }
        public String MC_ANAL9 { get; set; }
        public String MC_ANAL10 { get; set; }
        public Int32 MC_ANAL11 { get; set; }
        public Int32 MC_ANAL12 { get; set; }
        public Int32 MC_ANAL13 { get; set; }
        public DateTime MC_ANAL14 { get; set; }
        public DateTime MC_ANAL15 { get; set; }
        public String MC_GRUP { get; set; }
        public String MC_ANAL16 { get; set; }
        public String MC_ANAL17 { get; set; }
        public String MC_ANAL18 { get; set; }
        public String MC_ANAL19 { get; set; }
        public String MC_ANAL20 { get; set; }
        public String MC_ANAL21 { get; set; }
        public String MC_ANAL22 { get; set; }
        public String MC_ANAL23 { get; set; }
        public String MC_ANAL24 { get; set; }
        public String MC_ANAL25 { get; set; }
        public Int32 MC_ANAL26 { get; set; }
        public String MC_TAX_CALC_MTD { get; set; }
        public Int32 MC_RESMULTAXINV { get; set; }
        public Int32 MC_IS_COM_ITM { get; set; }
        public Int32 MC_WORKS_ON { get; set; }
        public Int32 MC_AGE_SLOT { get; set; }
        public Int32 MC_WAVEOFF_VAL { get; set; }
        public Int32 MC_IS_SCM2_FMS { get; set; }
        public Int32 MC_IS_ECD { get; set; }
        public Int32 MC_IS_GRAN_WO_SER { get; set; }
        public Int32 MC_ALW_MINUS_BAL { get; set; }
        public Int32 MC_ISSCM2 { get; set; }
        public String MC_PIC_PATH { get; set; }
        public static MST_COM Converter(DataRow row)
        {
            return new MST_COM
            {
                MC_CD = row["MC_CD"] == DBNull.Value ? string.Empty : row["MC_CD"].ToString(),
                MC_DESC = row["MC_DESC"] == DBNull.Value ? string.Empty : row["MC_DESC"].ToString(),
                MC_ADD1 = row["MC_ADD1"] == DBNull.Value ? string.Empty : row["MC_ADD1"].ToString(),
                MC_ADD2 = row["MC_ADD2"] == DBNull.Value ? string.Empty : row["MC_ADD2"].ToString(),
                MC_TEL = row["MC_TEL"] == DBNull.Value ? string.Empty : row["MC_TEL"].ToString(),
                MC_FAX = row["MC_FAX"] == DBNull.Value ? string.Empty : row["MC_FAX"].ToString(),
                MC_EMAIL = row["MC_EMAIL"] == DBNull.Value ? string.Empty : row["MC_EMAIL"].ToString(),
                MC_WEB = row["MC_WEB"] == DBNull.Value ? string.Empty : row["MC_WEB"].ToString(),
                MC_TAX1 = row["MC_TAX1"] == DBNull.Value ? string.Empty : row["MC_TAX1"].ToString(),
                MC_TAX2 = row["MC_TAX2"] == DBNull.Value ? string.Empty : row["MC_TAX2"].ToString(),
                MC_TAX3 = row["MC_TAX3"] == DBNull.Value ? string.Empty : row["MC_TAX3"].ToString(),
                MC_TAX4 = row["MC_TAX4"] == DBNull.Value ? string.Empty : row["MC_TAX4"].ToString(),
                MC_TAX5 = row["MC_TAX5"] == DBNull.Value ? string.Empty : row["MC_TAX5"].ToString(),
                MC_CUR_CD = row["MC_CUR_CD"] == DBNull.Value ? string.Empty : row["MC_CUR_CD"].ToString(),
                MC_VAL_METHOD = row["MC_VAL_METHOD"] == DBNull.Value ? string.Empty : row["MC_VAL_METHOD"].ToString(),
                MC_IT_POWERED = row["MC_IT_POWERED"] == DBNull.Value ? string.Empty : row["MC_IT_POWERED"].ToString(),
                MC_ACT = row["MC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ACT"].ToString()),
                MC_CRE_BY = row["MC_CRE_BY"] == DBNull.Value ? string.Empty : row["MC_CRE_BY"].ToString(),
                MC_CRE_DT = row["MC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MC_CRE_DT"].ToString()),
                MC_MOD_BY = row["MC_MOD_BY"] == DBNull.Value ? string.Empty : row["MC_MOD_BY"].ToString(),
                MC_MOD_DT = row["MC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MC_MOD_DT"].ToString()),
                MC_SESSION_ID = row["MC_SESSION_ID"] == DBNull.Value ? string.Empty : row["MC_SESSION_ID"].ToString(),
                MC_ANAL1 = row["MC_ANAL1"] == DBNull.Value ? string.Empty : row["MC_ANAL1"].ToString(),
                MC_ANAL2 = row["MC_ANAL2"] == DBNull.Value ? string.Empty : row["MC_ANAL2"].ToString(),
                MC_ANAL3 = row["MC_ANAL3"] == DBNull.Value ? string.Empty : row["MC_ANAL3"].ToString(),
                MC_ANAL4 = row["MC_ANAL4"] == DBNull.Value ? string.Empty : row["MC_ANAL4"].ToString(),
                MC_ANAL5 = row["MC_ANAL5"] == DBNull.Value ? string.Empty : row["MC_ANAL5"].ToString(),
                MC_ANAL6 = row["MC_ANAL6"] == DBNull.Value ? string.Empty : row["MC_ANAL6"].ToString(),
                MC_ANAL7 = row["MC_ANAL7"] == DBNull.Value ? string.Empty : row["MC_ANAL7"].ToString(),
                MC_ANAL8 = row["MC_ANAL8"] == DBNull.Value ? string.Empty : row["MC_ANAL8"].ToString(),
                MC_ANAL9 = row["MC_ANAL9"] == DBNull.Value ? string.Empty : row["MC_ANAL9"].ToString(),
                MC_ANAL10 = row["MC_ANAL10"] == DBNull.Value ? string.Empty : row["MC_ANAL10"].ToString(),
                MC_ANAL11 = row["MC_ANAL11"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ANAL11"].ToString()),
                MC_ANAL12 = row["MC_ANAL12"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ANAL12"].ToString()),
                MC_ANAL13 = row["MC_ANAL13"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ANAL13"].ToString()),
                MC_ANAL14 = row["MC_ANAL14"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MC_ANAL14"].ToString()),
                MC_ANAL15 = row["MC_ANAL15"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MC_ANAL15"].ToString()),
                MC_GRUP = row["MC_GRUP"] == DBNull.Value ? string.Empty : row["MC_GRUP"].ToString(),
                MC_ANAL16 = row["MC_ANAL16"] == DBNull.Value ? string.Empty : row["MC_ANAL16"].ToString(),
                MC_ANAL17 = row["MC_ANAL17"] == DBNull.Value ? string.Empty : row["MC_ANAL17"].ToString(),
                MC_ANAL18 = row["MC_ANAL18"] == DBNull.Value ? string.Empty : row["MC_ANAL18"].ToString(),
                MC_ANAL19 = row["MC_ANAL19"] == DBNull.Value ? string.Empty : row["MC_ANAL19"].ToString(),
                MC_ANAL20 = row["MC_ANAL20"] == DBNull.Value ? string.Empty : row["MC_ANAL20"].ToString(),
                MC_ANAL21 = row["MC_ANAL21"] == DBNull.Value ? string.Empty : row["MC_ANAL21"].ToString(),
                MC_ANAL22 = row["MC_ANAL22"] == DBNull.Value ? string.Empty : row["MC_ANAL22"].ToString(),
                MC_ANAL23 = row["MC_ANAL23"] == DBNull.Value ? string.Empty : row["MC_ANAL23"].ToString(),
                MC_ANAL24 = row["MC_ANAL24"] == DBNull.Value ? string.Empty : row["MC_ANAL24"].ToString(),
                MC_ANAL25 = row["MC_ANAL25"] == DBNull.Value ? string.Empty : row["MC_ANAL25"].ToString(),
                MC_ANAL26 = row["MC_ANAL26"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ANAL26"].ToString()),
                MC_TAX_CALC_MTD = row["MC_TAX_CALC_MTD"] == DBNull.Value ? string.Empty : row["MC_TAX_CALC_MTD"].ToString(),
                MC_RESMULTAXINV = row["MC_RESMULTAXINV"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_RESMULTAXINV"].ToString()),
                MC_IS_COM_ITM = row["MC_IS_COM_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_IS_COM_ITM"].ToString()),
                MC_WORKS_ON = row["MC_WORKS_ON"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_WORKS_ON"].ToString()),
                MC_AGE_SLOT = row["MC_AGE_SLOT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_AGE_SLOT"].ToString()),
                MC_WAVEOFF_VAL = row["MC_WAVEOFF_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_WAVEOFF_VAL"].ToString()),
                MC_IS_SCM2_FMS = row["MC_IS_SCM2_FMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_IS_SCM2_FMS"].ToString()),
                MC_IS_ECD = row["MC_IS_ECD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_IS_ECD"].ToString()),
                MC_IS_GRAN_WO_SER = row["MC_IS_GRAN_WO_SER"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_IS_GRAN_WO_SER"].ToString()),
                MC_ALW_MINUS_BAL = row["MC_ALW_MINUS_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ALW_MINUS_BAL"].ToString()),
                MC_ISSCM2 = row["MC_ISSCM2"] == DBNull.Value ? 0 : Convert.ToInt32(row["MC_ISSCM2"].ToString())
            };
        }
    } 

}
