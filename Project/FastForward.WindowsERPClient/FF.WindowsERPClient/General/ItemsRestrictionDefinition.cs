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
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.General
{
    //SELECT *from sar_price_type

    public partial class ItemsRestrictionDefinition :Base
    {
        //sp_save_MST_ITM_BLOCK = NEW

        DataTable select_PC_List = new DataTable();
        DataTable select_LOC_List = new DataTable();
        DataTable select_Chnl_List = new DataTable();
        DataTable select_SbChnl_List = new DataTable();
        DataTable select_ITEMS_List = new DataTable();
        DataTable select_Brand_List = new DataTable();
        

        public ItemsRestrictionDefinition()
        {
            InitializeComponent();

            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            ucLoactionSearch1.Company = BaseCls.GlbUserComCode;
            //TODO: BIND PRICE TYPES.
            grvPriceTp.DataSource = null;
            grvPriceTp.AutoGenerateColumns = false;
            grvPriceTp.DataSource= CHNLSVC.General.Get_sar_price_type("");
            grvPriceTp.AutoGenerateColumns = false;          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ItemsRestrictionDefinition formnew = new ItemsRestrictionDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        //---------------------------------------------------------------------------------------
        private List<string> get_Selected_PC_List()
        {
            grvProfCents.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());//PC CODE
                }
            }
            return list;
        }

        private List<string> get_Selected_LOC_List()
        {
            grvLocations.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvLocations.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString()); //LOCATION CODE
                }
            }
            return list;
        }
        private List<string> get_Selected_Chnl_List()
        {
            grvChnl.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvChnl.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString()); //channel CODE
                }
            }
            return list;
        }
        private List<string> get_Selected_SubChnl_List()
        {
            grvsbchnl.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvsbchnl.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString()); //channel CODE
                }
            }
            return list;
        }

        private Dictionary<string,string> get_Selected_Items_List()
        {
            grvItemList.EndEdit();
            List<string> list = new List<string>();
            Dictionary<string, string> itme_Dic = new Dictionary<string, string>();

            foreach (DataGridViewRow dgvr in grvItemList.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString()); //ITEM CODE
                    itme_Dic.Add(dgvr.Cells[1].Value.ToString(), dgvr.Cells[2].Value.ToString());
                }
            }
            //return list;
             return itme_Dic;
        }


        //---------------------------------------------------------------------------------------
       // private List<string> get_Selected_PRICE_TP_List()
        private Dictionary<string, string> get_Selected_PRICE_TP_List()
        {
            grvPriceTp.EndEdit();
            List<string> list = new List<string>();
            Dictionary<string, string> PriceTp_Dic = new Dictionary<string, string>();

            foreach (DataGridViewRow dgvr in grvPriceTp.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString()); //INDEX
                    dgvr.Cells[0].Value.ToString();
                    PriceTp_Dic.Add(dgvr.Cells["SARPT_INDI"].Value.ToString(), dgvr.Cells["SARPT_CD"].Value.ToString() + " - " + dgvr.Cells["SARPT_DESC"].Value.ToString());
                }
            }
            //return list;
            return PriceTp_Dic;
        }
        //--------------------------------------------------------------------------
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        //ucLCSE001
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + TextBoxChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }                           
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + "" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(txtItemCD.Text + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append("" + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
            
             
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
             
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }          
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
              
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //List<string> Selected_ITEMS_List = get_Selected_Items_List();
            if (txtCom.Text.Trim()=="")
            {
                MessageBox.Show("Enter company","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            Dictionary<string, string> Selected_ITEMS_Dic = new Dictionary<string, string>();
            if (radItm.Checked == true)
            {
                 Selected_ITEMS_Dic = get_Selected_Items_List();
                if (Selected_ITEMS_Dic.Count == 0)
                {
                    MessageBox.Show("Please select item(s)!", "Restrict Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {

                if (grvBMS.Rows.Count == 0)
                {
                    MessageBox.Show("Please select item(s)!", "Restrict Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //-----------------------------------------------
            Dictionary<string, string> Selected_PRICE_TP_Dic = get_Selected_PRICE_TP_List();
            if (Selected_PRICE_TP_Dic.Count == 0)
            {
                MessageBox.Show("Please select price type(s)!", "Price Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-----------------------------------------------
            List<MasterItemBlock> SAVE_BLOCK_LIST = new List<MasterItemBlock>();

            int OuterLoopCount = 0;

            foreach (KeyValuePair<string, string> Price_pair in Selected_PRICE_TP_Dic)
            {
                if (radItm.Checked == true)
                {
                    //builder.AppendFormat("{0}:{1}.", pair.Key, pair.Value);     
                    foreach (KeyValuePair<string, string> Item_pair in Selected_ITEMS_Dic)
                    {
                        if (tabControl1.SelectedTab.Name == "tabPage1")
                        {
                            List<string> Selected_PC_List = get_Selected_PC_List();
                            if (Selected_PC_List.Count > 0)
                            {
                                //TODO: -LOOP PC LIST
                                foreach (string pc_code in Selected_PC_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "P";
                                    itmBloc.Mib_category = pc_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = Item_pair.Key;
                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                // itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = Item_pair.Key;
                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;

                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        if (tabControl1.SelectedTab.Name == "tabPage2")
                        {
                            //MessageBox.Show("tabPage2");
                            List<string> Selected_LOC_List = get_Selected_LOC_List();
                            if (Selected_LOC_List.Count > 0)
                            {
                                //TODO: -LOOP LOC LIST
                                foreach (string loc_code in Selected_LOC_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "L";
                                    itmBloc.Mib_category = loc_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = Item_pair.Key;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                //itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = Item_pair.Key;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        if (tabControl1.SelectedTab.Name == "tabPage3")
                        {
                            //MessageBox.Show("tabPage2");
                            List<string> Selected_Chnl_List = get_Selected_Chnl_List();
                            if (Selected_Chnl_List.Count > 0)
                            {
                                //TODO: -LOOP LOC LIST
                                foreach (string chnl_code in Selected_Chnl_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "C";
                                    itmBloc.Mib_category = chnl_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = Item_pair.Key;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                //itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = Item_pair.Key;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        if (tabControl1.SelectedTab.Name == "tabPage4")
                        {
                            //MessageBox.Show("tabPage2");
                            List<string> Selected_SubChnl_List = get_Selected_SubChnl_List();
                            if (Selected_SubChnl_List.Count > 0)
                            {
                                //TODO: -LOOP LOC LIST
                                foreach (string loc_code in Selected_SubChnl_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "S";
                                    itmBloc.Mib_category = loc_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = Item_pair.Key;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                //itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = Item_pair.Key;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                    }
                }
                else    //kapila 23/11/2016
                {
                    foreach (DataGridViewRow r in grvBMS.Rows)
                    {
                        if (tabControl1.SelectedTab.Name == "tabPage1")
                        {
                            List<string> Selected_PC_List = get_Selected_PC_List();
                            if (Selected_PC_List.Count > 0)
                            {
                                //TODO: -LOOP PC LIST
                                foreach (string pc_code in Selected_PC_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "P";
                                    itmBloc.Mib_category = pc_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = "";
                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                    itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                    itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                // itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = "";
                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();

                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        if (tabControl1.SelectedTab.Name == "tabPage2")
                        {
                            //MessageBox.Show("tabPage2");
                            List<string> Selected_LOC_List = get_Selected_LOC_List();
                            if (Selected_LOC_List.Count > 0)
                            {
                                //TODO: -LOOP LOC LIST
                                foreach (string loc_code in Selected_LOC_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "L";
                                    itmBloc.Mib_category = loc_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = "";

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                    itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                    itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                //itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = "";

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();
                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        if (tabControl1.SelectedTab.Name == "tabPage3")
                        {
                            //MessageBox.Show("tabPage2");
                            List<string> Selected_Chnl_List = get_Selected_Chnl_List();
                            if (Selected_Chnl_List.Count > 0)
                            {
                                //TODO: -LOOP LOC LIST
                                foreach (string chnl_code in Selected_Chnl_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "C";
                                    itmBloc.Mib_category = chnl_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = "";

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                    itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                    itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                //itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = "";

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();
                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        if (tabControl1.SelectedTab.Name == "tabPage4")
                        {
                            //MessageBox.Show("tabPage2");
                            List<string> Selected_SubChnl_List = get_Selected_SubChnl_List();
                            if (Selected_SubChnl_List.Count > 0)
                            {
                                //TODO: -LOOP LOC LIST
                                foreach (string loc_code in Selected_SubChnl_List)
                                {
                                    MasterItemBlock itmBloc = new MasterItemBlock();
                                    itmBloc.Mbi_category_tp = "S";
                                    itmBloc.Mib_category = loc_code;
                                    itmBloc.Mib_com = txtCom.Text.Trim();
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_dt = DateTime.Now;
                                    itmBloc.Mib_itm = "";

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                    itmBloc.Mib_mod_dt = DateTime.Now;
                                    itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                    itmBloc.Mib_stus = chkActive.Checked;

                                    itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                    itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                    itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                    itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();

                                    SAVE_BLOCK_LIST.Add(itmBloc);
                                }
                            }
                            else
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                //itmBloc.Mbi_category_tp = "N/A";
                                // itmBloc.Mib_category = "N/A";
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = "";

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);

                                itmBloc.Mib_stus = chkActive.Checked;

                                itmBloc.Mib_mod_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_brnd = r.Cells["Brand"].Value.ToString();
                                itmBloc.Mib_cat1 = r.Cells["Main_Cat"].Value.ToString();
                                itmBloc.Mib_cat2 = r.Cells["Sub_Cat"].Value.ToString();
                                SAVE_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                Int32 _CNT = SAVE_BLOCK_LIST.Count;
                Int32 _NextCNT = 0;
                //foreach (MasterItemBlock _one in SAVE_BLOCK_LIST)
                //{
                //    List<MasterItemBlock> _two = new List<MasterItemBlock>();
                //    _two.Add(_one);
                //    CHNLSVC.General.Save_MST_ITM_BLOCK(_two, 1);
                //    _NextCNT = _NextCNT + 1;
                //}

                int result = CHNLSVC.General.Save_MST_ITM_BLOCK(SAVE_BLOCK_LIST, 1);
                OuterLoopCount += 1;

                if (result != _CNT)
                {
                    break;
                }

                this.Cursor = Cursors.Default;
               
                //Int32 eff = CHNLSVC.General.Save_MST_ITM_BLOCK(SAVE_BLOCK_LIST, 1);
                //if (_NextCNT ==_CNT)
                //{
                //    MessageBox.Show("Successfully saved!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //    this.btnClear_Click(null, null);
                //}
                //else
                //{
                //    MessageBox.Show("Not saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            if (OuterLoopCount == Selected_PRICE_TP_Dic.Count)
            {
                MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);
                
            }
            else
            {
                MessageBox.Show("Not saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //--------------
        }

        //private void btnAdd_Click(object sender, EventArgs e)
        //{

        //}

        private void btnBrand_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBrand;
            //_CommonSearch.txtSearchbyword.Text = txtBrand.Text;
            _CommonSearch.ShowDialog();
            txtBrand.Focus();
        }

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate1;
           // _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
            _CommonSearch.ShowDialog();
            txtCate1.Focus();
        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate2;
          //  _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
            _CommonSearch.ShowDialog();
            txtCate2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
           // _CommonSearch.ReturnIndex = 0;
           // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
           // DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
           // _CommonSearch.dvResult.DataSource = _result;
           // _CommonSearch.BindUCtrlDDLData(_result);
           // _CommonSearch.obj_TragetTextBox = txtItemCD;
           //// _CommonSearch.txtSearchbyword.Text = txtItemCD.Text;
           // _CommonSearch.ShowDialog();
           // txtItemCD.Focus();

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemCD;
            _CommonSearch.ShowDialog();
            txtItemCD.Focus();

            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchType = "ITEMS";
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtItemCD;
            //_CommonSearch.ShowDialog();
            //txtItemCD.Select();
        }

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            if (radItm.Checked == true)
            {
                if (txtBrand.Text.Trim() == "" && txtCate1.Text.Trim() == "" && txtCate2.Text.Trim() == "" && txtItemCD.Text.Trim().ToUpper() == "")
                {
                    MessageBox.Show("Please select a searching value", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //select_ITEMS_List = new DataTable();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("ITEM", txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), null, txtItemCD.Text.Trim(), string.Empty, string.Empty, string.Empty);
                select_ITEMS_List.Merge(dt);
                grvItemList.DataSource = null;
                grvItemList.AutoGenerateColumns = false;
                grvItemList.DataSource = select_ITEMS_List;
            }
            else
            {
                DataTable tem = new DataTable();
                tem.Columns.Add("Brand");
                tem.Columns.Add("Main_Cat");
                tem.Columns.Add("Sub_Cat");

                if (radBMS.Checked == true)
                {
                    if(string.IsNullOrEmpty( txtBrand.Text) || string.IsNullOrEmpty(txtCate1.Text) || string.IsNullOrEmpty(txtCate2.Text) )
                    {
                        MessageBox.Show("Please select Brand/Main Cat./Sub Cat.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var _duplicate = from _dup in select_Brand_List.AsEnumerable()
                                     where _dup["Brand"].ToString() == txtBrand.Text && _dup["Main_Cat"].ToString() == txtCate1.Text && _dup["Sub_Cat"].ToString() == txtCate2.Text
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        MessageBox.Show("Already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = txtBrand.Text;
                    dr[1] = txtCate1.Text;
                    dr[2] = txtCate2.Text;

                    tem.Rows.Add(dr);

                    select_Brand_List.Merge(tem);
                }
                if (radBM.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtBrand.Text) || string.IsNullOrEmpty(txtCate1.Text) )
                    {
                        MessageBox.Show("Please select Brand/Main Cat.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var _duplicate = from _dup in select_Brand_List.AsEnumerable()
                                     where _dup["Brand"].ToString() == txtBrand.Text && _dup["Main_Cat"].ToString() == txtCate1.Text && string.IsNullOrEmpty(_dup["Sub_Cat"].ToString())
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        MessageBox.Show("Already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = txtBrand.Text;
                    dr[1] = txtCate1.Text;
                    dr[2] = "";

                    tem.Rows.Add(dr);

                    select_Brand_List.Merge(tem);
                }
                if (radMS.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtCate1.Text) || string.IsNullOrEmpty(txtCate2.Text))
                    {
                        MessageBox.Show("Please select Main Cat./Sub Cat.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var _duplicate = from _dup in select_Brand_List.AsEnumerable()
                                     where string.IsNullOrEmpty(_dup["Brand"].ToString()) && _dup["Main_Cat"].ToString() == txtCate1.Text && _dup["Sub_Cat"].ToString() == txtCate2.Text
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        MessageBox.Show("Already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = "";
                    dr[1] = txtCate1.Text;
                    dr[2] = txtCate2.Text;

                    tem.Rows.Add(dr);

                    select_Brand_List.Merge(tem);
                }
                if (radB.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtBrand.Text))
                    {
                        MessageBox.Show("Please select Brand", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var _duplicate = from _dup in select_Brand_List.AsEnumerable()
                                     where _dup["Brand"].ToString() == txtBrand.Text && string.IsNullOrEmpty(_dup["Main_Cat"].ToString()) && string.IsNullOrEmpty(_dup["Sub_Cat"].ToString())
                                     select _dup;
                    if (_duplicate.Count() != 0)
                    {
                        MessageBox.Show("Already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = txtBrand.Text;
                    dr[1] = "";
                    dr[2] = "";

                    tem.Rows.Add(dr);

                    select_Brand_List.Merge(tem);
                }

                grvBMS.DataSource = null;
                grvBMS.AutoGenerateColumns = false;
                grvBMS.DataSource = select_Brand_List;
            }
            //radItm.Enabled = false;
            //radBMS.Enabled = false;
            //radBM.Enabled = false;
            //radMS.Enabled = false;
            //radB.Enabled = false;
        }

        private void btnLocAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvLocations.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLocNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvLocations.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvLocations.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnLocClear_Click(object sender, EventArgs e)
        {
            //select_LOC_List = new DataTable();
            //GridAllLocations.DataSource = null;
            //GridAllLocations.AutoGenerateColumns = false;
            //GridAllLocations.DataSource = select_LOC_List;

            grvLocations.DataSource = null;
            grvLocations.AutoGenerateColumns = false;           
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

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            //select_LOC_List = new DataTable();
            //GridAllLocations.DataSource = null;
            //GridAllLocations.AutoGenerateColumns = false;
            //GridAllLocations.DataSource = select_LOC_List;
            select_PC_List = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            grvItemList.DataSource = null;
            grvItemList.AutoGenerateColumns = false;
            select_ITEMS_List = null;

            radItm.Enabled = true;
            radBMS.Enabled = true;
            radBM.Enabled = true;
            radMS.Enabled = true;
            radB.Enabled = true;
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvItemList.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvItemList.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvItemList.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvItemList.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddLoc_Click(object sender, EventArgs e)
        {
            string com = ucLoactionSearch1.Company;
            string chanel = ucLoactionSearch1.Channel;
            string subChanel = ucLoactionSearch1.SubChannel;
            string area = ucLoactionSearch1.Area;
            string region = ucLoactionSearch1.Regien;
            string zone = ucLoactionSearch1.Zone;
            string pc = ucLoactionSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            //-----------------------------------------------------
            foreach (DataRow drr in dt.Rows)
            {
                string itmcd = drr["LOCATION"].ToString();
                // string descirption = drr["mi_shortdesc"].ToString();
                var _duplicate = from _dup in select_LOC_List.AsEnumerable()
                                 where _dup["LOCATION"].ToString() == itmcd
                                 select _dup;
                if (_duplicate.Count() != 0)
                {
                    MessageBox.Show("Location(s) already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //-----------------------------------------------------           

            select_LOC_List.Merge(dt);
            grvLocations.DataSource = null;
            grvLocations.AutoGenerateColumns = false;
            grvLocations.DataSource = select_LOC_List;

            this.btnLocAll_Click(sender, e);
          
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            string com = ucProfitCenterSearch1.Company;
            string chanel = ucProfitCenterSearch1.Channel;
            string subChanel = ucProfitCenterSearch1.SubChannel;
            string area = ucProfitCenterSearch1.Area;
            string region = ucProfitCenterSearch1.Regien;
            string zone = ucProfitCenterSearch1.Zone;
            string pc = ucProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            //-----------------------------------------------------
            foreach (DataRow drr in dt.Rows)
            {
                string itmcd = drr["PROFIT_CENTER"].ToString();
                // string descirption = drr["mi_shortdesc"].ToString();
                var _duplicate = from _dup in select_PC_List.AsEnumerable()
                                 where _dup["PROFIT_CENTER"].ToString() == itmcd
                                 select _dup;
                if (_duplicate.Count() != 0)
                {
                    MessageBox.Show("Profit center(s) already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;                  
                }
            }
            //-----------------------------------------------------
            select_PC_List.Merge(dt);
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;          
            grvProfCents.DataSource = select_PC_List;

            this.btnAllPc_Click(sender, e);
          
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (txtCom.Text.Trim() == "")
            {
                MessageBox.Show("Enter company", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //List<string> Selected_ITEMS_List = get_Selected_Items_List();
            Dictionary<string,string> Selected_ITEMS_Dic = get_Selected_Items_List();
            if (Selected_ITEMS_Dic.Count == 0)
            {
                MessageBox.Show("Please select item(s)!", "Restrict Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-----------------------------------------------
            Dictionary<string, string> Selected_PRICE_TP_Dic = get_Selected_PRICE_TP_List();
            if (Selected_PRICE_TP_Dic.Count == 0)
            {
                MessageBox.Show("Please select price type(s)!", "Price Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-----------------------------------------------

            List<MasterItemBlock> VIEW_BLOCK_LIST = new List<MasterItemBlock>();

            foreach (KeyValuePair<string, string> Price_pair in Selected_PRICE_TP_Dic)
            {
                //builder.AppendFormat("{0}:{1}.", pair.Key, pair.Value);     
                foreach (KeyValuePair<string, string> Item_pair in Selected_ITEMS_Dic)
                {                 
                    if (tabControl1.SelectedTab.Name == "tabPage1")
                    {
                        //MessageBox.Show("tabPage1");
                       
                        List<string> Selected_PC_List = get_Selected_PC_List();
                        if (Selected_PC_List.Count > 0)
                        {
                            //TODO: -LOOP PC LIST
                            foreach (string pc_code in Selected_PC_List)
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                itmBloc.Mbi_category_tp = "PC";
                                itmBloc.Mib_category = pc_code;
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = Item_pair.Key;
                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                                itmBloc.Mib_stus = chkActive.Checked;
                                //itmBloc.Mib_mod_by = chkActive.Checked==true?"Active":"No";
                                itmBloc.Mib_mod_by = Item_pair.Value; //FOR DISPLAY PURPOSE
                                itmBloc.Mib_cr_by = Price_pair.Value; //FOR DISPLAY PURPOSE

                                VIEW_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        else
                        {
                            MasterItemBlock itmBloc = new MasterItemBlock();
                            itmBloc.Mbi_category_tp = "N/A";
                            itmBloc.Mib_category= "N/A";
                            itmBloc.Mib_com = txtCom.Text.Trim();
                            itmBloc.Mib_cr_by= BaseCls.GlbUserID;
                            itmBloc.Mib_cr_dt= DateTime.Now;
                            itmBloc.Mib_itm = Item_pair.Key;
                            itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                            itmBloc.Mib_mod_dt =DateTime.Now;
                            itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                            itmBloc.Mib_stus = chkActive.Checked;
                            //itmBloc.Mib_mod_by = chkActive.Checked == true ? "Active" : "No";
                            itmBloc.Mib_mod_by = Item_pair.Value; //FOR DISPLAY PURPOSE
                            itmBloc.Mib_cr_by = Price_pair.Value; //FOR DISPLAY PURPOSE

                            VIEW_BLOCK_LIST.Add(itmBloc);
                        } 
                    }
                    if (tabControl1.SelectedTab.Name == "tabPage2")
                    {
                        //MessageBox.Show("tabPage2");
                        List<string> Selected_LOC_List = get_Selected_LOC_List();
                        if (Selected_LOC_List.Count > 0)
                        {
                            //TODO: -LOOP LOC LIST
                            foreach (string loc_code in Selected_LOC_List)
                            {
                                MasterItemBlock itmBloc = new MasterItemBlock();
                                itmBloc.Mbi_category_tp = "LOCATION";
                                itmBloc.Mib_category = loc_code;
                                itmBloc.Mib_com = txtCom.Text.Trim();
                                itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                                itmBloc.Mib_cr_dt = DateTime.Now;
                                itmBloc.Mib_itm = Item_pair.Key;
                                itmBloc.Mib_mod_by = Item_pair.Value; //FOR DISPLAY PURPOSE
                                itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                                itmBloc.Mib_mod_dt = DateTime.Now;
                                itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                                itmBloc.Mib_cr_by = Price_pair.Value; //FOR DISPLAY PURPOSE
                                itmBloc.Mib_stus = chkActive.Checked;
                                //itmBloc.Mib_mod_by = chkActive.Checked == true ? "Active" : "No";
                                VIEW_BLOCK_LIST.Add(itmBloc);
                            }
                        }
                        else
                        {
                            MasterItemBlock itmBloc = new MasterItemBlock();
                            itmBloc.Mbi_category_tp = "N/A";
                            itmBloc.Mib_category = "N/A";
                            itmBloc.Mib_com = txtCom.Text.Trim();
                            itmBloc.Mib_cr_by = BaseCls.GlbUserID;
                            itmBloc.Mib_cr_dt = DateTime.Now;
                            itmBloc.Mib_itm = Item_pair.Key;
                            itmBloc.Mib_mod_by = Item_pair.Value; //FOR DISPLAY PURPOSE
                            itmBloc.Mib_mod_by = BaseCls.GlbUserID; ;
                            itmBloc.Mib_mod_dt = DateTime.Now;
                            itmBloc.Mib_pr_tp = Convert.ToInt32(Price_pair.Key);
                            itmBloc.Mib_cr_by = Price_pair.Value; //FOR DISPLAY PURPOSE
                            itmBloc.Mib_stus = chkActive.Checked;
                            //itmBloc.Mib_mod_by = chkActive.Checked == true ? "Active" : "No";
                            VIEW_BLOCK_LIST.Add(itmBloc);
                        }
                    }
                }
            }     
       
            //--------------
            grvSaveList.DataSource = null;
            grvSaveList.AutoGenerateColumns = false;
            grvSaveList.DataSource = VIEW_BLOCK_LIST;

            foreach (DataGridViewRow dr in grvSaveList.Rows)
            {
                if (dr.Cells["isActive"].Value.ToString() == "True")
                {
                    dr.Cells["status"].Value = "Active";
                }
                else
                {
                    dr.Cells["status"].Value = "No";
                }
                
            }
            grvSaveList.EndEdit();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCom;
            _CommonSearch.ShowDialog();

            txtCom.Focus();

        }

        private void txtCom_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnItem_Click(null, null);
        }

        private void txtCom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnItem_Click(null, null);
            }
        }

        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnBrand_Click(null, null);
        }
        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnBrand_Click(null, null);
            }
        }  
        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnMainCat_Click(null, null);
        }
        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnMainCat_Click(null, null);
            }
        }

        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnCat_Click(null, null);
        }
        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnCat_Click(null, null);
            }
        }

        private void txtItemCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.button1_Click(null, null);
        }
               
        private void txtItemCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.button1_Click(null, null);
            }
        }

        private void btnBrLoc_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btnItmUpload_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtUploadItems.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Text = "";
                txtUploadItems.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }
            else
            {
                MessageBox.Show("Selected file does not match file format.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
                return;
            }


            conStr = String.Format(conStr, txtUploadItems.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            DataTable tem = new DataTable();
            tem.Columns.Add("code");
            tem.Columns.Add("descript");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(_dr[0].ToString())) { continue; }

                    MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                    if (_item == null)
                    {
                        MessageBox.Show("Invalid Item - " + _dr[0].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = _dr[0].ToString();
                    dr[1] = _item.Mi_shortdesc;
                    tem.Rows.Add(dr);
                }
            }
            grvItemList.AutoGenerateColumns = false;
            grvItemList.DataSource = tem;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = @"C:\";
            openFileDialog2.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 2;
            openFileDialog2.ShowDialog();
            txtUploadLoc.Text = openFileDialog2.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog3.InitialDirectory = @"C:\";
            openFileDialog3.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog3.FilterIndex = 2;
            openFileDialog3.ShowDialog();
            txtUploadPC.Text = openFileDialog3.FileName;
        }

        private void btnupPC_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtUploadPC.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadPC.Text = "";
                txtUploadPC.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadPC.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }
            else
            {
                MessageBox.Show("Selected file does not match file format.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
                return;
            }

            conStr = String.Format(conStr, txtUploadPC.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            DataTable tem = new DataTable();
            tem.Columns.Add("PROFIT_CENTER");
            tem.Columns.Add("PC_DESCRIPTION");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(_dr[0].ToString())) { continue; }

                    MasterProfitCenter _item = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, _dr[0].ToString());
                    if (_item == null)
                    {
                        MessageBox.Show("Invalid PC - " + _dr[0].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = _dr[0].ToString();
                    dr[1] = _item.Mpc_desc;
                    tem.Rows.Add(dr);
                }
            }
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = tem;
            select_PC_List = tem;
        }

        private void btnupLoc_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtUploadLoc.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadPC.Text = "";
                txtUploadPC.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadLoc.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }
            else
            {
                MessageBox.Show("Selected file does not match file format.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
                return;
            }


            conStr = String.Format(conStr, txtUploadLoc.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            DataTable tem = new DataTable();
            tem.Columns.Add("PROFIT_CENTER");
            tem.Columns.Add("PC_DESCRIPTION");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(_dr[0].ToString())) { continue; }

                    MasterLocation _item = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, _dr[0].ToString());
                    if (_item == null)
                    {
                        MessageBox.Show("Invalid Location - " + _dr[0].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    DataRow dr = tem.NewRow();
                    dr[0] = _dr[0].ToString();
                    dr[1] = _item.Ml_loc_desc;
                    tem.Rows.Add(dr);
                }
            }
            grvLocations.AutoGenerateColumns = false;
            grvLocations.DataSource = tem;
            select_PC_List = tem;
        }

        private void btn_srch_Chnl_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                _CommonSearch.IsRawData = true;
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChnl;
            _CommonSearch.ShowDialog();

            txtChnl.Focus();
        }

        private void imgSubChaSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                _CommonSearch.IsRawData = true;
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxSubChannel;
            _CommonSearch.ShowDialog();

            TextBoxSubChannel.Focus();
        }

        private void TextBoxChannel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxChannel.Text)) { TextBoxSubChannelDes.Clear(); return; }
            if (!CHNLSVC.General.CheckChannel(BaseCls.GlbUserComCode, TextBoxChannel.Text.Trim().ToUpper())) { MessageBox.Show("Please check the channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxChannel.Clear(); TextBoxSubChannelDes.Clear(); return; }
            DataTable _dt = CHNLSVC.Sales.GetChanelData(BaseCls.GlbUserComCode, TextBoxChannel.Text);
            TextBoxSubChannelDes.Text = _dt.Rows[0]["MSC_DESC"].ToString();
        }

        private void TextBoxSubChannel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text)) { TextBoxSubChannelDes.Clear(); return; }

            if (!CHNLSVC.General.CheckSubChannel(BaseCls.GlbUserComCode, TextBoxSubChannel.Text.Trim().ToUpper())) { MessageBox.Show("Please check the sub channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxSubChannel.Clear(); TextBoxSubChannelDes.Clear(); return; }
            DataTable _dt = CHNLSVC.General.getSubChannelDet(BaseCls.GlbUserComCode, TextBoxSubChannel.Text);
            TextBoxSubChannelDes.Text = _dt.Rows[0]["MSSC_DESC"].ToString();
        }

        private void txtChnl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtChnl.Text)) { txtChnlDesc.Clear(); return; }
            if (!CHNLSVC.General.CheckChannel(BaseCls.GlbUserComCode, txtChnl.Text.Trim().ToUpper())) { MessageBox.Show("Please check the channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); txtChnl.Clear(); txtChnlDesc.Clear(); return; }
            DataTable _dt = CHNLSVC.Sales.GetChanelData(BaseCls.GlbUserComCode, txtChnl.Text);
            txtChnlDesc.Text = _dt.Rows[0]["MSC_DESC"].ToString();
        }

        private void btn_clr_chnl_Click(object sender, EventArgs e)
        {
            grvChnl.DataSource = null;
            grvChnl.AutoGenerateColumns = false;   
        }

        private void btnclearsbchnl_Click(object sender, EventArgs e)
        {
            grvsbchnl.DataSource = null;
            grvsbchnl.AutoGenerateColumns = false;  
        }

        private void btnallsbchnl_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvsbchnl.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvsbchnl.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_all_chnl_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvChnl.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvChnl.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_non_chnl_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvChnl.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvChnl.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnnonsbchnl_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvsbchnl.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvsbchnl.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_add_chnl_Click(object sender, EventArgs e)
        {
            DataTable tem = new DataTable();
            tem.Columns.Add("CHANNEL");
            tem.Columns.Add("CHNL_DESCRIPTION");

                            string itmcd = txtChnl.Text;
                var _duplicate = from _dup in select_Chnl_List.AsEnumerable()
                                 where _dup["CHANNEL"].ToString() == itmcd
                                 select _dup;
                if (_duplicate.Count() != 0)
                {
                    MessageBox.Show("Channel already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            
            //-----------------------------------------------------           

                DataRow dr = tem.NewRow();
                dr[0] = txtChnl.Text;
                dr[1] = txtChnlDesc.Text;
                tem.Rows.Add(dr);

                select_Chnl_List.Merge(tem);
            grvChnl.DataSource = null;
            grvChnl.AutoGenerateColumns = false;
            grvChnl.DataSource = select_Chnl_List;
        }

        private void btnaddsbchnl_Click(object sender, EventArgs e)
        {
            DataTable tem = new DataTable();
            tem.Columns.Add("SUBCHANNEL");
            tem.Columns.Add("SCHNL_DESCRIPTION");

            string itmcd = TextBoxSubChannel.Text;
            var _duplicate = from _dup in select_SbChnl_List.AsEnumerable()
                             where _dup["SUBCHANNEL"].ToString() == itmcd
                             select _dup;
            if (_duplicate.Count() != 0)
            {
                MessageBox.Show("Sub Channel already added.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //-----------------------------------------------------           

            DataRow dr = tem.NewRow();
            dr[0] = TextBoxSubChannel.Text;
            dr[1] = TextBoxSubChannelDes.Text;
            tem.Rows.Add(dr);

            select_SbChnl_List.Merge(tem);
            grvsbchnl.DataSource = null;
            grvsbchnl.AutoGenerateColumns = false;
            grvsbchnl.DataSource = select_SbChnl_List;
        }

        private void radBMS_CheckedChanged(object sender, EventArgs e)
        {
            if (radItm.Checked==true)
            {
                grvItemList.Visible = true;
                grvBMS.Visible = false;
            }
            else
            {
                grvItemList.Visible = false;
                grvBMS.Visible = true;
            }
        }

        private void radBM_CheckedChanged(object sender, EventArgs e)
        {
            if (radItm.Checked == true)
            {
                grvItemList.Visible = true;
                grvBMS.Visible = false;
            }
            else
            {
                grvItemList.Visible = false;
                grvBMS.Visible = true;
            }
        }

        private void radItm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void imgChaSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            _CommonSearch.IsRawData = true;
            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxChannel;
            _CommonSearch.ShowDialog();

            TextBoxChannel.Focus();
        }

        private void radMS_CheckedChanged(object sender, EventArgs e)
        {
            if (radItm.Checked == true)
            {
                grvItemList.Visible = true;
                grvBMS.Visible = false;
            }
            else
            {
                grvItemList.Visible = false;
                grvBMS.Visible = true;
            }
        }

        private void radB_CheckedChanged(object sender, EventArgs e)
        {
            if (radItm.Checked == true)
            {
                grvItemList.Visible = true;
                grvBMS.Visible = false;
            }
            else
            {
                grvItemList.Visible = false;
                grvBMS.Visible = true;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void chkpricetype_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpricetype.Checked == true)
            {
                for (int i = 0; i < grvPriceTp.RowCount; i++)
                {
                    grvPriceTp[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grvPriceTp.RowCount; i++)
                {
                    grvPriceTp[0, i].Value = false;
                }
            }
        }

        private void chkselectdet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkselectdet.Checked == true)
            {
                for (int i = 0; i < grvSaveList.RowCount; i++)
                {
                    grvSaveList[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grvSaveList.RowCount; i++)
                {
                    grvSaveList[0, i].Value = false;
                }
            }
        }

       

            
    }
}
