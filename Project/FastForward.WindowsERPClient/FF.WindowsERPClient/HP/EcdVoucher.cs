using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Diagnostics;
using System.Drawing.Printing;

namespace FF.WindowsERPClient.HP
{
    public partial class EcdVoucher : Base
    {
        //sp_getschemes_ON_TP_CODE  =NEW
        //sp_getSchmeDet_on_term_tp  =new
        //seq_vou_def  =  NEW SEQUENCE
        //sp_save_hpr_ecd_vou_defn  =new
        //get_ecd_vou_defn_SchmList  =NEW
        //get_ecd_vou_defn_PClist =NEW
        //getAccFor_ecd_schems  =NEW  --CHECK AGAIN
        //get_ecd_vou_defn_onSeq =NEW
        //sp_update_ecd_vou_defn_prc  =NEW
        //sp_getVoucher  = NEW

        //get_vouchersToPrint =NEW
        //get_ecdDefn_PClist= NEW
        //get_ecd_vou_defn_PrintSchmList =NEW
        //sp_getVoucherOnSchemeRate  =NEW
        //SP_Update_ecdVou_PrintStatus  =NEW
        //sp_get_ECD_Voucher =NEW  -BY NADEEKA

        //get_processed_ecd_vou_PClist
        //sp_getscheme_type_by_cat =UPDATE

        DataTable select_PC_List = new DataTable();
        List<HpSchemeDetails> schDetList = new List<HpSchemeDetails>();

        public EcdVoucher()
        {
            InitializeComponent();
            LoadSchemeCategory();
            DropDownListSchemeCategory.SelectedIndex = -1;
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            //   ucProfitCenterSearch2.Company = BaseCls.GlbUserComCode;
            txtToBal.Text = "9999999";
            //9999999
        }
        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.DataSource = dt;
            DropDownListSchemeCategory.DisplayMember = "HSC_DESC";
            DropDownListSchemeCategory.ValueMember = "HSC_CD";
        }
        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            grvSchemes.DataSource = null;
            schDetList = new List<HpSchemeDetails>();
            grvSchemes.AutoGenerateColumns = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            EcdVoucher formnew = new EcdVoucher();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnAll_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAllPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNonPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            select_PC_List = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
        }

        private void checkBox_SCEME_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SCEME.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void checkBox_PC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PC.Checked == true)
            {
                this.btnAllPc_Click(null, null);
            }
            else
            {
                this.btnNonPc_Click(null, null);
            }
        }

        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.btnClearScheme_Click(null, null);
            LoadSchemeType();
            DropDownListSchemeType.SelectedIndex = -1;

            ddlSchemeTerms.DataSource = null;

            if (CheckBoxAll_SHcat.Checked == true)
            {
                DropDownListSchemeType.DataSource = null;
                DropDownListSchemeCategory.DataSource = null;
            }
            else
            {
                if (DropDownListSchemeCategory.SelectedIndex == -1)
                {
                    return;
                }

                List<int> termsList = new List<int>();
                List<HpSchemeDetails> schDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(DropDownListSchemeCategory.SelectedValue.ToString(), null, null);
                schDetList.ForEach(item =>
                {
                    var _duplicate = from _dup in termsList
                                     where _dup == item.Hsd_term
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        termsList.Add(item.Hsd_term);
                    }
                }
                );

                termsList.Sort();
                ddlSchemeTerms.DataSource = termsList;
            }
            //ddlSchemeTerms.DataSource = null;         
        }
        private void LoadSchemeType()
        {
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());
            if (_schemeList == null)
            {
                DropDownListSchemeType.DataSource = null;
                //return;
            }
            else
            {
                foreach (HpSchemeType sc in _schemeList)
                {
                    string space = "";
                    if ((10 - sc.Hst_cd.Length) > 0)
                    {
                        for (int i = 0; i <= (10 - sc.Hst_cd.Length); i++)
                        {
                            space = space + " ";
                        }
                    }
                    sc.Hst_desc = sc.Hst_cd + space + "--" + sc.Hst_desc;
                }

                DropDownListSchemeType.DataSource = _schemeList;
                DropDownListSchemeType.DisplayMember = "Hst_desc";
                DropDownListSchemeType.ValueMember = "Hst_cd";
            }
            CheckBoxAll_SHMtp.Checked = false;

        }

        private void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: LOAD TERM DDL
            //-------------------------------------------------------------------------------------------
            if (DropDownListSchemeType.SelectedIndex == -1)
            {
                return;
            }
            List<int> termsList = new List<int>();
            List<HpSchemeDetails> schDetList = CHNLSVC.Sales.GetSchemeByType_orCode(DropDownListSchemeCategory.SelectedValue.ToString(), DropDownListSchemeType.SelectedValue.ToString());

            schDetList.ForEach(item =>
            {
                var _duplicate = from _dup in termsList
                                 where _dup == item.Hsd_term
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    termsList.Add(item.Hsd_term);
                }
            }
            );
            termsList.Sort();
            ddlSchemeTerms.DataSource = termsList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: LOAD GRID
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            //this.btnClearPc_Click(null, null);
            //elect_PC_List = new DataTable();

            string oldCom = BaseCls.GlbUserComCode;
            string Company = ucProfitCenterSearch1.Company;
            //if (oldCom != Company)
            //{
            //    this.btnClearPc_Click(null, null);
            //}
            if (ucProfitCenterSearch1.Company == "")
            {
                return;
            }
            string com = ucProfitCenterSearch1.Company;
            string chanel = ucProfitCenterSearch1.Channel;
            string subChanel = ucProfitCenterSearch1.SubChannel;
            string area = ucProfitCenterSearch1.Area;
            string region = ucProfitCenterSearch1.Regien;
            string zone = ucProfitCenterSearch1.Zone;
            string pc = ucProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            //select_PC_List.Merge(dt);
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = dt;
            this.btnAllPc_Click(sender, e);
        }

        private void CheckBoxAll_SHMtp_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAll_SHMtp.Checked == true)
            {
                DropDownListSchemeType.SelectedIndex = -1;
                ddlSchemeTerms.DataSource = null;

                DropDownListSchemeType.DataSource = null;

                //TODO: LOAD TERM DDL
                //-------------------------------------------------------------------------------------------
                if (DropDownListSchemeCategory.SelectedIndex == -1)
                {
                    return;
                }
                List<int> termsList = new List<int>();
                List<HpSchemeDetails> schDetList = CHNLSVC.Sales.GetSchemeByType_orCode(DropDownListSchemeCategory.SelectedValue.ToString(), null);

                schDetList.ForEach(item =>
                {
                    var _duplicate = from _dup in termsList
                                     where _dup == item.Hsd_term
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        termsList.Add(item.Hsd_term);
                    }
                }
                );
                termsList.Sort();
                ddlSchemeTerms.DataSource = termsList;

            }
            else
            {
                this.DropDownListSchemeCategory_SelectedIndexChanged(null, null);
            }
        }

        private void CheckBoxAll_SHcat_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAll_SHcat.Checked == true)
            {
                // DropDownListSchemeCategory.SelectedIndex = -1;
                //TODO :BIND TERMS
                // ddlSchemeTerms.DataSource = null;
                DropDownListSchemeType.DataSource = null;
                DropDownListSchemeCategory.DataSource = null;

                //TODO: LOAD TERM DDL
                //-------------------------------------------------------------------------------------------              
                List<int> termsList = new List<int>();

                DataTable dt_SCH_CAT = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
                foreach (DataRow dr in dt_SCH_CAT.Rows)
                {
                    List<HpSchemeDetails> schDetList = CHNLSVC.Sales.GetSchemeByType_orCode(dr["HSC_CD"].ToString(), null);

                    schDetList.ForEach(item =>
                    {
                        var _duplicate = from _dup in termsList
                                         where _dup == item.Hsd_term
                                         select _dup;
                        if (_duplicate.Count() == 0)
                        {
                            termsList.Add(item.Hsd_term);
                        }
                    }
                    );
                }
                termsList.Sort();
                ddlSchemeTerms.DataSource = termsList;

            }

            else
            {
                LoadSchemeCategory();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ddlSchemeTerms.SelectedIndex = -1;
            //if (CheckBoxAll_terms.Checked == true)
            //{
            //    ddlSchemeTerms.DataSource = null;
            //}
            //else
            //{ 

            //}
        }

        private void bindShemes_toGrid()
        {
            grvSchemes.DataSource = null;
            grvSchemes.AutoGenerateColumns = false;
            List<HpSchemeDetails> temp_schDet_List = new List<HpSchemeDetails>();

            if (CheckBoxAll_SHcat.Checked == true)
            {
                //TODO: ADD SCHEMES

                if (CheckBoxAll_terms.Checked == true)
                {
                    List<HpSchemeDetails> temschDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(null, null, null);
                    if (temschDetList != null && temschDetList.Count > 0)
                    {
                        foreach (HpSchemeDetails rate in temschDetList)
                        {
                            List<HpSchemeDetails> tem = (from _res in schDetList
                                                         where _res.Hsd_cd == rate.Hsd_cd
                                                         select _res).ToList<HpSchemeDetails>();
                            if (tem.Count == 0)
                            {
                                List<HpSchemeDetails> temp = (from _res in temschDetList
                                                              where _res.Hsd_cd == rate.Hsd_cd
                                                              select _res).ToList<HpSchemeDetails>();
                                temp_schDet_List.AddRange(temp);

                            }
                        }
                        schDetList.AddRange(temschDetList);
                    }
                }
                else
                {
                    List<HpSchemeDetails> temschDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(null, null, Convert.ToInt32(ddlSchemeTerms.SelectedValue).ToString());
                    if (temschDetList != null && temschDetList.Count > 0)
                    {
                        foreach (HpSchemeDetails rate in temschDetList)
                        {
                            List<HpSchemeDetails> tem = (from _res in schDetList
                                                         where _res.Hsd_cd == rate.Hsd_cd
                                                         select _res).ToList<HpSchemeDetails>();
                            if (tem.Count == 0)
                            {
                                List<HpSchemeDetails> temp = (from _res in temschDetList
                                                              where _res.Hsd_cd == rate.Hsd_cd
                                                              select _res).ToList<HpSchemeDetails>();
                                temp_schDet_List.AddRange(temp);

                            }
                        }
                        schDetList.AddRange(temschDetList);
                    }
                }
                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                BindingSource _sou = new BindingSource();
                _sou.DataSource = schDetList;
                grvSchemes.DataSource = _sou;
            }
            else
            {


                if (CheckBoxAll_SHMtp.Checked == true)
                {
                    List<int> termsList = new List<int>();

                    if (CheckBoxAll_terms.Checked == true)
                    {
                        List<HpSchemeDetails> temschDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(DropDownListSchemeCategory.SelectedValue.ToString(), null, null);
                        if (temschDetList != null && temschDetList.Count > 0)
                        {
                            foreach (HpSchemeDetails rate in temschDetList)
                            {
                                List<HpSchemeDetails> tem = (from _res in schDetList
                                                             where _res.Hsd_cd == rate.Hsd_cd
                                                             select _res).ToList<HpSchemeDetails>();
                                if (tem.Count == 0)
                                {
                                    List<HpSchemeDetails> temp = (from _res in temschDetList
                                                                  where _res.Hsd_cd == rate.Hsd_cd
                                                                  select _res).ToList<HpSchemeDetails>();
                                    temp_schDet_List.AddRange(temp);

                                }
                            }
                            schDetList.AddRange(temschDetList);


                        }
                    }
                    else
                    {
                        List<HpSchemeDetails> temschDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(DropDownListSchemeCategory.SelectedValue.ToString(), null, Convert.ToInt32(ddlSchemeTerms.SelectedValue).ToString());
                        if (temschDetList != null && temschDetList.Count > 0)
                        {
                            foreach (HpSchemeDetails rate in temschDetList)
                            {
                                List<HpSchemeDetails> tem = (from _res in schDetList
                                                             where _res.Hsd_cd == rate.Hsd_cd
                                                             select _res).ToList<HpSchemeDetails>();
                                if (tem.Count == 0)
                                {
                                    List<HpSchemeDetails> temp = (from _res in temschDetList
                                                                  where _res.Hsd_cd == rate.Hsd_cd
                                                                  select _res).ToList<HpSchemeDetails>();
                                    temp_schDet_List.AddRange(temp);

                                }
                            }
                            schDetList.AddRange(temschDetList);


                        }
                    }
                }
                else
                {
                    if (DropDownListSchemeType.SelectedIndex == -1)
                    {
                        MessageBox.Show("Select scheme type", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //List<int> termsList = new List<int>();
                    if (CheckBoxAll_terms.Checked == true)
                    {
                        List<HpSchemeDetails> temschDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(DropDownListSchemeCategory.SelectedValue.ToString(), DropDownListSchemeType.SelectedValue.ToString(), null);

                        if (temschDetList != null && temschDetList.Count > 0)
                        {
                            foreach (HpSchemeDetails rate in temschDetList)
                            {
                                List<HpSchemeDetails> tem = (from _res in schDetList
                                                             where _res.Hsd_cd == rate.Hsd_cd
                                                             select _res).ToList<HpSchemeDetails>();
                                if (tem.Count == 0)
                                {
                                    List<HpSchemeDetails> temp = (from _res in temschDetList
                                                                  where _res.Hsd_cd == rate.Hsd_cd
                                                                  select _res).ToList<HpSchemeDetails>();
                                    temp_schDet_List.AddRange(temp);

                                }
                            }
                            schDetList.AddRange(temp_schDet_List);
                        }
                    }
                    else
                    {
                        List<HpSchemeDetails> temschDetList = CHNLSVC.Sales.GetSchmeDet_on_term_tp(DropDownListSchemeCategory.SelectedValue.ToString(), DropDownListSchemeType.SelectedValue.ToString(), Convert.ToInt32(ddlSchemeTerms.SelectedValue).ToString());
                        if (temschDetList != null && temschDetList.Count > 0)
                        {
                            foreach (HpSchemeDetails rate in temschDetList)
                            {
                                List<HpSchemeDetails> tem = (from _res in schDetList
                                                             where _res.Hsd_cd == rate.Hsd_cd
                                                             select _res).ToList<HpSchemeDetails>();
                                if (tem.Count == 0)
                                {
                                    List<HpSchemeDetails> temp = (from _res in temschDetList
                                                                  where _res.Hsd_cd == rate.Hsd_cd
                                                                  select _res).ToList<HpSchemeDetails>();
                                    temp_schDet_List.AddRange(temp);

                                }
                            }
                            schDetList.AddRange(temschDetList);


                        }
                    }
                }
                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                BindingSource _sou = new BindingSource();
                _sou.DataSource = schDetList;
                grvSchemes.DataSource = _sou;
            }
        }
        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            bindShemes_toGrid();
            //grvSchemes.DataSource = null;
            //grvSchemes.AutoGenerateColumns = false;

            //if (CheckBoxAll_SHcat.Checked == true)
            //{
            //    //TODO: ADD SCHEMES
            //}
            //else
            //{
            //    List<HpSchemeDetails> schDetList = new List<HpSchemeDetails>();
            //   // List<HpSchemeDetails> schDetList_ADD = new List<HpSchemeDetails>();
            //    if (CheckBoxAll_SHMtp.Checked == true)
            //    {
            //        List<int> termsList = new List<int>();                   
            //         schDetList = CHNLSVC.Sales.GetSchemeByType_orCode(DropDownListSchemeCategory.SelectedValue.ToString(), null);
            //         //schDetList_ADD = CHNLSVC.Sales.GetSchemeByType_orCode(DropDownListSchemeCategory.SelectedValue.ToString(), null);
            //         //-----------------------------------------------------------------------
            //         if (CheckBoxAll_terms.Checked == false)
            //         {
            //             if (ddlSchemeTerms.SelectedIndex == -1)
            //             {
            //                 MessageBox.Show("Select term", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                 return;
            //             }

            //             schDetList.ForEach(item =>
            //             {
            //                 if (item.Hsd_term != Convert.ToInt32(ddlSchemeTerms.SelectedValue))
            //                 {
            //                     schDetList.Remove(item);
            //                 }

            //             });
            //         }
            //    }
            //    else
            //    {
            //        if (DropDownListSchemeType.SelectedIndex==-1)
            //        {
            //            MessageBox.Show("Select scheme type","Select",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //            return;
            //        }
            //        List<int> termsList = new List<int>();
            //       // List<HpSchemeDetails> schDetList_Add = new List<HpSchemeDetails>();
            //       schDetList = CHNLSVC.Sales.GetSchemeByType_orCode(DropDownListSchemeCategory.SelectedValue.ToString(), DropDownListSchemeType.SelectedValue.ToString());
            //       //schDetList_ADD = CHNLSVC.Sales.GetSchemeByType_orCode(DropDownListSchemeCategory.SelectedValue.ToString(), DropDownListSchemeType.SelectedValue.ToString());
            //        //-----------------------------------------------------------------------
            //       if (CheckBoxAll_terms.Checked == false)
            //       {
            //           if (ddlSchemeTerms.SelectedIndex == -1)
            //           {
            //               MessageBox.Show("Select term", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //               return;
            //           }

            //           schDetList.ForEach(item =>
            //           {
            //               if (item.Hsd_term != Convert.ToInt32(ddlSchemeTerms.SelectedValue))
            //               {
            //                   schDetList.Remove(item);
            //               }

            //           });                    
            //       }
            //    }

            //    grvSchemes.DataSource = null;
            //    grvSchemes.AutoGenerateColumns = false;
            //    grvSchemes.DataSource = schDetList;

            //}

        }

        private List<string> get_selected_Schemes()
        {
            grvSchemes.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvSchemes.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_pc_list()
        {
            grvProfCents.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private void btnSaveDef_Click(object sender, EventArgs e)
        {
            List<string> list_schems = get_selected_Schemes();
            List<string> list_pc = get_selected_pc_list();

            if (list_schems.Count < 1 || list_pc.Count < 1)
            {
                MessageBox.Show("Please select atleaset one scheme and profit center to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ///////////////////////////////////////
            Decimal ecdRateORVal = 0;
            try
            {
                ecdRateORVal = Convert.ToDecimal(txtECDrt_amt.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Please enter a valid ECD rate/amount ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ecdRateORVal < 0)
            {
                MessageBox.Show("ECD rate/amount should be greater than 0. ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ecdRateORVal > 100 && rdoECDrt.Checked == true)
            {
                MessageBox.Show("ECD rate cannot be greater than 100. ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ////////////////////////////////////
            if (TextBoxFromDate.Value.Date > TextBoxToDate.Value.Date)
            {
                MessageBox.Show("From date should be less than To date ", "Invalid dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtFromBal.Text.Trim()) > Convert.ToDecimal(txtToBal.Text.Trim()))
            {
                MessageBox.Show("From value cannot be greater than To value ", "Invalid dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFromBal.Focus();
                return;
            }
            //if (TextBoxFromDate.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
            //{
            //    MessageBox.Show("From date cannot be less than current date", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    return;
            //}

            //Save_hpr_ecd_vou_defn
            ECDVoucher vou = new ECDVoucher();
            vou.Hvd_cre_by = BaseCls.GlbUserID; ;
            vou.Hvd_cre_dt = CHNLSVC.Security.GetServerDateTime().Date; ;
            vou.Hvd_ecd_val = Convert.ToDecimal(txtECDrt_amt.Text.Trim());
            vou.Hvd_from_bal = Convert.ToDecimal(txtFromBal.Text.Trim());
            vou.Hvd_from_dt = TextBoxFromDate.Value.Date;
            //vou.Hvd_is_prc  ;
            vou.Hvd_is_rt = rdoECDrt.Checked == true ? true : false;
            //vou.Hvd_pc;
            //vou.Hvd_prc_dt;
            //vou.Hvd_sch_cd;
            vou.Hvd_to_bal = Convert.ToDecimal(txtToBal.Text.Trim());
            vou.Hvd_to_dt = TextBoxToDate.Value.Date;
            vou.Hvd_acc_cr_from = TextBoxAccFrom.Value.Date;
            vou.Hvd_acc_cr_to = TextBoxAccTo.Value.Date;

            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Int32 eff = CHNLSVC.Sales.Save_hpr_ecd_vou_defn(list_schems, list_pc, vou);

            if (eff > 0)
            {
                MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);
            }
            else
            {
                MessageBox.Show("Not Saved!\nAdvice: please check for duplicates", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rdoECDrt_CheckedChanged(object sender, EventArgs e)
        {
            txtECDrt_amt.Text = "0.00";
            txtECDrt_amt.Focus();
        }

        private void rdoECDamt_CheckedChanged(object sender, EventArgs e)
        {
            txtECDrt_amt.Text = "0.00";
            txtECDrt_amt.Focus();
        }
        //********************************************************************************************************************
        private List<string> get_selected_Schemes_PROCESS()
        {
            grvSchemesPros.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvSchemesPros.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_pc_list_PROCESS()
        {
            grvPcPross.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPcPross.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<Int32> get_selected_Schemes_SEQ()
        {
            grvSchemesPros.EndEdit();
            List<Int32> list = new List<Int32>();
            foreach (DataGridViewRow dgvr in grvSchemesPros.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(Convert.ToInt32(dgvr.Cells["HVD_SEQ"].Value.ToString()));
                }
            }
            return list;
        }
        private void btnGetPcPross_Click(object sender, EventArgs e)
        {
            DateTime fromDt = datePickFromAct.Value.Date;
            DateTime toDt = datePickToAct.Value.Date;

            DataTable dt = CHNLSVC.Sales.Get_ecd_vou_defn_PClist(fromDt, toDt);

            grvPcPross.DataSource = false;
            grvPcPross.AutoGenerateColumns = false;
            grvPcPross.DataSource = dt;

            if (dt == null)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnGetSchems_Click(object sender, EventArgs e)
        {
            DateTime fromDt = datePickFromAct.Value.Date;
            DateTime toDt = datePickToAct.Value.Date;

            List<string> list_schems = get_selected_Schemes_PROCESS();
            List<string> list_pc = get_selected_pc_list_PROCESS();

            if (list_pc.Count < 1)
            {
                MessageBox.Show("Please select atleaset one scheme to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = CHNLSVC.Sales.Get_ecd_vou_defn_SchmList(list_pc, fromDt, toDt);
            grvSchemesPros.DataSource = null;
            grvSchemesPros.AutoGenerateColumns = false;
            grvSchemesPros.DataSource = dt;

            if (dt == null)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            List<Int32> Schemes_SEQlist = get_selected_Schemes_SEQ();

            if (Schemes_SEQlist.Count < 1)
            {
                MessageBox.Show("Please select atleaset one scheme to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //***********************************************************************
            EarlyClosingDiscount ECD = new EarlyClosingDiscount();
            //ECD.Hed_acc_no; //TODO:
            //ECD.Hed_comit = commit;
            ECD.Hed_cre_by = BaseCls.GlbUserID;
            ECD.Hed_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
            //ECD.Hed_ecd_base = ecdBase;
            //ECD.Hed_ecd_cls_val= ;
            //ECD.Hed_ecd_is_rt = isECD_rate;//TODO:
            //ECD.Hed_ecd_val =;  //TODO:
            //ECD.Hed_eff_acc_tp = effAccTp;
            //ECD.Hed_eff_cre_dt = effAccCreatDt;
            //ECD.Hed_eff_dt = date_effCreDt.Value.Date;
            //ECD.Hed_from_dt = TextBoxFromDate.Value.Date;
            //ECD.Hed_from_pd;
            //ECD.Hed_is_prt;
            //ECD.Hed_is_use;
            // ECD.Hed_pb= DropDownListPriceBook.SelectedValue.ToString();   //ASSIGNED LATER IN SERVICE
            //ECD.Hed_pb_lvl; ASSIGNED LATER IN SERVICE
            //ECD.Hed_prt_by;
            //ECD.Hed_prt_dt;
            //ECD.Hed_pty_cd; = ASSIGNED LATER IN SERVICE //TODO:
            ECD.Hed_pty_tp = "PC";
            //ECD.Hed_sch_cd;  ASSIGNED LATER IN SERVICE //TODO:
            //ECD.Hed_seq;
            //ECD.Hed_to_dt = TextBoxToDate.Value.Date;
            //ECD.Hed_to_pd;
            ECD.Hed_tp = "V";
            //ECD.Hed_use_dt;
            //ECD.Hed_val = ecdRateORVal; //TODO:
            //ECD.Hed_vou_no; //TODO:


            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = 0;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "ECD";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "ECDVOU";
            masterAuto.Aut_year = null;

            if (MessageBox.Show("Are you sure to process?", "Confirm Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //CHNLSVC.Sales.Get_AccountBalance(reciptDt, Account.Hpa_acc_no);
            DateTime fromDt = datePickFromAct.Value.Date;
            DateTime toDt = datePickToAct.Value.Date;
            List<string> vouchersList = new List<string>();
            string _error;
            Int32 eff = CHNLSVC.Sales.Process_hpr_ecd_vouchers(masterAuto, Schemes_SEQlist, ECD, CHNLSVC.Security.GetServerDateTime().Date, fromDt, toDt, out vouchersList, out _error);
            if (eff > 0)
            {
                MessageBox.Show("Successfully processed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // btnClear_Click(sender, e);
                this.btnClearPcPross_Click(null, null);
                this.btnClearSchPross_Click(null, null);

            }
            else
            {
                MessageBox.Show("Sorry. Not Processed!\n" + _error, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                grvECD_Def.DataSource = null;
                grvECD_Def.AutoGenerateColumns = false;
            }
            lblGenVouCount.Text = vouchersList.Count.ToString();
            MessageBox.Show("No Of vouchers: " + vouchersList.Count.ToString() + "\n\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //DISPLAY GENERATED VOUCHERS.
            if (vouchersList.Count > 0)
            {
                DataTable dt = CHNLSVC.Sales.Get_voucher_details(vouchersList);
                grvECD_Def.DataSource = null;
                grvECD_Def.AutoGenerateColumns = false;
                grvECD_Def.DataSource = dt;
            }
            else
            {
                grvECD_Def.DataSource = null;
                grvECD_Def.AutoGenerateColumns = false;
            }

        }

        private void chckAllPcPros_CheckedChanged(object sender, EventArgs e)
        {
            if (chckAllPcPros.Checked == true)
            {
                this.btnAllPcPross_Click(null, null);
            }
            else
            {
                this.btnNonPcPross_Click(null, null);
            }
        }

        private void btnNonPcPross_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPcPross.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvPcPross.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAllPcPross_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPcPross.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvPcPross.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearPcPross_Click(object sender, EventArgs e)
        {
            grvPcPross.DataSource = null;
            grvPcPross.AutoGenerateColumns = false;
        }

        private void btnClearSchPross_Click(object sender, EventArgs e)
        {
            grvSchemesPros.DataSource = null;
            grvSchemesPros.AutoGenerateColumns = false;
        }

        private void btnAllSchPross_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemesPros.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvSchemesPros.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNoneSchmPross_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemesPros.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvSchemesPros.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void chckAllSchmPros_CheckedChanged(object sender, EventArgs e)
        {
            if (chckAllSchmPros.Checked == true)
            {
                this.btnAllSchPross_Click(null, null);
            }
            else
            {
                this.btnNoneSchmPross_Click(null, null);
            }
        }

        private void chkAllPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPrint.Checked == true)
            {
                this.btnAllPrint_Click(null, null);
            }
            else
            {
                this.btnNonePrint_Click(null, null);
            }
        }

        private void btnAllPrint_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPrintVou.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvPrintVou.EndEdit();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnNonePrint_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPrintVou.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvPrintVou.EndEdit();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnClearPrint_Click(object sender, EventArgs e)
        {
            grvPrintVou.DataSource = null;
            grvPrintVou.AutoGenerateColumns = false;
        }

        private void btnAddToPrint_Click(object sender, EventArgs e)
        {
            //List<string> pc_list = new List<string>();

            //if(chkAllVouPc.Checked==true)
            //{
            //    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(ucProfitCenterSearch2.Company, "","", "","", "", "");
            //    foreach(DataRow drow in dt.Rows)
            //    {
            //        string pc = drow["PROFIT_CENTER"].ToString();
            //        pc_list.Add(pc);
            //    }
            //}
            //else
            //{
            //    string pc = ucProfitCenterSearch2.ProfitCenter;
            //    pc_list.Add(pc);
            //}

            //DataTable dt_vou= CHNLSVC.Sales.Get_vouchers_to_Print(pc_list);
            //grvPrintVou.DataSource = null;
            //grvPrintVou.AutoGenerateColumns = false;
            //grvPrintVou.DataSource = dt_vou;

            //if (dt_vou==null)
            //{
            //    MessageBox.Show("No processed vouchers found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (dt_vou.Rows.Count==0)
            //{
            //    MessageBox.Show("No processed vouchers found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
        }

        private List<string> get_selected_voucherNumbers_TO_Print()
        {
            //  List<string> print_vou_list = new List<string>();
            grvPrintVou.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPrintVou.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }


        private void chkAllVouPc_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkAllVouPc.Checked == true)
            //{    
            //    ucProfitCenterSearch2.ClearScreen(ucProfitCenterSearch2.Company);
            //}           
        }

        private void btnGetPcVou_Click(object sender, EventArgs e)
        {
            DateTime fromDt = dateFromVou.Value.Date;
            DateTime toDt = dateToVou.Value.Date;

            //DataTable dt = CHNLSVC.Sales.Get_ecd_vou_defn_PClist(fromDt, toDt);
            DataTable dt = CHNLSVC.Sales.Get_Processed_ecd_vou_defn_PClist(fromDt, toDt);

            grvPC_Vou.DataSource = false;
            grvPC_Vou.AutoGenerateColumns = false;
            grvPC_Vou.DataSource = dt;

            this.btnClearVouSchm_Click(null, null);
            this.btnClearPrint_Click(null, null);

            if (dt == null)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }



        private void btnNonePcPrint_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPC_Vou.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvPC_Vou.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAllPcPrint_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPC_Vou.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvPC_Vou.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearPcPrint_Click(object sender, EventArgs e)
        {
            grvPC_Vou.DataSource = null;
            grvPC_Vou.AutoGenerateColumns = false;
        }

        private List<string> get_selected_pc_list_PRINT()
        {
            grvPC_Vou.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPC_Vou.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_Schemes_PRINT()
        {
            grv_schemes_Vou.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grv_schemes_Vou.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }

        private void btnGetSchmeVou_Click(object sender, EventArgs e)
        {
            DateTime fromDt = dateFromVou.Value.Date;
            DateTime toDt = dateToVou.Value.Date;

            // List<string> list_schems = get_selected_Schemes_PROCESS();
            List<string> list_pc = get_selected_pc_list_PRINT();

            if (list_pc.Count < 1)
            {
                MessageBox.Show("Please select one or more profit centers.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dt = CHNLSVC.Sales.Get_ecd_vou_defn_For_Print_SchmList(list_pc, fromDt, toDt);
            grv_schemes_Vou.DataSource = null;
            grv_schemes_Vou.AutoGenerateColumns = false;
            grv_schemes_Vou.DataSource = dt;

            if (dt == null)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void grv_schemes_Vou_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex != -1)
            //{
            //MessageBox.Show("K");
            grv_schemes_Vou.EndEdit();
            List<string> rateList_bind = new List<string>();
            List<string> rateList = get_selected_SchemesRates_PRINT();
            foreach (string rate in rateList)
            {
                // rateList_bind.Add(rate);
                var _duplicate = from _dup in rateList_bind
                                 where _dup == rate
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    rateList_bind.Add(rate);
                }
            }
            ddlSchmRate.DataSource = rateList_bind;
            // if (ddlSchmRate.DroppedDown==false)
            // {
            //  ddlSchmRate.DroppedDown = true;
            //  }

            //}
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.btnAllPcPrint_Click(null, null);
            }
            else
            {
                this.btnNonePcPrint_Click(null, null);
            }
        }

        private List<string> get_selected_SchemesRates_PRINT()
        {
            grv_schemes_Vou.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grv_schemes_Vou.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[3].Value.ToString());
                }
            }
            return list;
        }

        private void btnGetRates_Click(object sender, EventArgs e)
        {
            List<string> rateList_bind = new List<string>();
            List<string> rateList = get_selected_SchemesRates_PRINT();
            foreach (string rate in rateList)
            {
                // rateList_bind.Add(rate);
                var _duplicate = from _dup in rateList_bind
                                 where _dup == rate
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    rateList_bind.Add(rate);
                }
            }
            ddlSchmRate.DataSource = rateList_bind;
        }

        private void btnClearVouSchm_Click(object sender, EventArgs e)
        {
            grv_schemes_Vou.DataSource = null;
            grv_schemes_Vou.AutoGenerateColumns = false;

            ddlSchmRate.DataSource = null;
            this.btnClearPrint_Click(null, null);

        }

        private void btnAllVouSchm_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grv_schemes_Vou.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grv_schemes_Vou.EndEdit();
            }
            catch (Exception ex)
            {

            }

            this.btnGetRates_Click(null, null);
        }

        private void ddlSchmRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchmRate.SelectedIndex != -1)
            {
                //string rate = ddlSchmRate.SelectedValue.ToString();
                //MessageBox.Show(rate, "Data found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGetVouchers_Click(object sender, EventArgs e)
        {
            if (ddlSchmRate.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                DateTime fromDt = dateFromVou.Value.Date;
                DateTime toDt = dateToVou.Value.Date;

                List<string> list_pc = get_selected_pc_list_PRINT();
                List<string> schemsList = get_selected_Schemes_PRINT();

                var schemsList_dis = (from t in schemsList
                                      select t).Distinct();

                List<Decimal> rateList = new List<Decimal>();
                if (chkAllRateAmt.Checked == true)
                {
                    List<string> rateList_bind = new List<string>();
                    List<string> rateList_ = get_selected_SchemesRates_PRINT();
                    foreach (string rate in rateList_)
                    {
                        // rateList_bind.Add(rate);
                        var _duplicate = from _dup in rateList_bind
                                         where _dup == rate
                                         select _dup;
                        if (_duplicate.Count() == 0)
                        {
                            rateList_bind.Add(rate);
                            rateList.Add(Convert.ToDecimal(rate));
                        }
                    }

                    //rateList = rateList_bind;
                }
                else
                {
                    Decimal rate = Convert.ToDecimal(ddlSchmRate.SelectedValue.ToString());
                    rateList.Add(rate);
                }


                if (schemsList.Count < 1 || list_pc.Count < 1)
                {
                    MessageBox.Show("Please select atleaset one scheme and profit center to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable dt = CHNLSVC.Sales.Get_VoucherOnSchemeRate(list_pc, schemsList_dis.ToList(), fromDt, toDt, rateList);
                grvPrintVou.DataSource = null;
                grvPrintVou.AutoGenerateColumns = false;
                grvPrintVou.DataSource = dt;
                // Get_VoucherOnSchemeRate

                if (dt == null)
                {
                    MessageBox.Show("No vouchers found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No vouchers found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void dateFromVou_ValueChanged(object sender, EventArgs e)
        {
            this.btnClearVouSchm_Click(null, null);
            this.btnClearPrint_Click(null, null);
            this.btnClearPcPrint_Click(null, null);
        }

        private void dateToVou_ValueChanged(object sender, EventArgs e)
        {
            this.btnClearVouSchm_Click(null, null);
            this.btnClearPrint_Click(null, null);
            this.btnClearPcPrint_Click(null, null);
        }

        private void chkAllSchmeVou_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.btnAllVouSchm_Click(null, null);
            }
            else
            {
                this.btnNonePcPrint_Click(null, null);
            }
            this.btnGetRates_Click(null, null);
        }

        private void btnNoneVouSchm_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grv_schemes_Vou.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grv_schemes_Vou.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        //****************************************FOR PRINTING**********************************************
        private void btnPrintVouchers_Click(object sender, EventArgs e)
        {
            List<string> selectVou_list = get_selected_voucherNumbers_TO_Print();
            if (selectVou_list.Count == 0)
            {
                MessageBox.Show("Select one or more vouchers to print!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure to print?", "Confirm Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

                 DataTable ECD_VOU_PRINT   = new DataTable();
            foreach (string voucherNo in selectVou_list)
            {
                ////TODO: PRINT VOUCHERS
                //// if (BaseCls.GlbReportName == "EcdVouchar.rpt")
                //// {

                ////D:\SCM Web\FastForwardERP.Project\FastForward.WindowsERPClient\FF.WindowsERPClient\Reports\Finance\ReportViewerFinance.cs

                //BaseCls.GlbReportName = "EcdVouchar.rpt";
                //FF.WindowsERPClient.Reports.Finance.clsFinanceRep obj = new FF.WindowsERPClient.Reports.Finance.clsFinanceRep();
                //obj._chqSts.PrintOptions.PrinterName = GetDefaultPrinter();
                //int traynbr = gettraynbr("Manual"); // returns 4 int
                //int papernbr = getprtnbr("Letter"); // returns 257 int
                //obj._chqSts.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
                //obj._chqSts.PrintOptions.PaperSource = (CrystalDecisions.Shared.PaperSource)traynbr;
                //obj._chqSts.PrintToPrinter(1, false, 0, 0);

                //// };

                // 23-03-2015 Nadeeka (Modified Printing function)
                DataTable TMP_ECD_VOU_PRINT = new DataTable();
                TMP_ECD_VOU_PRINT = CHNLSVC.Sales.ECD_vouchers_Print(voucherNo);
                ECD_VOU_PRINT.Merge(TMP_ECD_VOU_PRINT);
            

                //TODO: UPDATE VOUCHER PRINT STATUS
                CHNLSVC.Sales.Update_ECD_Voucher_printStatus(voucherNo, BaseCls.GlbUserID, CHNLSVC.Security.GetServerDateTime());
            }


            if (optPrint.Checked == true)
            {
                clsSalesRep objrep = new clsSalesRep();

                //objrep.ECDVoucher(ECD_VOU_PRINT);
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                BaseCls.GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;

                BaseCls.GlbReportName = "EcdVouchar.rpt";
                _view.GlbReportName = "EcdVouchar.rpt";
                BaseCls.GlbReportDataTable = ECD_VOU_PRINT;

                _view.Show();
                _view = null;

                MessageBox.Show("Print Completed!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string _err="";
                if (ECD_VOU_PRINT.Rows.Count > 0)
                {
                    foreach (DataRow drow in ECD_VOU_PRINT.Rows)
                    {
                        string _msg = "";
                        if (BaseCls.GlbUserComCode == "ABL" || BaseCls.GlbUserComCode == "LRP")
                        {
                            _msg = "Dear Customer, for 1st 100 customers 15% discount on present balance, if close the A/C. T/C applies. Abans-0112301349 " + drow["hed_acc_no"].ToString() + " Vou. No-" + drow["hed_vou_no"].ToString();
                        }
                        if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD")
                        {
                            _msg = "Dear Customer, for 1st 100 customers 15% discount on present balance, if close the A/C. T/C applies. Singhagiri-0114322178 " + drow["hed_acc_no"].ToString() + " Vou. No-" + drow["hed_vou_no"].ToString();
                        }
                        if (BaseCls.GlbUserComCode == "PNG")
                        {
                            _msg = "Dear Customer, for 1st 100 customers 15% discount on present balance, if close the A/C. T/C applies. P&G-0114322178 " + drow["hed_acc_no"].ToString() + " Vou. No-" + drow["hed_vou_no"].ToString();
                        }
                        if (_msg=="")
                        {
                            MessageBox.Show("SMS Send Failed! Message not set up.");
                            return;
                        }
                        int i = CHNLSVC.Sales.send_SMS_ecd_vouchers(drow["hpa_com"].ToString(), drow["htc_cust_cd"].ToString(), drow["hed_acc_no"].ToString(), drow["hed_vou_no"].ToString(), _msg, BaseCls.GlbUserID, out _err);

                        if (i == -1)
                        {
                            MessageBox.Show("SMS Send Failed! Voucher :" + drow["hed_vou_no"].ToString() +" "+ _err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                    MessageBox.Show("SMS Send Completed!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            


            
            this.btnClearPrint_Click(null, null);
            this.btnClearVouSchm_Click(null, null);
            this.btnClearPcPrint_Click(null, null);
        }
        private void datePickFromAct_ValueChanged(object sender, EventArgs e)
        {
            this.btnClearSchPross_Click(null, null);
            this.btnClearPcPross_Click(null, null);
            this.btnClearGenVou_Click(null, null);
        }

        private void datePickToAct_ValueChanged(object sender, EventArgs e)
        {
            this.btnClearSchPross_Click(null, null);
            this.btnClearPcPross_Click(null, null);
            this.btnClearGenVou_Click(null, null);
        }

        private void btnClearGenVou_Click(object sender, EventArgs e)
        {
            grvECD_Def.DataSource = null;
            grvECD_Def.AutoGenerateColumns = false;
        }
        //***************************************************************************************************
    }
}
