using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.WindowsERPClient;
using FF.WindowsERPClient.Enquiries;
using FF.WindowsERPClient.Sales;
using FF.WindowsERPClient.Reports;
using System.Threading;
using FF.BusinessObjects;
using FF.WindowsERPClient.MDINotification;
using FF.WindowsERPClient.Barcode;

namespace FF.WindowsERPClient
{
    public partial class MainMenu : Form
    {
        Base _base = new Base();
        DataTable dt;
        private int childFormNumber = 0;

        #region Popup Alert Engine
        Thread _threadAlert = null;
        private bool isHaveAlert;
        private string AlertMsg;
        private bool isPnlOut;
        private string AlertDocument = string.Empty;
        private bool isAlertUser = false;
        Int32 count = 0;
        int _posY = -17;
        int _posX;

        private Boolean FormOpenAlready(string formName)
        {
            //If a form is opened already in this application, this method returns true.
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == formName)
                {
                    //MessageBox.Show("Form already opened!", "Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.WindowState = FormWindowState.Normal;
                    f.Activate();
                    return true;
                }
            }

            CloseNotification();
            return false;
        }
        private void check_unread_messages()
        {
            try
            {

                if (isHaveAlert)
                {
                    // timAlert.Enabled = false;
                }
                else
                {
                    timAlert_Tick(null, null);
                    // timAlert.Enabled = true;
                }
            }
            catch
            {
                _base.CHNLSVC.CloseChannel();
            }
        }
        private void ThreadingAlert()
        {
            try
            {
                timAlert_Tick(null, null);
            }
            catch
            {
                _base.CHNLSVC.CloseChannel();
            }

        }
        private void timAlert_Tick(object sender, System.EventArgs e)
        {
            try
            {
                _threadAlert = new System.Threading.Thread(new System.Threading.ThreadStart(NewMethod));
                _threadAlert.IsBackground = true;
                _threadAlert.Start();
            }
            catch
            {
                pnlAlert.Location = new Point(_posX, _posY);
                _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                //GC.Collect();
            }
        }

        private void UpdatePanel()
        {
            if (this.pnlAlert.InvokeRequired)
            {

                this.pnlAlert.Invoke(new MethodInvoker(UpdatePanel));
            }
            else
            {
                lblAlertMsg.Text = AlertMsg;
                pnlAlert.Location = new Point(_posX, 25);
            }
        }

        private void NewMethod()
        {
            while (!isHaveAlert)
            {
                try
                {
                    //Label.CheckForIllegalCrossThreadCalls = false;
                    DataTable _table = _base.CHNLSVC.Security.GetUnReadMessages(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf);
                    if (_table != null)
                        if (_table.Rows.Count > 0)
                        {
                            AlertMsg = string.Empty;
                            var val = _table.AsEnumerable().Select(x => x.Field<string>("MESSAGE")).ToList();
                            var _doc = _table.AsEnumerable().Select(x => x.Field<string>("DOC_NO")).ToList();
                            AlertMsg = Convert.ToString(val[0]);
                            AlertDocument = Convert.ToString(_doc[0]);
                            if (!string.IsNullOrEmpty(AlertMsg)) isHaveAlert = true; else isHaveAlert = false;
                        }
                        else
                            isHaveAlert = false;

                    _base.CHNLSVC.CloseAllChannels();

                    if (isHaveAlert)
                    {
                        //lblAlertMsg.Text = AlertMsg;
                        // timAlert.Enabled = false;
                        isPnlOut = false;
                        //pnlAlert.Location = new Point(_posX, 25);
                        //timPnlShift.Enabled = true;
                        UpdatePanel();
                    }
                    else
                    {
                        if (isPnlOut == false) { lblAlertMsg.Text = string.Empty; }
                        else
                        {
                            isPnlOut = true;
                            // timAlert.Enabled = true;
                            // timPnlShift.Enabled = true;
                            lblAlertMsg.Text = string.Empty;
                        }
                        Thread.Sleep(20000);
                    }
                }
                catch
                {
                    pnlAlert.Location = new Point(_posX, _posY);
                    _base.CHNLSVC.CloseChannel();
                }
                finally
                {
                    //GC.Collect();
                }
            }
        }
        private void btnAlertClose_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                int _effect = _base.CHNLSVC.Security.UpdateViewedMessage(AlertDocument);
                pnlAlert.Location = new Point(_posX, _posY);
                isPnlOut = true;
                isHaveAlert = false;
                pnlMsg.Visible = false;
                timAlert_Tick(null, null);
                //timPnlShift.Enabled = true;
                //timAlert.Enabled = true;
                //_base.CHNLSVC.CloseAllChannels();
            }
            catch
            {
                pnlAlert.Location = new Point(_posX, _posY);
                _base.CHNLSVC.CloseChannel();
            }

        }
        public bool IsAlertUsers()
        {
            bool functionReturnValue = false;
            try
            {
                DataTable _isUser = _base.CHNLSVC.Security.GetUserMessageType(BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf);
                if (_isUser != null)
                    if (_isUser.Rows.Count > 0)
                    {
                        functionReturnValue = true;
                    }
                    else
                    {
                        functionReturnValue = false;
                    }
                _base.CHNLSVC.CloseAllChannels();
            }
            catch
            {
                pnlAlert.Location = new Point(_posX, _posY);
                _base.CHNLSVC.CloseChannel();
            }
            return functionReturnValue;

        }
        private void lblAlertMsg_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                btnAlertClose_Click(null, null);
            }
            catch
            {
                _base.CHNLSVC.CloseChannel();
            }
        }
        private void timPnlShift_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isPnlOut == true)
                {
                    pnlAlert.Location = new Point(_posX, count);
                    if (count <= _posY)
                    {
                        count = _posY;
                        timPnlShift.Enabled = false;
                        isPnlOut = false;
                        pnlAlert.Location = new Point(_posX, _posY);
                    }
                    count -= 2;
                }
                else
                {
                    if (count >= 25)
                    {
                        count = 25;
                    }

                    pnlAlert.Location = new Point(_posX, count);
                    if (count >= 25)
                    {
                        count = 25;
                        timPnlShift.Enabled = false;
                        isPnlOut = true;
                    }
                    count += 2;
                }
            }
            catch
            {
                _base.CHNLSVC.CloseChannel();
            }
        }
        private void btnAlertMore_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlMsg.Visible) { pnlMsg.Visible = false; return; }
                pnlMsg.Visible = true;
                BindingSource _source = new BindingSource();
                DataTable _tbl = _base.CHNLSVC.General.GetUnReadMessages(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf);
                _source.DataSource = _tbl;
                gvMsg.DataSource = _tbl;
                _base.CHNLSVC.CloseAllChannels();
            }
            catch
            {
                _base.CHNLSVC.CloseChannel();
            }
        }
        #endregion

        #region --- MAIN MENU ----

        #region Default Menu Items
        public MainMenu()
        {
            try
            {
                InitializeComponent();
                tsslLoginUser.Text = BaseCls.GlbUserID + " - " + BaseCls.GlbUserName;
                tsslDept.Text = BaseCls.GlbUserDeptName;
                tsslCompany.Text = BaseCls.GlbUserComCode;
                tsslLoc.Text = BaseCls.GlbUserDefLoca;
                tsslPC.Text = BaseCls.GlbUserDefProf;
                tsslIP.Text = BaseCls.GlbUserIP;
                tsslLogonat.Text = "Logon on - " + _base.CHNLSVC.Security.GetServerDateTime().ToString();
                tsslVersion.Text = "Version - " + BaseCls.GlbVersionNo;
                if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "1")
                {
                    lblTestVersion.Visible = true;
                    lblTestVersion.Text = "*** Test Version - I ***";
                    lblTestVersion.ForeColor = Color.Crimson;
                }
                if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "2")
                {
                    lblTestVersion.Visible = true;
                    lblTestVersion.Text = "*** Test Version - Parallel ***";
                    lblTestVersion.ForeColor = Color.MediumVioletRed;
                }
                if (System.Configuration.ConfigurationManager.AppSettings["SystemType"].ToString() == "3")
                {
                    lblTestVersion.Visible = true;
                    lblTestVersion.Text = "- Version - II -";
                    lblTestVersion.ForeColor = Color.BlueViolet;
                }


                if (BaseCls.GlbUserID != "ADMIN")
                {
                    //Code Add by Chamal De Silva 24-01-2013 | Appling user permission menus
                    dt = _base.CHNLSVC.Security.GetUserSystemMenus(BaseCls.GlbUserID, BaseCls.GlbUserComCode);
                    //dt.Merge(dt); User Specify menus
                    //if (dt.Rows.Count > 0) 
                    menuActive(mnuMain);
                    dt = null;
                }
                //kapila 25/1/2017
                HpSystemParameters _getSystemParameter = new HpSystemParameters();
                _getSystemParameter = _base.CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "DSCNITM", DateTime.Now.Date);

                if (_getSystemParameter.Hsy_cd != null)
                    BaseCls.GlbDiscontItemDays =Convert.ToInt32( _getSystemParameter.Hsy_val);

                _base.CHNLSVC.CloseAllChannels();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                _base.CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Load User Privilage Menus
        private void menuActive(MenuStrip menus)
        {
            foreach (ToolStripMenuItem menu in menus.Items)
            {
                //Console.WriteLine(menu.Name.ToString());
                if (menu.Name.ToString() == "windowsMenu") continue;
                if (menu.Name.ToString() == "helpMenu") continue;

                menu.Enabled = false;
                if (checkMenu(menu.Name.ToString()) != "F")
                {
                    menu.Enabled = true;
                    activateItems(menu);
                }
                activateItems(menu);
            }
        }

        private string checkMenu(string _menuItem)
        {
            string _name = "F";
            var _exist = dt.AsEnumerable().Where(x => x.Field<string>("SSM_NAME") == _menuItem).Select(y => y.Field<string>("SSM_DISP_NAME")).ToList();
            if (_exist != null) if (_exist.Count > 0) _name = Convert.ToString(_exist[0]);
            return _name;
        }

        private void activateItems(ToolStripMenuItem item)
        {
            for (int i = 0; i < item.DropDown.Items.Count; i++)
            {
                ToolStripItem subItem = item.DropDown.Items[i];
                //Console.WriteLine(subItem.Name.ToString());
                subItem.Enabled = false;
                if (checkMenu(subItem.Name.ToString()) != "F") subItem.Enabled = true;

                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem _subItem = subItem as ToolStripMenuItem;
                    if (_subItem == null) continue;
                    activateItems(_subItem);
                }

            }
        }
        #endregion

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.FormClosed += child_FormClosed;
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            GetNotificationDetails();
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ContactUs") == true) return;
            General.ContactUs _contactUs = new General.ContactUs();
            _contactUs.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _contactUs.FormClosed += child_FormClosed;
            _contactUs.MdiParent = this;
            _contactUs.Show();
        }
        #endregion

        #region System
        private void m_System_Security_User_Inventory_DirectIssuePerm_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DirectIssueFacility") == true) return;
            General.DirectIssueFacility _directIssueFacility = new General.DirectIssueFacility();
            _directIssueFacility.FormClosed += child_FormClosed;
            _directIssueFacility.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _directIssueFacility.MdiParent = this;
            _directIssueFacility.Show();
        }
        #endregion

        #region Master Data Setup

        #region Organization
        private void m_MST_Orga_OrgaDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("OrganizationDefinition") == true) return;
            General.OrganizationDefinition _orgDefinition = new General.OrganizationDefinition();
            _orgDefinition.FormClosed += child_FormClosed;
            _orgDefinition.MdiParent = this;
            _orgDefinition.Show();
        }

        private void m_MST_Orga_ProCostDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ProfitCenterMasterDef") == true) return;
            General.ProfitCenterMasterDef _proCentDef = new General.ProfitCenterMasterDef();
            _proCentDef.FormClosed += child_FormClosed;
            _proCentDef.MdiParent = this;
            _proCentDef.Show();
        }
        private void m_MST_Orga_Town_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("TownMasterDef") == true) return;
            General.TownMaster _townMaster = new General.TownMaster();
            _townMaster.FormClosed += child_FormClosed;
            _townMaster.MdiParent = this;
            _townMaster.Show();
        }
        #endregion

        #region HP Master Data Setup
        private void m_MST_HP_GracePeriodDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("GracePeriodDefinition") == true) return;
            HP.GracePeriodDefinition _gracePeriodDefinition = new HP.GracePeriodDefinition();
            _gracePeriodDefinition.FormClosed += child_FormClosed;
            _gracePeriodDefinition.MdiParent = this;
            _gracePeriodDefinition.Show();
        }
        private void m_MST_HP_AccCreRest_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AccountCreationRestriction") == true) return;
            HP.AccountCreationRestriction _accRestric = new HP.AccountCreationRestriction();
            _accRestric.FormClosed += child_FormClosed;
            _accRestric.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _accRestric.MdiParent = this;
            _accRestric.Show();
        }
        private void m_MST_HP_SchCre_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SchemeCreation") == true) return;
            HP.SchemeCreation _schemeCreation = new HP.SchemeCreation();
            _schemeCreation.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _schemeCreation.FormClosed += child_FormClosed;
            _schemeCreation.MdiParent = this;
            _schemeCreation.Show();
        }
        private void m_MST_HP_CCDefinition_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CashConversionDefinition") == true) return;
            HP.CashConversionDefinition _ccDef = new HP.CashConversionDefinition();
            _ccDef.FormClosed += child_FormClosed;
            _ccDef.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ccDef.MdiParent = this;
            _ccDef.Show();
        }
        private void m_MST_HP_HPInsuDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpInsuDefinition") == true) return;
            HP.HpInsuDefinition _HpInsuDefinition = new HP.HpInsuDefinition();
            _HpInsuDefinition.FormClosed += child_FormClosed;
            _HpInsuDefinition.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _HpInsuDefinition.MdiParent = this;
            _HpInsuDefinition.Show();
        }
        private void m_MST_HP_ECDDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("EcdDefinition") == true) return;
            HP.EcdDefinition _ecdDef = new HP.EcdDefinition();
            _ecdDef.FormClosed += child_FormClosed;
            _ecdDef.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ecdDef.MdiParent = this;
            _ecdDef.Show();
        }
        private void m_MST_HP_ECDVou_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("EcdVoucher") == true) return;
            HP.EcdVoucher _ecdVou = new HP.EcdVoucher();
            _ecdVou.FormClosed += child_FormClosed;
            _ecdVou.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ecdVou.MdiParent = this;
            _ecdVou.Show();
        }
        private void m_MST_HP_CollectionBonusVchrUpload_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CollectionBonusVoucherUpload") == true) return;
            Finance.CollectionBonusVoucherUpload _bonus = new Finance.CollectionBonusVoucherUpload();
            _bonus.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _bonus.FormClosed += child_FormClosed;
            _bonus.MdiParent = this;
            _bonus.Show();
        }
        private void m_MST_HP_PromotorCommDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("IntroCommDef") == true) return;
            General.IntroCommDef _intrComm = new General.IntroCommDef();
            _intrComm.FormClosed += child_FormClosed;
            _intrComm.MdiParent = this;
            _intrComm.Show();
        }
        #endregion

        #region Sales Master Data Setup
        private void m_MST_Sales_SalesCommissionDefinition_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CommissionDefinition") == true) return;
            General.CommissionDefinition _commissionDefinition = new General.CommissionDefinition();
            _commissionDefinition.FormClosed += child_FormClosed;
            _commissionDefinition.MdiParent = this;
            _commissionDefinition.Show();
        }

        private void m_MST_Sales_LoyaltyDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("LoyaltyDefinitions") == true) return;
            Sales.LoyaltyDefinitions _loyaltyDefinition = new Sales.LoyaltyDefinitions();
            _loyaltyDefinition.FormClosed += child_FormClosed;
            _loyaltyDefinition.MdiParent = this;
            _loyaltyDefinition.Show();
        }

        private void m_MST_Sales_DiscountDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CashDiscountDefinition") == true) return;
            General.CashDiscountDefinition _cash = new General.CashDiscountDefinition();
            _cash.FormClosed += child_FormClosed;
            _cash.MdiParent = this;
            _cash.Show();
        }

        private void m_MST_Sales_EliteCommDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("EliteCommissionDefinition") == true) return;
            General.EliteCommissionDefinition _eliteDef = new General.EliteCommissionDefinition();
            _eliteDef.FormClosed += child_FormClosed;
            _eliteDef.MdiParent = this;
            _eliteDef.Show();
        }

        private void m_MST_Sales_SalesCommProc_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("EliteCommissionProcess") == true) return;
            General.EliteCommissionProcess _eliteCommProcess = new General.EliteCommissionProcess();
            _eliteCommProcess.FormClosed += child_FormClosed;
            _eliteCommProcess.MdiParent = this;
            _eliteCommProcess.Show();
        }

        private void m_MST_Sales_ProdBonDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ProductBonusDefinition") == true) return;
            General.ProductBonusDefinition _prodBonsDef = new General.ProductBonusDefinition();
            _prodBonsDef.FormClosed += child_FormClosed;
            _prodBonsDef.MdiParent = this;
            _prodBonsDef.Show();
        }

        private void m_MST_Sales_ProdBonProc_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ProductBonusProcess") == true) return;
            General.ProductBonusProcess _prodBonsProcess = new General.ProductBonusProcess();
            _prodBonsProcess.FormClosed += child_FormClosed;
            _prodBonsProcess.MdiParent = this;
            _prodBonsProcess.Show();
        }

        private void m_MST_Sales_SalesPromo_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SalePromotor") == true) return;
            Sales.SalePromotor _SaleProm = new Sales.SalePromotor();
            _SaleProm.FormClosed += child_FormClosed;
            _SaleProm.MdiParent = this;
            _SaleProm.Show();
        }
        private void m_MST_Sales_QuoPara_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SystemParaMaster") == true) return;
            General.SystemParaMaster _SysPara = new General.SystemParaMaster();
            _SysPara.FormClosed += child_FormClosed;
            _SysPara.MdiParent = this;
            _SysPara.Show();
        }
        private void m_MST_Sales_IntroComm_Click(object sender, EventArgs e)
        {
 
        }
        private void m_MST_Sales_BrndMgrSetup_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("BrandMgrSetup") == true) return;
            General.BrandMgrSetup _brndMgr = new General.BrandMgrSetup();
            _brndMgr.FormClosed += child_FormClosed;
            _brndMgr.MdiParent = this;
            _brndMgr.Show();
        }
        #endregion

        #region Inventory Master Data Setup
        private void m_MST_Inventory_ItemRestDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ItemsRestrictionDefinition") == true) return;
            General.ItemsRestrictionDefinition _itmBlockDef = new General.ItemsRestrictionDefinition();
            _itmBlockDef.FormClosed += child_FormClosed;
            _itmBlockDef.MdiParent = this;
            _itmBlockDef.Show();
        }

        private void m_MST_Inventory_CompSetup_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ItemComponentSetup") == true) return;
            Inventory.ItemComponentSetup _itemComp = new Inventory.ItemComponentSetup();
            _itemComp.FormClosed += child_FormClosed;
            _itemComp.MdiParent = this;
            _itemComp.Show();
        }

        private void m_MST_Inventory_Itemmaster_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ItemProfile") == true) return;
            General.ItemProfile _item = new General.ItemProfile();
            _item.FormClosed += child_FormClosed;
            _item.MdiParent = this;
            _item.Show();

        }

        private void m_MST_Inventory_Itemparameters_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ItemCategorization") == true) return;
            General.ItemCategorization _itempara = new General.ItemCategorization();
            _itempara.FormClosed += child_FormClosed;
            _itempara.MdiParent = this;
            _itempara.Show();

        }
        private void m_MST_Inventory_TaxStructure_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ItemTaxStructure") == true) return;
            General.ItemTaxStructure _itemtax = new General.ItemTaxStructure();
            _itemtax.FormClosed += child_FormClosed;
            _itemtax.MdiParent = this;
            _itemtax.Show();

        }
        private void m_MST_Inventory_MRNRestrict_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Restrict_Def") == true) return;
            General.Restrict_Def _restrDef = new General.Restrict_Def();
            _restrDef.FormClosed += child_FormClosed;
            _restrDef.MdiParent = this;
            _restrDef.Show();
        }
        private void m_MST_Inventory_SubLoc_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Restrict_Def") == true) return;
            General.SubLocation _subLocs = new General.SubLocation();
            _subLocs.FormClosed += child_FormClosed;
            _subLocs.MdiParent = this;
            _subLocs.Show();
        }

        #endregion

        #region Service Master Data Setup
        private void m_MST_Service_AcSevChgDef_Click(object sender, EventArgs e)
        {

            if (FormOpenAlready("ACserviceChargesDefinition") == true) return;
            General.ACserviceChargesDefinition _acSevChgDef = new General.ACserviceChargesDefinition();
            _acSevChgDef.FormClosed += child_FormClosed;
            _acSevChgDef.MdiParent = this;
            _acSevChgDef.Show();
        }

        private void m_MST_Service_ServiceAgentDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceAgentDefinition") == true) return;
            General.ServiceAgentDefinition _serviceAgentDefinition = new General.ServiceAgentDefinition();
            _serviceAgentDefinition.FormClosed += child_FormClosed;
            _serviceAgentDefinition.MdiParent = this;
            _serviceAgentDefinition.Show();
        }

        private void m_MST_Service_AllocateItemCat_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AllocateItemCategory") == true) return;
            Finance.AllocateItemCategory _itemcat = new Finance.AllocateItemCategory();
            _itemcat.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _itemcat.FormClosed += child_FormClosed;
            _itemcat.MdiParent = this;
            _itemcat.Show();
        }

        private void m_MST_Service_ServicePara_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceParameters") == true) return;
            Services.ServiceParameters _serpara = new Services.ServiceParameters();
            _serpara.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _serpara.FormClosed += child_FormClosed;
            _serpara.MdiParent = this;
            _serpara.Show();
        }

        private void m_MST_Service_ServiceDefinition_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceDefinitions") == true) return;
            Services.ServiceDefinitions _serdifi = new Services.ServiceDefinitions();
            _serdifi.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _serdifi.FormClosed += child_FormClosed;
            _serdifi.MdiParent = this;
            _serdifi.Show();
        }

        private void m_MST_Service_SuppWarrClaimDeff_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SupWarrantyClaimDef") == true) return;
            Services.SupWarrantyClaimDef Sup_Ar = new Services.SupWarrantyClaimDef();
            Sup_Ar.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            Sup_Ar.FormClosed += child_FormClosed;
            Sup_Ar.MdiParent = this;
            Sup_Ar.Show();
        }

        #endregion

        #region Finance Master Data Setup
        private void m_MST_Finance_RemSumColCreation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RemSumHeading") == true) return;
            General.RemSumHeading _remsumHdr = new General.RemSumHeading();
            _remsumHdr.FormClosed += child_FormClosed;
            _remsumHdr.MdiParent = this;
            _remsumHdr.Show();
        }
        private void m_MST_Finance_VouExDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VoucherPrintingExpenseDefinition") == true) return;
            Finance.VoucherPrintingExpenseDefinition _VouExDef = new Finance.VoucherPrintingExpenseDefinition();
            _VouExDef.FormClosed += child_FormClosed;
            _VouExDef.MdiParent = this;
            _VouExDef.Show();
        }
        private void m_MST_Finance_SalesTarget_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SalesTarget") == true) return;
            General.SalesTarget _salesTarget = new General.SalesTarget();
            _salesTarget.FormClosed += child_FormClosed;
            _salesTarget.MdiParent = this;
            _salesTarget.Show();
        }
        private void m_MST_Finance_DepositBankPCWise_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DepositBankPCWiseDef") == true) return;
            Finance.DepositBankPCWiseDef _Deposit = new Finance.DepositBankPCWiseDef();
            _Deposit.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _Deposit.FormClosed += child_FormClosed;
            _Deposit.MdiParent = this;
            _Deposit.Show();
        }
        private void m_MST_Finance_MerchantIdDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("MerchantIdDefinition") == true) return;
            Finance.MerchantIdDefinition _bonus = new Finance.MerchantIdDefinition();
            _bonus.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _bonus.FormClosed += child_FormClosed;
            _bonus.MdiParent = this;
            _bonus.Show();
        }
        private void m_MST_Finance_ChqBankIss_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ChequeBankIssue") == true) return;
            Finance.ChequeBankIssue _bonus = new Finance.ChequeBankIssue();
            _bonus.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _bonus.FormClosed += child_FormClosed;
            _bonus.MdiParent = this;
            _bonus.Show();
        }
        private void m_MST_Finance_ExpLimAlloc_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExpenseLimitAlloc") == true) return;
            General.ExpenseLimitAlloc _ExpLimitAlloc = new General.ExpenseLimitAlloc();
            _ExpLimitAlloc.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ExpLimitAlloc.FormClosed += child_FormClosed;
            _ExpLimitAlloc.MdiParent = this;
            _ExpLimitAlloc.Show();
        }
        
        #endregion

        #region Operation Data Setup
        private void m_MST_OperationData_WeekDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("WeekDefinition") == true) return;
            General.WeekDefinition _weekDef = new General.WeekDefinition();
            _weekDef.FormClosed += child_FormClosed;
            _weekDef.MdiParent = this;
            _weekDef.Show();
        }
        private void m_MST_OperationData_CustCreation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CustomerCreation") == true) return;
            HP.CustomerCreation _custCreation = new HP.CustomerCreation();
            _custCreation.FormClosed += child_FormClosed;
            _custCreation.MdiParent = this;
            _custCreation.Show();
        }
        private void m_MST_OperationData_SupCreation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SupplierCreation") == true) return;
            General.SupplierCreation _supCreation = new General.SupplierCreation();
            _supCreation.FormClosed += child_FormClosed;
            _supCreation.MdiParent = this;
            _supCreation.Show();
        }
        private void m_MST_OperationData_VehiRegDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehicalRegistrationDefinition") == true) return;
            General.VehicalRegistrationDefinition _vehiRegDef = new General.VehicalRegistrationDefinition();
            _vehiRegDef.FormClosed += child_FormClosed;
            _vehiRegDef.MdiParent = this;
            _vehiRegDef.Show();
        }
        private void m_MST_OperationData_VehiInsuDef_Click(object sender, EventArgs e)
        {
            //if (FormOpenAlready("VehicleInsuDefinition") == true) return;
            //General.VehicleInsuDefinition _vehiInsuDef = new General.VehicleInsuDefinition();
            //_vehiInsuDef.MdiParent = this;
            //_vehiInsuDef.Show();

            //if (lblTestVersion.Visible)
            //{
            if (FormOpenAlready("VehicleInsuDefinitionNew") == true) return;
            General.VehicleInsuDefinitionNew _vehiInsuDefNew = new General.VehicleInsuDefinitionNew();
            _vehiInsuDefNew.FormClosed += child_FormClosed;
            _vehiInsuDefNew.MdiParent = this;
            _vehiInsuDefNew.Show();
            //}
        }
        private void m_MST_OperationData_BrndNgrAlloc_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("BrandMgrSetup") == true) return;
            General.BrandMgrSetup _brndMgrAlloc = new General.BrandMgrSetup();
            _brndMgrAlloc.FormClosed += child_FormClosed;
            _brndMgrAlloc.MdiParent = this;
            _brndMgrAlloc.Show();
        }
        private void m_MST_OperationData_myabans_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("MyAbansEntry") == true) return;
            General.MyAbansEntry _myabans = new General.MyAbansEntry();
            _myabans.FormClosed += child_FormClosed;
            _myabans.MdiParent = this;
            _myabans.Show();
        }
        private void m_MST_OperationData_SupCreation_Click_1(object sender, EventArgs e)
        {
            if (FormOpenAlready("SupplierCreation") == true) return;
            General.SupplierCreation _supCre = new General.SupplierCreation();
            _supCre.FormClosed += child_FormClosed;
            _supCre.MdiParent = this;
            _supCre.Show();
        }
        private void m_MST_OperationData_ExRateDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExchangeRateDefinition") == true) return;
            General.ExchangeRateDefinition _exRate = new General.ExchangeRateDefinition();
            _exRate.FormClosed += child_FormClosed;
            _exRate.MdiParent = this;
            _exRate.Show();
        }
        #endregion

        #endregion

        #region Enquiries
        #region Sales
        private void m_Enquiries_Sales_CustomerMonitoring_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CustomerMonitoring") == true) return;
            CustomerMonitoring _CustomerMonitoring = new CustomerMonitoring();
            _CustomerMonitoring.FormClosed += child_FormClosed;
            _CustomerMonitoring.MdiParent = this;
            _CustomerMonitoring.Show();
        }

        private void m_Enquiries_Sales_PriceEnquiry_Click(object sender, EventArgs e)
        {
            //if (FormOpenAlready("PriceDetail") == true) return;
            //Enquiries.Sales.PriceDetail _priceDetail = new Enquiries.Sales.PriceDetail();
            //_priceDetail.MdiParent = this;
            //_priceDetail.Show();


            if (FormOpenAlready("PriceEnquiry") == true) return;
            Enquiries.Sales.PriceEnquiry _priceDetail1 = new Enquiries.Sales.PriceEnquiry();
            _priceDetail1.FormClosed += child_FormClosed;
            _priceDetail1.MdiParent = this;
            _priceDetail1.Show();

        }

        private void m_Enquiries_Sales_InsuranceDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("InsuranceDefEnquiry") == true) return;
            Enquiries.Sales.InsuranceDefEnquiry _InsuranceDefEnquiry = new Enquiries.Sales.InsuranceDefEnquiry();
            _InsuranceDefEnquiry.FormClosed += child_FormClosed;
            _InsuranceDefEnquiry.MdiParent = this;
            _InsuranceDefEnquiry.Show();
        }


        #endregion

        #region Inventory
        private void m_Enquiries_Inventory_InventoryTracker_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("InventoryTracker") == true) return;
            Enquiries.Inventory.InventoryTracker _inventoryTracker = new Enquiries.Inventory.InventoryTracker();
            _inventoryTracker.FormClosed += child_FormClosed;
            _inventoryTracker.MdiParent = this;
            _inventoryTracker.Show();
        }

        private void m_Enquiries_Inventory_StockLedger_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("StockLedger") == true) return;
            Enquiries.Inventory.StockLedger _stockLedger = new Enquiries.Inventory.StockLedger();
            _stockLedger.FormClosed += child_FormClosed;
            _stockLedger.MdiParent = this;
            _stockLedger.Show();
        }

        private void m_Enquiries_Inventory_SerialTracker_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SerialTracker") == true) return;
            Enquiries.Inventory.SerialTracker _serialTracker = new Enquiries.Inventory.SerialTracker();
            _serialTracker.FormClosed += child_FormClosed;
            _serialTracker.MdiParent = this;
            _serialTracker.Show();
        }

        private void m_Enquiries_Inventory_MRNTracker_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("MRNProcessTracking") == true) return;
            General.MRNProcessTracking _mrnTrack = new General.MRNProcessTracking();
            _mrnTrack.FormClosed += child_FormClosed;
            _mrnTrack.MdiParent = this;
            _mrnTrack.Show();
        }

        private void m_Enquiries_Inventory_RCCTracker_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RccTracker") == true) return;
            Enquiries.RCC.RccTracker _paymodeInquiry = new Enquiries.RCC.RccTracker();
            _paymodeInquiry.FormClosed += child_FormClosed;
            _paymodeInquiry.MdiParent = this;
            _paymodeInquiry.Show();
        }
        private void m_Enquiries_Inventory_SubLocStock_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SubLocationStock") == true) return;
            Enquiries.Inventory.SubLocationStock _paymodeInquiry = new Enquiries.Inventory.SubLocationStock();
            _paymodeInquiry.FormClosed += child_FormClosed;
            _paymodeInquiry.MdiParent = this;
            _paymodeInquiry.Show();
        }
        #endregion

        #region Finance
        private void m_Enquiries_Finance_PayModeTracker_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("PaymodeInquiry") == true) return;
            Enquiries.Finance.PaymodeInquiry _paymodeInquiry = new Enquiries.Finance.PaymodeInquiry();
            _paymodeInquiry.FormClosed += child_FormClosed;
            _paymodeInquiry.MdiParent = this;
            _paymodeInquiry.Show();
        }
        private void m_Enquiries_Finance_IntPayVoucher_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VoucherEnquiry") == true) return;
            Finance.VoucherEnquiry _voucherEnquiry = new Finance.VoucherEnquiry();
            _voucherEnquiry.FormClosed += child_FormClosed;
            _voucherEnquiry.MdiParent = this;
            _voucherEnquiry.Show();
        }
        #endregion

        #region Service
        private void m_Enquiries_Service_JobEnquiry_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceJobEnquiry") == true) return;
            FF.WindowsERPClient.Enquiries.Service.ServiceJobEnquiry frm = new FF.WindowsERPClient.Enquiries.Service.ServiceJobEnquiry();
            frm.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            frm.FormClosed += child_FormClosed;
            frm.MdiParent = this;
            frm.Show();
        }
        private void m_Enquiries_Service_PendingJobs_Click(object sender, EventArgs e)
        {
     
        }
        #endregion

        #region Additional
        private void m_Enquiries_Additional_RegProcTrack_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RegistrationPayTracker") == true) return;
            Enquiries.Finance.RegistrationPayTracker _vehiDocTrack = new Enquiries.Finance.RegistrationPayTracker();
            _vehiDocTrack.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vehiDocTrack.FormClosed += child_FormClosed;
            _vehiDocTrack.MdiParent = this;
            _vehiDocTrack.Show();
        }

        private void m_Enquiries_Additional_ReqAppStatus_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RequestApprovalStatus") == true) return;
            Enquiries.Inventory.RequestApprovalStatus _reqAppStus = new Enquiries.Inventory.RequestApprovalStatus();
            _reqAppStus.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _reqAppStus.FormClosed += child_FormClosed;
            _reqAppStus.MdiParent = this;
            _reqAppStus.Show();

        }

        #endregion

        #endregion

        #region Hire Purchase
        private void m_Trans_HP_AccountCreation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AccountCreation") == true) return;
            HP.AccountCreation _accountCreation = new HP.AccountCreation();
            _accountCreation.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _accountCreation.FormClosed += child_FormClosed;
            _accountCreation.MdiParent = this;
            _accountCreation.Show();
        }
        private void m_Trans_HP_Collection_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpCollection") == true) return;
            HP.HpCollection _hpCollection = new HP.HpCollection();
            _hpCollection.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpCollection.FormClosed += child_FormClosed;
            _hpCollection.MdiParent = this;
            _hpCollection.Show();
        }
        private void m_Trans_HP_Revert_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpRevert") == true) return;
            HP.HpRevert _hpRevert = new HP.HpRevert();
            _hpRevert.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpRevert.FormClosed += child_FormClosed;
            _hpRevert.MdiParent = this;
            _hpRevert.Show();
        }
        private void m_Trans_HP_CashConversion_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AccountSettlement") == true) return;
            HP.AccountSettlement _cashConversion = new HP.AccountSettlement();
            _cashConversion.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _cashConversion.FormClosed += child_FormClosed;
            _cashConversion.MdiParent = this;
            _cashConversion.Show();
        }
        private void m_Trans_HP_HpExchange_Click(object sender, EventArgs e)
        {
            //add live to 01/06/2013
            if (FormOpenAlready("HpExchange") == true) return;
            HP.HpExchange _hpExchange = new HP.HpExchange();
            _hpExchange.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpExchange.FormClosed += child_FormClosed;
            _hpExchange.MdiParent = this;
            _hpExchange.Show();
        }
        private void m_Trans_HP_HpReversal_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HPReversal") == true) return;
            HP.HPReversal _hpRev = new HP.HPReversal();
            _hpRev.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpRev.FormClosed += child_FormClosed;
            _hpRev.MdiParent = this;
            _hpRev.Show();
        }
        private void m_Trans_HP_Adjustment_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HPAdjustment") == true) return;
            HP.HPAdjustment _hpAdjustment = new HP.HPAdjustment();
            _hpAdjustment.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpAdjustment.FormClosed += child_FormClosed;
            _hpAdjustment.MdiParent = this;
            _hpAdjustment.Show();
        }
        private void m_Trans_HP_RevertRelease_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpRevertRelease") == true) return;
            HP.HpRevertRelease _revertRelease = new HP.HpRevertRelease();
            _revertRelease.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _revertRelease.FormClosed += child_FormClosed;
            _revertRelease.MdiParent = this;
            _revertRelease.Show();
        }
        private void m_Trans_HP_GroupSalesCreation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("GroupSale") == true) return;
            HP.GroupSale _groupSale = new HP.GroupSale();
            _groupSale.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _groupSale.FormClosed += child_FormClosed;
            _groupSale.MdiParent = this;
            _groupSale.Show();
        }
        private void m_Trans_HP_AccountReschedule_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AccountReschedule") == true) return;
            HP.AccountReschedule _reschedule = new HP.AccountReschedule();
            _reschedule.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _reschedule.FormClosed += child_FormClosed;
            _reschedule.MdiParent = this;
            _reschedule.Show();
        }
        private void m_Trans_HP_AccDetUpdate_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AccountsDetailUpdates") == true) return;
            HP.AccountsDetailUpdates _accUpdt = new HP.AccountsDetailUpdates();
            _accUpdt.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _accUpdt.FormClosed += child_FormClosed;
            _accUpdt.MdiParent = this;
            _accUpdt.Show();
        }
        private void m_Trans_HP_Reminders_AccRem_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HPReminders") == true) return;
            HP.HPReminders _hpReminders = new HP.HPReminders();
            _hpReminders.FormClosed += child_FormClosed;
            _hpReminders.MdiParent = this;
            _hpReminders.Show();
        }
        private void m_Trans_HP_Reminders_SMSRem_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HPRemindersSMS") == true) return;
            HP.HPRemindersSMS _hpRemindersSMS = new HP.HPRemindersSMS();
            _hpRemindersSMS.FormClosed += child_FormClosed;
            _hpRemindersSMS.MdiParent = this;
            _hpRemindersSMS.Show();
        }
        private void m_Trans_HP_Reminders_ManualRem_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HPManualReminders") == true) return;
            HP.HPManualReminders _hpmanualReminder = new HP.HPManualReminders();
            _hpmanualReminder.FormClosed += child_FormClosed;
            _hpmanualReminder.MdiParent = this;
            _hpmanualReminder.Show();
        }
        private void m_Trans_HP_VehicleRelease_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehicleReleaseToHPAccHolder") == true) return;
            General.VehicleReleaseToHPAccHolder _vehiRelease = new General.VehicleReleaseToHPAccHolder();
            _vehiRelease.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vehiRelease.FormClosed += child_FormClosed;
            _vehiRelease.MdiParent = this;
            _vehiRelease.Show();
        }
        private void m_Trans_HP_ReceiptReversal_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpReceiptReversal") == true)
            {
                return;
            }
            HP.HpReceiptReversal _receiptReversal = new HP.HpReceiptReversal();
            _receiptReversal.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _receiptReversal.FormClosed += child_FormClosed;
            _receiptReversal.MdiParent = this;
            _receiptReversal.Show();
        }
        private void m_Trans_HP_RtnReqApp_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ReturnRequestApproval") == true) return;
            General.ReturnRequestApproval _rtnReqApp = new General.ReturnRequestApproval();
            _rtnReqApp.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _rtnReqApp.FormClosed += child_FormClosed;
            _rtnReqApp.MdiParent = this;
            _rtnReqApp.Show();
        }
        private void m_Trans_HP_AgrPrinting_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AgreementPrinting") == true) return;
            HP.AgreementPrinting _argPrint = new HP.AgreementPrinting();
            _argPrint.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _argPrint.FormClosed += child_FormClosed;
            _argPrint.MdiParent = this;
            _argPrint.Show();
        }
        private void m_Trans_HP_MonEnd_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ProcessAccountBalance") == true) return;
            HP.ProcessAccountBalance _hpMonEnd = new HP.ProcessAccountBalance();
            _hpMonEnd.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpMonEnd.FormClosed += child_FormClosed;
            _hpMonEnd.MdiParent = this;
            _hpMonEnd.Show();
        }
        private void m_Trans_HP_AgrTrck_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpAgreementTracker") == true) return;
            Enquiries.HP.HpAgreementTracker _agrTrack = new Enquiries.HP.HpAgreementTracker();
            _agrTrack.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _agrTrack.FormClosed += child_FormClosed;
            _agrTrack.MdiParent = this;
            _agrTrack.Show();
        }
        private void m_Trans_HP_AccTrim_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AccountTrim") == true) return;
            HP.AccountTrim _trim = new HP.AccountTrim();
            _trim.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _trim.FormClosed += child_FormClosed;
            _trim.MdiParent = this;
            _trim.Show();
        }
        private void m_Trans_HP_AccChk_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AgrementChecklist") == true) return;
            HP.AgrementChecklist _chk = new HP.AgrementChecklist();
            _chk.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _chk.FormClosed += child_FormClosed;
            _chk.MdiParent = this;
            _chk.Show();
        }
        private void m_Trans_HP_CusAck_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CustomerAcknowledgement") == true) return;
            HP.CustomerAcknowledgement _ack = new HP.CustomerAcknowledgement();
            _ack.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ack.FormClosed += child_FormClosed;
            _ack.MdiParent = this;
            _ack.Show();
        }
        private void m_Trans_HP_SetOffMngrIssuance_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ManagerIssueReceive") == true) return;
            HP.ManagerIssueReceive _mngrIssuReceive = new HP.ManagerIssueReceive();
            _mngrIssuReceive.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _mngrIssuReceive.FormClosed += child_FormClosed;
            _mngrIssuReceive.MdiParent = this;
            _mngrIssuReceive.Show();
        }
        private void m_Trans_HP_PersonalChqUpdate_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ManagerIssueUpdate") == true) return;
            HP.ManagerIssueUpdate _mngrIssuUpdate = new HP.ManagerIssueUpdate();
            _mngrIssuUpdate.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _mngrIssuUpdate.FormClosed += child_FormClosed;
            _mngrIssuUpdate.MdiParent = this;
            _mngrIssuUpdate.Show();
        }
        private void m_Trans_HP_AgreementTracker_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AgreementTracker") == true) return;
            HP.AgreementTracker _hpAgrTrckt = new HP.AgreementTracker();
            _hpAgrTrckt.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _hpAgrTrckt.FormClosed += child_FormClosed;
            _hpAgrTrckt.MdiParent = this;
            _hpAgrTrckt.Show();
        }
        #endregion

        #region Sales
        private void m_Transaction_Sales_SalesInvoice_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Invoice") == true) return;
            Sales.Invoice _invoice = new Sales.Invoice();
            _invoice.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _invoice.FormClosed += child_FormClosed;
            _invoice.MdiParent = this;
            _invoice.Show();
        }
        private void m_Trans_Sales_SalesInvoiceDFS_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DutyFreeInvoice") == true) return;
            Sales.DutyFreeInvoice _dfInvoice = new Sales.DutyFreeInvoice();
            _dfInvoice.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _dfInvoice.FormClosed += child_FormClosed;
            _dfInvoice.MdiParent = this;
            _dfInvoice.Show();
        }
        private void m_Trans_Sales_Reversal_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SalesReversal") == true) return;
            Sales.SalesReversal _salesRev = new Sales.SalesReversal();
            _salesRev.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _salesRev.FormClosed += child_FormClosed;
            _salesRev.MdiParent = this;
            _salesRev.Show();
        }
        private void m_Trans_Sales_LoyaltyMem_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("LoyaltyMembership") == true) return;
            General.LoyaltyMembership _loyMem = new General.LoyaltyMembership();
            _loyMem.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _loyMem.FormClosed += child_FormClosed;
            _loyMem.MdiParent = this;
            _loyMem.Show();
        }
        private void m_Trans_Sales_CustQuot_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormOpenAlready("CustomerQuotation") == true) return;
                Sales.CustomerQuotation _CusQuo = new Sales.CustomerQuotation();
                _CusQuo.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
                _CusQuo.FormClosed += child_FormClosed;
                _CusQuo.MdiParent = this;
                _CusQuo.Show();
            }
            catch (Exception ex)
            {
                this.MainMenu_Load(null, null);
                MessageBox.Show("You cannot perform this action. " + ex.Message, "System Informetion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            //if (FormOpenAlready("CustomerQuotation") == true) return;
            //Sales.CustomerQuotation _CusQuo = new Sales.CustomerQuotation();
            //_CusQuo.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            //_CusQuo.FormClosed += child_FormClosed;
            //_CusQuo.MdiParent = this;
            //_CusQuo.Show();
        }
        private void m_Trans_Sales_CusCreReq_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CreditCustomerRequest") == true) return;
            General.CreditCustomerRequest _credCustReq = new General.CreditCustomerRequest();
            _credCustReq.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _credCustReq.FormClosed += child_FormClosed;
            _credCustReq.MdiParent = this;
            _credCustReq.Show();
        }
        private void m_Trans_Sales_ClrSalesInvoice_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ClrSaleInvoice") == true) return;
            Sales.ClrSaleInvoice _clrSaleInvoice = new Sales.ClrSaleInvoice();
            _clrSaleInvoice.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _clrSaleInvoice.FormClosed += child_FormClosed;
            _clrSaleInvoice.MdiParent = this;
            _clrSaleInvoice.Show();
        }
        private void m_Trans_Sales_Invoice_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("BroadcastInvoice") == true) return;
            Sales.BroadcastInvoice _brdInvoice = new Sales.BroadcastInvoice();
            _brdInvoice.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _brdInvoice.FormClosed += child_FormClosed;
            _brdInvoice.MdiParent = this;
            _brdInvoice.Show();
        }
        private void m_Trans_Sales_WesternUnion_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("WesternUnion") == true) return;
            Sales.WesternUnion _westernUnion = new Sales.WesternUnion();
            _westernUnion.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _westernUnion.FormClosed += child_FormClosed;
            _westernUnion.MdiParent = this;
            _westernUnion.Show();
        }
        private void m_Trans_Sales_POS_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("POSInvoice") == true) return;
            Sales.POSInvoice _pos = new Sales.POSInvoice();
            _pos.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _pos.FormClosed += child_FormClosed;
            _pos.MdiParent = this;
            _pos.Show();
        }
        private void m_Trans_Sales_POSTouch_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("POSInvoiceTouch") == true) return;
            Sales.POSInvoiceTouch _posTouch = new Sales.POSInvoiceTouch();
            _posTouch.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _posTouch.FormClosed += child_FormClosed;
            _posTouch.MdiParent = this;
            _posTouch.Show();
        }
        private void m_Trans_Sales_SO_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SalesOrderRequest") == true) return;
            Inventory.SalesOrderRequest _soReq = new  Inventory.SalesOrderRequest();
            _soReq.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _soReq.FormClosed += child_FormClosed;
            _soReq.MdiParent = this;
            _soReq.Show();
        }
        private void m_Trans_Sales_Debit_Note_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DebitNote") == true) return;
            Sales.DebitNote _debitNote = new Sales.DebitNote();
            _debitNote.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _debitNote.FormClosed += child_FormClosed;
            _debitNote.MdiParent = this;
            _debitNote.Show();
        }
        #endregion

        #region Inventory
        private void m_Trans_FA_MRN_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("FAMaterialRequisition") == true) return;
            Inventory.FAMaterialRequisition _mrn = new Inventory.FAMaterialRequisition();
            _mrn.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _mrn.FormClosed += child_FormClosed;
            _mrn.MdiParent = this;
            _mrn.Show();
        }
        private void m_Trans_FA_StockTransOut_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("FAInterCompanyOutWardEntry") == true) return;
            Inventory.FAInterCompanyOutWardEntry _FAOut = new Inventory.FAInterCompanyOutWardEntry();
            _FAOut.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _FAOut.FormClosed += child_FormClosed;
            _FAOut.MdiParent = this;
            _FAOut.Show();
        }
        private void m_Transaction_Inventory_CustomerDeliveryOrder_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DeliveryOrderCustomer") == true) return;
            Inventory.DeliveryOrderCustomer _deliveryOrderCustomer = new Inventory.DeliveryOrderCustomer();
            _deliveryOrderCustomer.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _deliveryOrderCustomer.FormClosed += child_FormClosed;
            _deliveryOrderCustomer.MdiParent = this;
            _deliveryOrderCustomer.Show();
        }
        private void m_Trans_Inventory_MRN_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("MaterialRequisition") == true) return;
            Inventory.MaterialRequisition _mrn = new Inventory.MaterialRequisition();
            _mrn.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _mrn.FormClosed += child_FormClosed;
            _mrn.MdiParent = this;
            _mrn.Show();
        }
        private void m_Transaction_Inventory_StockTransferOutward_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("InterCompanyOutWardEntry") == true) return;
            Inventory.InterCompanyOutWardEntry _out = new Inventory.InterCompanyOutWardEntry();
            _out.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _out.FormClosed += child_FormClosed;
            _out.MdiParent = this;
            _out.Show();
        }
        private void m_Transaction_Inventory_GRAN_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("GRAN") == true) return;
            Inventory.GRAN _gran = new Inventory.GRAN();
            _gran.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _gran.FormClosed += child_FormClosed;
            _gran.MdiParent = this;
            _gran.Show();
        }
        private void m_Transaction_Inventory_FixedAssetsFGAP_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("FixAssetOrAdhocRequestAndApprove") == true) return;
            Inventory.FixAssetOrAdhocRequestAndApprove _out = new Inventory.FixAssetOrAdhocRequestAndApprove();
            _out.FormClosed += child_FormClosed;
            _out.MdiParent = this;
            _out.Show();
        }
        private void m_Trans_Inventory_StockTransferInward_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormOpenAlready("InterCompanyInWardEntry") == true) return;
                Inventory.InterCompanyInWardEntry _inward = new Inventory.InterCompanyInWardEntry();
                _inward.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
                _inward.FormClosed += child_FormClosed;
                _inward.MdiParent = this;
                _inward.Show();
            }
            catch { }
        }
        private void m_Trans_Inventory_InterTransferNote_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("InterTransferDocument") == true) return;
            Inventory.InterTransferDocument _interTransfer = new Inventory.InterTransferDocument();
            _interTransfer.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _interTransfer.FormClosed += child_FormClosed;
            _interTransfer.MdiParent = this;
            _interTransfer.Show();
        }
        private void m_Trans_Inventory_GRN_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("GoodsReceivedNote") == true) return;
            Inventory.GoodsReceivedNote _grn = new Inventory.GoodsReceivedNote();
            _grn.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _grn.FormClosed += child_FormClosed;
            _grn.MdiParent = this;
            _grn.Show();
        }
        private void m_Trans_Inventory_PurchaseReturnNote_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("PurchaseReturnNote") == true) return;
            Inventory.PurchaseReturnNote _prn = new Inventory.PurchaseReturnNote();
            _prn.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _prn.FormClosed += child_FormClosed;
            _prn.MdiParent = this;
            _prn.Show();
        }
        private void m_Trans_Inventory_StockAdjustment_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("StockAdjustment") == true) return;
            Inventory.StockAdjustment _adj = new Inventory.StockAdjustment();
            _adj.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _adj.FormClosed += child_FormClosed;
            _adj.MdiParent = this;
            _adj.Show();
        }
        private void m_Trans_Inventory_AFSLRevert_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("StockAdjustment") == true) return;
            Inventory.StockAdjustment _adj = new Inventory.StockAdjustment();
            _adj.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _adj.FormClosed += child_FormClosed;
            _adj.AdjType = "AFSL";
            _adj.MdiParent = this;
            _adj.Show();
        }
        private void m_Trans_Inventory_StatusChange_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ChangeItemStatus") == true) return;
            Inventory.ChangeItemStatus _adjStatus = new Inventory.ChangeItemStatus();
            _adjStatus.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _adjStatus.FormClosed += child_FormClosed;
            _adjStatus.MdiParent = this;
            _adjStatus.Show();
        }
        private void m_Trans_Inventory_AODDocCorr_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AODCorrection") == true) return;
            Inventory.AODCorrection _aodCorrection = new Inventory.AODCorrection();
            _aodCorrection.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _aodCorrection.FormClosed += child_FormClosed;
            _aodCorrection.MdiParent = this;
            _aodCorrection.Show();
        }
        private void m_Trans_Inventory_AODDocCancel_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("InventoryDocumentCancellation") == true) return;
            Inventory.InventoryDocumentCancellation _aodCancel = new Inventory.InventoryDocumentCancellation();
            _aodCancel.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _aodCancel.FormClosed += child_FormClosed;
            _aodCancel.MdiParent = this;
            _aodCancel.Show();
        }
        private void m_Trans_Inventory_ConsStock_StockRec_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ConsignmentReceiveNote") == true) return;
            Inventory.ConsignmentReceiveNote _ConsGrn = new Inventory.ConsignmentReceiveNote();
            _ConsGrn.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ConsGrn.FormClosed += child_FormClosed;
            _ConsGrn.MdiParent = this;
            _ConsGrn.Show();
        }
        private void m_Trans_Inventory_ConsStock_StockRtn_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ConsignmentReturnNote") == true) return;
            Inventory.ConsignmentReturnNote _ConsGrnRtn = new Inventory.ConsignmentReturnNote();
            _ConsGrnRtn.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ConsGrnRtn.FormClosed += child_FormClosed;
            _ConsGrnRtn.MdiParent = this;
            _ConsGrnRtn.Show();
        }
        private void m_Trans_Inventory_QuotationDeliveryOrder_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DeliveryOrderQuotation") == true) return;
            Inventory.DeliveryOrderQuotation _Quodel = new Inventory.DeliveryOrderQuotation();
            _Quodel.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _Quodel.FormClosed += child_FormClosed;
            _Quodel.MdiParent = this;
            _Quodel.Show();
        }
        private void m_Trans_Inventory_WarRepPendSRN_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("WarrantyReplacedPendingSRN") == true) return;
            Inventory.WarrantyReplacedPendingSRN _warRepSRN = new Inventory.WarrantyReplacedPendingSRN();
            _warRepSRN.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _warRepSRN.FormClosed += child_FormClosed;
            _warRepSRN.MdiParent = this;
            _warRepSRN.Show();
        }
        private void m_Trans_Inventory_TempIssue_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("TemporaryIssueItems") == true) return;

            Inventory.TemporaryIssueItems _tempIssueItems = new Inventory.TemporaryIssueItems();
            _tempIssueItems.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _tempIssueItems.FormClosed += child_FormClosed;
            _tempIssueItems.MdiParent = this;
            _tempIssueItems.Show();
        }
        #endregion

        #region Finance
        private void m_Trans_Finance_DayEnd_Click_1(object sender, EventArgs e)
        {
            if (FormOpenAlready("DayEndProcess") == true) return;
            Finance.DayEndProcess _dayEnd = new Finance.DayEndProcess();
            _dayEnd.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _dayEnd.FormClosed += child_FormClosed;
            _dayEnd.MdiParent = this;
            _dayEnd.Show();
        }
        private void m_Trans_Finance_ExtendWarranty_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("WarrantyExtend") == true) return;
            Finance.WarrantyExtend _warrantyExtend = new Finance.WarrantyExtend();
            _warrantyExtend.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _warrantyExtend.FormClosed += child_FormClosed;
            _warrantyExtend.MdiParent = this;
            _warrantyExtend.Show();
        }
        private void m_Trans_Finance_ReceiptEntry_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ReceiptEntry") == true) return;
            Finance.ReceiptEntry _receipt = new Finance.ReceiptEntry();
            _receipt.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _receipt.FormClosed += child_FormClosed;
            _receipt.MdiParent = this;
            _receipt.Show();
        }
        private void m_Trans_Finance_ReceiptReversal_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RecieptReversal") == true) return;
            Finance.RecieptReversal _receiptRev = new Finance.RecieptReversal();
            _receiptRev.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _receiptRev.FormClosed += child_FormClosed;
            _receiptRev.MdiParent = this;
            _receiptRev.Show();
        }

        private void m_Trans_Finance_SUNUpload_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SOS_Upload") == true) return;
            Finance.SOS_Upload _SOSUpload = new Finance.SOS_Upload();
            _SOSUpload.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _SOSUpload.FormClosed += child_FormClosed;
            _SOSUpload.MdiParent = this;
            _SOSUpload.Show();
        }
        private void m_Trans_Finance_CommissionChange_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CommissionChange") == true) return;
            Finance.CommissionChange _commissionChange = new Finance.CommissionChange();
            _commissionChange.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _commissionChange.FormClosed += child_FormClosed;
            _commissionChange.MdiParent = this;
            _commissionChange.Show();
        }
        private void m_Trans_Finance_RemittanceAdjustment_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RemSummaryAdjesment") == true) return;
            Finance.RemSummaryAdjesment _remSumAdj = new Finance.RemSummaryAdjesment();
            _remSumAdj.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _remSumAdj.FormClosed += child_FormClosed;
            _remSumAdj.MdiParent = this;
            _remSumAdj.Show();
        }
        private void m_Trans_Finance_ReturnCheque_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ReturnCheque") == true) return;
            Finance.ReturnCheque _returnCheque = new Finance.ReturnCheque();
            _returnCheque.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _returnCheque.FormClosed += child_FormClosed;
            _returnCheque.MdiParent = this;
            _returnCheque.Show();
        }
        private void m_Trans_Finance_ReturnChequeSettlement_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ReturnChequeSettlement") == true) return;
            Finance.ReturnChequeSettlement _rtnCheqSet = new Finance.ReturnChequeSettlement();
            _rtnCheqSet.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _rtnCheqSet.FormClosed += child_FormClosed;
            _rtnCheqSet.MdiParent = this;
            _rtnCheqSet.Show();
        }
        private void m_Trans_Finance_ExcessShort_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExcessShortRem") == true) return;
            Finance.ExcessShortRem _excessShort = new Finance.ExcessShortRem();
            _excessShort.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _excessShort.FormClosed += child_FormClosed;
            _excessShort.MdiParent = this;
            _excessShort.Show();
        }
        private void m_Trans_Finance_ShortSettle_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExcessShortSettle") == true) return;
            Finance.ExcessShortSettle _shortSettle = new Finance.ExcessShortSettle();
            _shortSettle.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _shortSettle.FormClosed += child_FormClosed;
            _shortSettle.MdiParent = this;
            _shortSettle.Show();
        }
        private void m_Trans_Finance_ProdBonVou_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ProductBonus") == true) return;
            Finance.ProductBonus _productBonus = new Finance.ProductBonus();
            _productBonus.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _productBonus.FormClosed += child_FormClosed;
            _productBonus.MdiParent = this;
            _productBonus.Show();
        }
        private void m_Trans_Finance_OnlinePayment_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("OnlinePaymentConfirmation") == true) return;
            Finance.OnlinePaymentConfirmation _onlinePaymentConfirmation = new Finance.OnlinePaymentConfirmation();
            _onlinePaymentConfirmation.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _onlinePaymentConfirmation.FormClosed += child_FormClosed;
            _onlinePaymentConfirmation.MdiParent = this;
            _onlinePaymentConfirmation.Show();
        }
        private void m_Trans_Finance_ESDProc_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ESDProcess") == true) return;
            Finance.ESDProcess _ecdProcess = new Finance.ESDProcess();
            _ecdProcess.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ecdProcess.FormClosed += child_FormClosed;
            _ecdProcess.MdiParent = this;
            _ecdProcess.Show();
        }
        private void m_Trans_Finance_FinancialAdj_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("AdjustmentReceipt") == true) return;
            Finance.AdjustmentReceipt _finAdj = new Finance.AdjustmentReceipt();
            _finAdj.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _finAdj.FormClosed += child_FormClosed;
            _finAdj.MdiParent = this;
            _finAdj.Show();
        }
        private void m_Trans_Finance_DepositBankUpdate_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DepositBankUpdate") == true) return;
            Finance.DepositBankUpdate _depositBankUpdate = new Finance.DepositBankUpdate();
            _depositBankUpdate.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _depositBankUpdate.FormClosed += child_FormClosed;
            _depositBankUpdate.MdiParent = this;
            _depositBankUpdate.Show();
        }
        private void m_Trans_Finance_BankRealization_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("BankRealization") == true) return;
            Finance.BankRealization _bankReal = new Finance.BankRealization();
            _bankReal.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _bankReal.FormClosed += child_FormClosed;
            _bankReal.MdiParent = this;
            _bankReal.Show();
        }
        private void m_Trans_Finance_CashControl_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CashControl") == true) return;
            Finance.CashControl _cashControl = new Finance.CashControl();
            _cashControl.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _cashControl.FormClosed += child_FormClosed;
            _cashControl.MdiParent = this;
            _cashControl.Show();
        }

        private void m_Trans_Finance_RedeemPromo_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SalePromotorRedeem") == true) return;
            Sales.SalePromotorRedeem _redeemPromo = new Sales.SalePromotorRedeem();
            _redeemPromo.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _redeemPromo.FormClosed += child_FormClosed;
            _redeemPromo.MdiParent = this;
            _redeemPromo.Show();
        }
        private void m_Trans_Finance_LoyaltyUpd_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("LoyaltyUpdate") == true) return;
            General.LoyaltyUpdate _loyUpd = new General.LoyaltyUpdate();
            _loyUpd.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _loyUpd.FormClosed += child_FormClosed;
            _loyUpd.MdiParent = this;
            _loyUpd.Show();
        }
        private void m_Trans_Finance_LoyaltyVouGen_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("LoyaltyVoucherGen") == true) return;
            General.LoyaltyVoucherGen _loyVouGen = new General.LoyaltyVoucherGen();
            _loyVouGen.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _loyVouGen.FormClosed += child_FormClosed;
            _loyVouGen.MdiParent = this;
            _loyVouGen.Show();
        }
        private void m_Trans_Finance_CreditCardRealization_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CreditCardRealization") == true) return;
            Finance.CreditCardRealization _cdrcard = new Finance.CreditCardRealization();
            _cdrcard.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _cdrcard.FormClosed += child_FormClosed;
            _cdrcard.MdiParent = this;
            _cdrcard.Show();
        }
        #endregion

        #region Service
        private void m_Trans_Service_VehiServiceJobs_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehicleJobShowroom") == true) return;
            Services.VehicleJobShowroom _vhServJob = new Services.VehicleJobShowroom();
            _vhServJob.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vhServJob.FormClosed += child_FormClosed;
            _vhServJob.MdiParent = this;
            _vhServJob.Show();
        }
        private void m_Transaction_Service_ACServiceJobs_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ShowroomServicesJob") == true) return;
            Services.ShowroomServicesJob _acServJob = new Services.ShowroomServicesJob();
            _acServJob.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _acServJob.FormClosed += child_FormClosed;
            _acServJob.MdiParent = this;
            _acServJob.Show();
        }
        private void m_Trans_Service_RCC_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RCC_Entry") == true) return;
            Services.RCC_Entry _rccEntry = new Services.RCC_Entry();
            _rccEntry.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _rccEntry.FormClosed += child_FormClosed;
            _rccEntry.MdiParent = this;
            _rccEntry.Show();
        }
        private void m_Trans_Service_WarrantyClaimCreditNote_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("WarrantyClaimCreditNote") == true) return;
            Sales.WarrantyClaimCreditNote _wara = new Sales.WarrantyClaimCreditNote();
            _wara.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _wara.FormClosed += child_FormClosed;
            _wara.MdiParent = this;
            _wara.Show();
        }
        private void m_Trans_Service_ProductExchangeReceipt_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExchangeRequestReceive_new") == true) return;
            Services.ExchangeRequestReceive_new _vhServJob = new Services.ExchangeRequestReceive_new();
            _vhServJob.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vhServJob.FormClosed += child_FormClosed;
            _vhServJob.MdiParent = this;
            _vhServJob.Show();
        }
        private void m_Trans_Service_ProductExchangeIssue_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExchangeIssue_new") == true) return;
            Services.ExchangeIssue_new _vhServJob = new Services.ExchangeIssue_new();
            _vhServJob.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vhServJob.FormClosed += child_FormClosed;
            _vhServJob.MdiParent = this;
            _vhServJob.Show();
        }
        private void m_Trans_Service_Approve_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Approval") == true) return;
            Services.Approval Sup_Wr = new Services.Approval();
            Sup_Wr.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            Sup_Wr.FormClosed += child_FormClosed;
            Sup_Wr.MdiParent = this;
            Sup_Wr.Show();
        }
        private void m_Trans_Service_ProductExchangeIssue_old_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ExchangeIssue") == true) return;
            Services.ExchangeIssue _vhServJob = new Services.ExchangeIssue();
            _vhServJob.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vhServJob.FormClosed += child_FormClosed;
            _vhServJob.MdiParent = this;
            _vhServJob.Show();
        }

        private void m_Trans_Service_ScvInvoice_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Service_Invoice2") == true) return;
            Services.Service_Invoice2 Sup_Wr = new Services.Service_Invoice2();
            Sup_Wr.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            Sup_Wr.FormClosed += child_FormClosed;
            Sup_Wr.MdiParent = this;
            Sup_Wr.Show();
        }
        private void m_Trans_JobEstimate_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("JobEstimate") == true) return;
            Services.JobEstimate frm = new Services.JobEstimate();
            frm.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            frm.FormClosed += child_FormClosed;
            frm.MdiParent = this;
            frm.Show();
        }
        private void m_Trans_WorkInProgress_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceWIP") == true) return;
            Services.ServiceWIP frm = new Services.ServiceWIP();
            frm.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            frm.FormClosed += child_FormClosed;
            frm.MdiParent = this;
            frm.Show();
        }
        private void m_Trans_Service_JobRequest_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Service_Request") == true) return;
            Services.Service_Request _JobReq = new Services.Service_Request();
            _JobReq.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _JobReq.FormClosed += child_FormClosed;
            _JobReq.MdiParent = this;
            _JobReq.Show();
        }
        private void m_Trans_Service_JobEntry_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("JobEntry") == true) return;
            Services.JobEntry _JobEntry = new Services.JobEntry();
            _JobEntry.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _JobEntry.FormClosed += child_FormClosed;
            _JobEntry.MdiParent = this;
            _JobEntry.Show();
        }
        private void m_Trans_technicianAllocation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("TechnicianAllocation") == true) return;
            Services.TechnicianAllocation frm = new Services.TechnicianAllocation();
            frm.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            frm.FormClosed += child_FormClosed;
            frm.MdiParent = this;
            frm.Show();
        }
        private void m_Trans_Service_SupplierWarrantyClaim_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SupplierWarrntyClaim") == true) return;
            Services.SupplierWarrntyClaim Sup_Wr = new Services.SupplierWarrntyClaim();
            Sup_Wr.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            Sup_Wr.FormClosed += child_FormClosed;
            Sup_Wr.MdiParent = this;
            Sup_Wr.Show();
        }
        private void m_Trans_Service_Conf_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("JobConfirmation") == true) return;
            Services.JobConfirmation Job_Conf = new Services.JobConfirmation();
            Job_Conf.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            Job_Conf.FormClosed += child_FormClosed;
            Job_Conf.MdiParent = this;
            Job_Conf.Show();
        }
        private void m_Trans_Service_GatePass_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceGatePass") == true) return;

            Services.ServiceGatePass _gatePass = new Services.ServiceGatePass();
            _gatePass.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _gatePass.FormClosed += child_FormClosed;
            _gatePass.MdiParent = this;
            _gatePass.Show();
        }
        private void m_Trans_Service_DlrScvInvoice_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceInv") == true) return;
            Services.ServiceInv _DlServiceInv = new Services.ServiceInv();
            _DlServiceInv.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _DlServiceInv.FormClosed += child_FormClosed;
            _DlServiceInv.MdiParent = this;
            _DlServiceInv.Show();
        }
        private void m_Trans_Service_Itemcannibalize_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Service_Canibalize") == true) return;
            Services.Service_Canibalize _itemCanib = new Services.Service_Canibalize();
            _itemCanib.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _itemCanib.FormClosed += child_FormClosed;
            _itemCanib.MdiParent = this;
            _itemCanib.Show();
        }
        private void m_Trans_Service_CustSatis_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CustomerSatisfaction") == true) return;
            Services.CustomerSatisfaction _custSatis = new Services.CustomerSatisfaction();
            _custSatis.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _custSatis.FormClosed += child_FormClosed;
            _custSatis.MdiParent = this;
            _custSatis.Show();
        }
        private void m_Trans_Service_JobTransfer_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceJobTransfer") == true) return;
            Services.ServiceJobTransfer _jobTrs = new Services.ServiceJobTransfer();
            _jobTrs.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _jobTrs.FormClosed += child_FormClosed;
            _jobTrs.MdiParent = this;
            _jobTrs.Show();
        }
        private void m_Trans_Service_MainItemcannibalize_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Service_CanibalizeMainUnit") == true) return;
            Services.Service_CanibalizeMainUnit _mitemCanib = new Services.Service_CanibalizeMainUnit();
            _mitemCanib.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _mitemCanib.FormClosed += child_FormClosed;
            _mitemCanib.MdiParent = this;
            _mitemCanib.Show();
        }
        private void m_Trans_Service_Agreement_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceAgreement") == true) return;
            Services.ServiceAgreement _agrmnt = new Services.ServiceAgreement();
            _agrmnt.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _agrmnt.FormClosed += child_FormClosed;
            _agrmnt.MdiParent = this;
            _agrmnt.Show();
        }
        private void m_Trans_Service_POConf_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("POConfirmation") == true) return;
            Services.POConfirmation PO_Conf = new Services.POConfirmation();
            PO_Conf.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            PO_Conf.FormClosed += child_FormClosed;
            PO_Conf.MdiParent = this;
            PO_Conf.Show();
        }
        #endregion

        #region Additional
        private void m_Additional_Reprint_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ReprintDocuments") == true) return;
            General.ReprintDocuments _reprint = new General.ReprintDocuments();
            _reprint.FormClosed += child_FormClosed;
            _reprint.MdiParent = this;
            _reprint.Show();
        }
        private void m_Additional_BackDate_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("BackDate") == true) return;
            Security.BackDate _backDate = new Security.BackDate();
            _backDate.FormClosed += child_FormClosed;
            _backDate.MdiParent = this;
            _backDate.Show();
        }
        private void m_Additional_VehicleInsurance_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehicalInsurance") == true) return;
            General.VehicalInsurance _insurance = new General.VehicalInsurance();
            _insurance.FormClosed += child_FormClosed;
            _insurance.MdiParent = this;
            _insurance.Show();
        }
        private void m_Additional_VehicleReg_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehicalRegistration") == true) return;
            General.VehicalRegistration _vehicalRegistration = new General.VehicalRegistration();
            _vehicalRegistration.FormClosed += child_FormClosed;
            _vehicalRegistration.MdiParent = this;
            _vehicalRegistration.Show();
        }
        private void m_Additional_ManualDoc_Receive_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ManualDocReceived") == true) return;
            General.ManualDocReceived _manualDocReceived = new General.ManualDocReceived();
            _manualDocReceived.FormClosed += child_FormClosed;
            _manualDocReceived.MdiParent = this;
            _manualDocReceived.Show();
        }
        private void m_Trans_Finance_DealerComm_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DealerCommission") == true) return;
            Sales.DealerCommission _dealerComm = new Sales.DealerCommission();
            _dealerComm.FormClosed += child_FormClosed;
            _dealerComm.MdiParent = this;
            _dealerComm.Show();
        }
        private void m_Trans_Finance_IntPayVoucher_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VoucherEntry") == true) return;
            Finance.VoucherEntry _IntPayVoucher = new Finance.VoucherEntry();
            _IntPayVoucher.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _IntPayVoucher.FormClosed += child_FormClosed;
            _IntPayVoucher.MdiParent = this;
            _IntPayVoucher.Show();
        }
        private void m_Additional_ManualDoc_Cancelation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ManualDocumentCancel") == true) return;
            General.ManualDocumentCancel _manualDocCancel = new General.ManualDocumentCancel();
            _manualDocCancel.FormClosed += child_FormClosed;
            _manualDocCancel.MdiParent = this;
            _manualDocCancel.Show();
        }
        private void m_Additional_ScanPhyDocs_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Scan_Physical_Docs") == true) return;
            Finance.Scan_Physical_Docs _scanDocs = new Finance.Scan_Physical_Docs();
            _scanDocs.FormClosed += child_FormClosed;
            _scanDocs.MdiParent = this;
            _scanDocs.Show();
        }
        private void m_Additional_DocCheckList_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Document_Check_List") == true) return;
            General.Document_Check_List _docCheckList = new General.Document_Check_List();
            _docCheckList.FormClosed += child_FormClosed;
            _docCheckList.MdiParent = this;
            _docCheckList.Show();
        }
        private void m_Additional_GiftVouchers_IssueForItems_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("IssueGiftVoucherItems") == true) return;
            General.IssueGiftVoucherItems _giftItems = new General.IssueGiftVoucherItems();
            _giftItems.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _giftItems.FormClosed += child_FormClosed;
            _giftItems.MdiParent = this;
            _giftItems.Show();
        }
        private void m_Additional_GiftVouchers_Amendments_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("IssueGiftVoucherAmenment") == true) return;
            General.IssueGiftVoucherAmenment _giftAmd = new General.IssueGiftVoucherAmenment();
            _giftAmd.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _giftAmd.FormClosed += child_FormClosed;
            _giftAmd.MdiParent = this;
            _giftAmd.Show();
        }
        private void m_Additional_GiftVouchers_Settlement_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("GiftVoucherSettle") == true) return;
            General.GiftVoucherSettle _giftSetl = new General.GiftVoucherSettle();
            _giftSetl.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _giftSetl.FormClosed += child_FormClosed;
            _giftSetl.MdiParent = this;
            _giftSetl.Show();
        }
        private void m_Additional_SpecVouchers_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("IssueGiftVoucherSpecial") == true) return;
            General.IssueGiftVoucherSpecial _specVou = new General.IssueGiftVoucherSpecial();
            _specVou.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _specVou.FormClosed += child_FormClosed;
            _specVou.MdiParent = this;
            _specVou.Show();
        }
        private void m_Additional_CredSaleDocs_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Credit_Sale_Docs") == true) return;
            General.Credit_Sale_Docs _credSalesDocs = new General.Credit_Sale_Docs();
            _credSalesDocs.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _credSalesDocs.FormClosed += child_FormClosed;
            _credSalesDocs.MdiParent = this;
            _credSalesDocs.Show();
        }
        private void m_Additional_HPManReceiptReqApp_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HpManualReceiptRequestApproval") == true) return;
            General.HpManualReceiptRequestApproval _HpManuRecReqApp = new General.HpManualReceiptRequestApproval();
            _HpManuRecReqApp.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _HpManuRecReqApp.FormClosed += child_FormClosed;
            _HpManuRecReqApp.MdiParent = this;
            _HpManuRecReqApp.Show();
        }
        private void m_Additional_BOCCustRes_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("BulkInvReserve") == true) return;
            Sales.BulkInvReserve _bulkInvReserve = new Sales.BulkInvReserve();
            _bulkInvReserve.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _bulkInvReserve.FormClosed += child_FormClosed;
            _bulkInvReserve.MdiParent = this;
            _bulkInvReserve.Show();
        }
        private void m_Additional_ItemCondSetup_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("frmItemConditionSetup") == true) return;
            FF.WindowsERPClient.General.frmItemConditionSetup frm = new General.frmItemConditionSetup();
            frm.FormClosed += child_FormClosed;
            frm.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            frm.MdiParent = this;
            frm.Show();
        }
        private void m_Additional_Approval_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RequestApproval") == true) return;
            General.RequestApproval req_approval = new General.RequestApproval();
            req_approval.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            req_approval.FormClosed += child_FormClosed;
            req_approval.MdiParent = this;
            req_approval.Show();
        }
        private void m_Additional_Cancel_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("RequestCancel") == true) return;
            HP.HPRequestCancel req_cancel = new HP.HPRequestCancel();
            req_cancel.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            req_cancel.FormClosed += child_FormClosed;
            req_cancel.MdiParent = this;
            req_cancel.Show();
        }
        private void m_Additional_ToursEnquiry_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ToursEnquiry") == true) return;
            General.ToursEnquiry _turEnq = new General.ToursEnquiry();
            _turEnq.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _turEnq.FormClosed += child_FormClosed;
            _turEnq.MdiParent = this;
            _turEnq.Show();
        }
        private void m_Additional_InstallReq_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ACInstallRequest") == true) return;
            Services.ACInstallRequest _insReq = new Services.ACInstallRequest();
            _insReq.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _insReq.FormClosed += child_FormClosed;
            _insReq.MdiParent = this;
            _insReq.Show();
        }
          private void m_Additional_InvCustChange_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("InvoiceCustomerChang") == true) return;
            Sales.InvoiceCustomerChang _cusChange = new Sales.InvoiceCustomerChang();
            _cusChange.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _cusChange.FormClosed += child_FormClosed;
            _cusChange.MdiParent = this;
            _cusChange.Show();
        }
        

        private void m_Additional_VehInsRenewPay_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehInsRentPay") == true) return;
            Finance.VehInsRentPay _vehInsRen = new Finance.VehInsRentPay();
            _vehInsRen.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vehInsRen.FormClosed += child_FormClosed;
            _vehInsRen.MdiParent = this;
            _vehInsRen.Show();
        }

        private void m_Additional_VehInsRenewal_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehInsRenewal") == true) return;
            Finance.VehInsRenewal _vehInsRenew = new Finance.VehInsRenewal();
            _vehInsRenew.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vehInsRenew.FormClosed += child_FormClosed;
            _vehInsRenew.MdiParent = this;
            _vehInsRenew.Show();
        }
        private void m_Additional_VehInsDebit_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("VehInsDebitNote") == true) return;
            Finance.VehInsDebitNote _vehInsDebit = new Finance.VehInsDebitNote();
            _vehInsDebit.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _vehInsDebit.FormClosed += child_FormClosed;
            _vehInsDebit.MdiParent = this;
            _vehInsDebit.Show();
        }
        #endregion

        #region Reports
        private void m_Reports_Sales_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Sales_Rep") == true) return;
            Sales.Sales_Rep _salesRep = new Sales.Sales_Rep();
            _salesRep.FormClosed += child_FormClosed;
            _salesRep.MdiParent = this;
            _salesRep.Show();
        }
        private void m_Reports_Sales2_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Sales2_Rep") == true) return;
            Reports.Sales_2.Sales2_Rep _sales2Rep = new Reports.Sales_2.Sales2_Rep();
            _sales2Rep.FormClosed += child_FormClosed;
            _sales2Rep.MdiParent = this;
            _sales2Rep.Show();
        }
        private void m_Reports_HP_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("HP_Rep") == true) return;
            Reports.HP.HP_Rep _hpRep = new Reports.HP.HP_Rep();
            _hpRep.FormClosed += child_FormClosed;
            _hpRep.MdiParent = this;
            _hpRep.Show();
        }
        private void m_Reports_Inventory_Click(object sender, EventArgs e)
        {
            //if (BaseCls.GlbUserID == "ADMIN") by Prabahth on 5/03/2013 as per request by Mr.Kelum 
            //{
            if (FormOpenAlready("Inv_Rep") == true) return;
            Reports.Inventory.Inv_Rep _invRep = new Reports.Inventory.Inv_Rep();
            _invRep.FormClosed += child_FormClosed;
            _invRep.MdiParent = this;
            _invRep.Show();
            //}
        }
        private void m_Reports_Finance_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Financial_Rep") == true) return;
            Reports.Finance.Financial_Rep _finRep = new Reports.Finance.Financial_Rep();
            _finRep.FormClosed += child_FormClosed;
            _finRep.MdiParent = this;
            _finRep.Show();
        }
        private void m_Reports_Service_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Service__Rep") == true) return;
            Reports.Service.Service__Rep _serviceRep = new Reports.Service.Service__Rep();
            _serviceRep.FormClosed += child_FormClosed;
            _serviceRep.MdiParent = this;
            _serviceRep.Show();
        }
        private void m_Reports_Reconciliation_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Reconcile_Rep") == true) return;
            Reports.Reconciliation.Reconcile_Rep _reconRep = new Reports.Reconciliation.Reconcile_Rep();
            _reconRep.FormClosed += child_FormClosed;
            _reconRep.MdiParent = this;
            _reconRep.Show();
        }
        private void m_Reports_Audit_Click(object sender, EventArgs e)
        {
            //Add new menu 24/10/2013 Sanjeewa/Chamal
            if (FormOpenAlready("Audit_Rep") == true) return;
            Reports.Audit.Audit_Rep _auditRep = new Reports.Audit.Audit_Rep();
            _auditRep.FormClosed += child_FormClosed;
            _auditRep.MdiParent = this;
            _auditRep.Show();
        }
        private void m_Reports_Other_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Service_Rep") == true) return;
            Reports.Service.Service_Rep _serviceRep = new Reports.Service.Service_Rep();
            _serviceRep.FormClosed += child_FormClosed;
            _serviceRep.MdiParent = this;
            _serviceRep.Show();
        }
        #endregion

        #region Security
        private void m_System_Security_Admin_NewMenuRegistration_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SystemMenuRegistration") == true) return;
            Security.SystemMenuRegistration _systemMenuRegistration = new Security.SystemMenuRegistration();
            _systemMenuRegistration.FormClosed += child_FormClosed;
            _systemMenuRegistration.MdiParent = this;
            _systemMenuRegistration.Show();
        }

        private void m_System_Security_User_ChgPw_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("UserChangePassword") == true) return;
            Security.UserChangePassword _chgPw = new Security.UserChangePassword();
            _chgPw.FormClosed += child_FormClosed;
            _chgPw.MdiParent = this;
            _chgPw.Show();
        }

        private void m_System_Security_Admin_RoleCreation_Click(object sender, EventArgs e)
        {
            if (_base.CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "SEC1") == false)
            {
                MessageBox.Show("No permission for 'Create New Role' task!", "Power User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (FormOpenAlready("RoleCreation") == true) return;
                Security.RoleCreation _roleMst = new Security.RoleCreation();
                _roleMst.FormClosed += child_FormClosed;
                _roleMst.MdiParent = this;
                _roleMst.Show();
            }
        }
        private void m_System_Security_Admin_UserProf_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("UserCreation") == true) return;
            Security.UserCreation _userProf = new Security.UserCreation();
            _userProf.FormClosed += child_FormClosed;
            _userProf.MdiParent = this;
            _userProf.Show();
        }

        private void m_System_Security_Admin_SysChk_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SystemChecking") == true) return;
            Security.SystemChecking _sysChk = new Security.SystemChecking();
            _sysChk.FormClosed += child_FormClosed;
            _sysChk.MdiParent = this;
            _sysChk.Show();
        }

        private void m_System_Security_Admin_SecurityPolicy_Click(object sender, EventArgs e)
        {
            if (BaseCls.GlbUserID == "ADMIN")
            {
                if (FormOpenAlready("PasswordPolicy") == true) return;
                Security.PasswordPolicy _SecurityPolicy = new Security.PasswordPolicy();
                _SecurityPolicy.FormClosed += child_FormClosed;
                _SecurityPolicy.MdiParent = this;
                _SecurityPolicy.Show();
            }
        }

        #endregion

        #region  Purchasing
        private void m_Trans_Purchasing_LOPO_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("PurchaseOrder") == true) return;
            Purchase.PurchaseOrder _lopo = new Purchase.PurchaseOrder();
            _lopo.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _lopo.FormClosed += child_FormClosed;
            _lopo.MdiParent = this;
            _lopo.Show();
        }
        private void m_Trans_Purchasing_SupQuot_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SupplierQuotation") == true) return;
            Sales.SupplierQuotation _supQuot = new Sales.SupplierQuotation();
            _supQuot.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _supQuot.FormClosed += child_FormClosed;
            _supQuot.MdiParent = this;
            _supQuot.Show();
        }
        #endregion

        #region Pricing
        private void m_Trans_Pricing_PriceDef_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("PriceDefinition") == true) return;
            Sales.PriceDefinition _priceDef = new Sales.PriceDefinition();
            _priceDef.FormClosed += child_FormClosed;
            _priceDef.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _priceDef.MdiParent = this;
            _priceDef.Show();
        }
        #endregion

        #region Internal Audit
        private void m_Trans_Audit_PhyCashVerif_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("PhysicalCashVerification") == true) return;
            Finance.PhysicalCashVerification _phyCashVerf = new Finance.PhysicalCashVerification();
            _phyCashVerf.FormClosed += child_FormClosed;
            _phyCashVerf.MdiParent = this;
            _phyCashVerf.Show();
        }

        private void m_Trans_Audit_PhylStockVerif_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("PhysicalStockVerification") == true) return;
            Finance.PhysicalStockVerification _phyStkVerf = new Finance.PhysicalStockVerification();
            _phyStkVerf.FormClosed += child_FormClosed;
            _phyStkVerf.MdiParent = this;
            _phyStkVerf.Show();
        }

        #endregion

        #endregion

        #region dynamically add menu
        private void MainMenu_Load(object sender, EventArgs e)
        {
            MenuStrip a = new MenuStrip();
            a.ItemAdded += new ToolStripItemEventHandler(kiki);

            _posX = this.Width - pnlAlert.Width - 17;
            pnlAlert.Location = new Point(_posX, _posY);
            isAlertUser = IsAlertUsers();
            if (isAlertUser)
                timAlert_Tick(null, null);

            GetNotificationDetails();
        }
        private void MainMenu_Shown(object sender, EventArgs e)
        {
            if (IsAlertUsers())
            {
                //added 2013/08/2
                //stop alert 
                List<Hpr_SysParameter> _list = _base.CHNLSVC.Sales.GetAll_hpr_Para("ALERT", "COM", BaseCls.GlbUserComCode);
                if (_list != null && _list.Count > 0)
                {
                    if (_list[0].Hsy_val == 1)
                    {
                        ThreadingAlert();
                    }
                }
            }
            else
            {
                //timAlert.Enabled = false;
                //timPnlShift.Enabled = false;
            }
        }

        private void kiki(object sender, EventArgs e)
        { }
        #endregion

        #region Application Exit Function
        private void m_System_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (BaseCls.GlbIsExit == false)
                {
                    _base.CHNLSVC.CloseChannel();
                    _base.CHNLSVC.Security.ExitLoginSession(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID);
                    BaseCls.GlbIsExit = true;
                }
                Application.Exit();
            }
            catch (Exception err)
            {
                _base.CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Exit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_System_SignOff_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SignOff"))
            {
                return;
            }
            General.SignOff _signoff = new General.SignOff();
            _signoff.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _signoff.FormClosed += child_FormClosed;
            _signoff.MdiParent = this;
            _signoff.Show();
        }

        private void m_System_SignOn_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("SignOn") == true) return;
            General.SignOn _signon = new General.SignOn();
            _signon.FormClosed += child_FormClosed;
            _signon.MdiParent = this;
            _signon.Show();
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_threadAlert != null)
                {
                    _threadAlert.Abort();
                }
                if (BaseCls.GlbIsExit == false)
                {
                    _base.CHNLSVC.CloseChannel();
                    _base.CHNLSVC.Security.ExitLoginSession(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID);
                    BaseCls.GlbIsExit = true;
                }

                Application.Exit();
            }
            catch (Exception err)
            {

                _base.CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Exit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        #endregion

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void tsslLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVal;
                _CommonSearch.txtSearchbyword.Text = txtVal.Text;
                _CommonSearch.ShowDialog();
                if (!string.IsNullOrEmpty(txtVal.Text))
                {
                    if (_base.CheckLocation(BaseCls.GlbUserComCode, txtVal.Text.ToString()) == false)
                    {
                        MessageBox.Show("Selected location " + txtVal.Text.ToString() + " is invalid or inactivated!", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    BaseCls.GlbUserDefLoca = txtVal.Text;
                    tsslLoc.Text = txtVal.Text;
                    _base.LoadLocationDetail();
                    txtVal.Clear();
                    CloseAll();
                }
                _base.CHNLSVC.CloseAllChannels();
                GetNotificationDetails();              
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                _base.CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsslPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVal;
                _CommonSearch.txtSearchbyword.Text = txtVal.Text;
                _CommonSearch.ShowDialog();
                if (!string.IsNullOrEmpty(txtVal.Text))
                {
                    if (_base.CheckProfitCenter(BaseCls.GlbUserComCode, txtVal.Text.ToString()) == false)
                    {
                        MessageBox.Show("Selected profit center " + txtVal.Text.ToString() + " is invalid or inactivated!", "Invalid Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    BaseCls.GlbUserDefProf = txtVal.Text;
                    tsslPC.Text = txtVal.Text;
                    _base.LoadProfitCenterDetail();
                    txtVal.Clear();
                    CloseAll();
                }
                _base.CHNLSVC.CloseAllChannels();
                GetNotificationDetails(); 
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                _base.CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Other Functions

        void CloseAll()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.L)
            {
                tsslLoc_Click(null, null);
            }
            else if (e.Alt && e.KeyCode == Keys.P)
            {
                tsslPC_Click(null, null);
            }
        }

        #endregion

        #region T E S T I N G
        private void testPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (BaseCls.GlbUserID == "ADMIN")
            //{
            //Reports.Sales.invoice1 _testPrint = new Reports.Sales.invoice1();
            //_testPrint.MdiParent = this;
            //_testPrint.Show();

            //General.IssueGiftVoucherItems _gift = new General.IssueGiftVoucherItems();
            //_gift.MdiParent = this;
            //_gift.Show();

            //HP.EcdVoucher _ecdDef = new HP.EcdVoucher();
            //_ecdDef.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            //_ecdDef.MdiParent = this;
            //_ecdDef.Show();

            //General.CashDiscountDefinition _cash = new General.CashDiscountDefinition();
            //_cash.MdiParent = this;
            //_cash.Show();
            //}
            //if (BaseCls.GlbUserID == "1")
            //{
            if (BaseCls.GlbUserID == "ADMIN")
            {
                Reports.Sales.DataMigrate _testPrint = new Reports.Sales.DataMigrate();
                _testPrint.FormClosed += child_FormClosed;
                _testPrint.MdiParent = this;
                _testPrint.Show();
            }
            else
            {
                Reports.Sales.invoice1 _testPrint = new Reports.Sales.invoice1();
                _testPrint.FormClosed += child_FormClosed;
                _testPrint.MdiParent = this;
                _testPrint.Show();
            }
            //    }


        }

        private void directPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BaseCls.GlbUserID == "ADMIN")
            {
                //Reports.Sales.testprint _testPrint = new Reports.Sales.testprint();
                //_testPrint.MdiParent = this;
                //_testPrint.Show();

                Inventory.DocDateCorrection _testPrint = new Inventory.DocDateCorrection();
                _testPrint.FormClosed += child_FormClosed;
                _testPrint.MdiParent = this;
                _testPrint.Show();
                General.CashDiscountDefinition _cash = new General.CashDiscountDefinition();
                _cash.FormClosed += child_FormClosed;
                _cash.MdiParent = this;
                _cash.Show();
                //Inventory.DocDateCorrection _testPrint = new Inventory.DocDateCorrection();
                //_testPrint.MdiParent = this;
                //_testPrint.Show();
                //General.CashDiscountDefinition _cash = new General.CashDiscountDefinition();
                //_cash.MdiParent = this;
                //_cash.Show();
            }


            if (BaseCls.GlbUserID == "ADMIN")
            {
                General.ACserviceChargesDefinition _commissionDefinition = new General.ACserviceChargesDefinition();
                _commissionDefinition.FormClosed += child_FormClosed;
                _commissionDefinition.MdiParent = this;
                _commissionDefinition.Show();
            }
        }
        #endregion

        #region Notofication Panal

        private void tssNotifications_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("frmNotification") == true)
            {
                if (ActiveMdiChild != null)
                {
                    if (ActiveMdiChild.Name == "frmNotification")
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["ShowNotification"].ToString() == "0")
                        {
                            return;
                        }

                        frmNotification child = ActiveMdiChild as frmNotification;
                        child.ShowDetails(0);
                        child.setPanalPossitions();
                    }
                }
                else
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["ShowNotification"].ToString() == "0")
                    {
                        return;
                    }

                    frmNotification frm = new frmNotification();
                    frm.MdiParent = this;
                    frm.ControlBox = false;
                    frm.Show();
                }
            }
            else
            {

                if (this.MdiChildren.Length > 0)
                {
                    DialogResult ds = MessageBox.Show("Do you want to close all forms?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (ds == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (Form childForm in MdiChildren)
                        {
                            childForm.Close();
                        }
                    }
                }
            }
        }

        public bool GetNotificationDetails()
        {
            bool status = false;
            if (FormOpenAlready("frmNotification") == true)
            {
                return status;
            }

            if (this.MdiChildren.Length > 1)
            {
                return status;
            }

            if (System.Configuration.ConfigurationManager.AppSettings["ShowNotification"].ToString() == "0")
            {
                return true;
            }
            //tssNotifications.Enabled = true;
            frmNotification frm = new frmNotification();
            frm.MdiParent = this;
            frm.ControlBox = false;
            frm.Show();
            return true;
        }

        private void CloseNotification()
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "frmNotification")
                {
                    item.Close();
                    //tssNotifications.Enabled = false;
                    return;
                }
            }
        }

        public void DisposeAllButThis(Form form)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == form.GetType()
                    && frm != form)
                {
                    frm.Close();
                }
            }
        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetNotificationDetails();//-- Sanjeewa 2016-11-15 Remove login issue when Standby Down
        }

        #endregion

        private void barcodePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Barcode") == true) return;
            BaseCls.GlbReportDoc = "";
            MultipleBarcode brcode = new MultipleBarcode();
            brcode.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            brcode.FormClosed += child_FormClosed;
            brcode.MdiParent = this;
            brcode.Show();
        }


        private void m_Stock_Verification_New_Click(object sender, EventArgs e) //By Akila
        {
            if (FormOpenAlready("AuditStockCount") == true) return;
            Audit.AuditStockCount _stockCount = new Audit.AuditStockCount();
            _stockCount.FormClosed += child_FormClosed;
            _stockCount.MdiParent = this;
            _stockCount.Show();
        }

        private void m_MST_Inventory_StusChangeDef_Click(object sender, EventArgs e)
        {
            //if (FormOpenAlready("ItemStatusChangeDef") == true) return;
            //General.ItemStatusChangeDef _itmstusdef = new General.ItemStatusChangeDef();
            //_itmstusdef.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            //_itmstusdef.FormClosed += child_FormClosed;
            //_itmstusdef.MdiParent = this;
            //_itmstusdef.Show();

            //Added new form by Udesh 17-Oct-2018
            if (FormOpenAlready("ItemStatusChangeDefinition") == true) return;
            General.ItemStatusChangeDefinition _itmstusdef = new General.ItemStatusChangeDefinition();
            _itmstusdef.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _itmstusdef.FormClosed += child_FormClosed;
            _itmstusdef.MdiParent = this;
            _itmstusdef.Show();
        }

        private void m_MST_Service_ServiceAreaSetup_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("ServiceAreaSetup") == true) return;
            Services.ServiceAreaSetup _Services = new Services.ServiceAreaSetup();
            _Services.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _Services.FormClosed += child_FormClosed;
            _Services.MdiParent = this;
            _Services.Show();
        }

        private void m_Trans_Inventory_CustomerDeliveryOrderShedule_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("DeliverySchedule") == true) return;
            Inventory.DeliverySchedule _DeliverySchedule = new Inventory.DeliverySchedule();
            _DeliverySchedule.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _DeliverySchedule.FormClosed += child_FormClosed;
            _DeliverySchedule.MdiParent = this;
            _DeliverySchedule.Show();
        }

        private void m_Enquiries_Click(object sender, EventArgs e)
        {

        }

        private void m_Trans_Finance_ValuationUpload_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("Valuation_Upload") == true) return;
            Finance.Valuation_Upload _ValuationUpload = new Finance.Valuation_Upload();
            _ValuationUpload.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _ValuationUpload.FormClosed += child_FormClosed;
            _ValuationUpload.MdiParent = this;
            _ValuationUpload.Show();
        }

        // Tharindu 2017-12-05
        private void productRefernceDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void m_Trans_Inventory_StockAdjustmentprocess_Click(object sender, EventArgs e)
        {
            //if (FormOpenAlready("StockAdjustmentProcess") == true) return;
            //Inventory.StockAdjustmentprocess _adj = new Inventory.StockAdjustmentprocess();
            //_adj.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            //_adj.FormClosed += child_FormClosed;
            //_adj.MdiParent = this;
            //_adj.Show();

            if (FormOpenAlready("ADJReqstProcess") == true) return;
            Inventory.ADJReqstProcess _adj = new Inventory.ADJReqstProcess();
            _adj.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            _adj.FormClosed += child_FormClosed;
            _adj.MdiParent = this;
            _adj.Show();

        }

        
        private void productRefernceDefinitionToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           
        }

        //Add by Tharindu 29-Dec-2017
        private void m_MST_Inventory_WhAppItemSetup_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormOpenAlready("WareHouseItemSetup") == true) return;
                Inventory.WareHouseItemSetup _itemWH = new Inventory.WareHouseItemSetup();
                _itemWH.FormClosed += child_FormClosed;
                _itemWH.MdiParent = this;
                _itemWH.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Tharindu 2017-12-29
        private void m_MST_Inventory_ProdRefDefinition_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormOpenAlready("ProductReferenceDefinition") == true) return;
                Inventory.ProductReferenceDefinition _proref = new Inventory.ProductReferenceDefinition();
                _proref.FormClosed += child_FormClosed;
                _proref.MdiParent = this;
                _proref.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void m_Trans_Finance_CreditCardRealizationNew_Click(object sender, EventArgs e)
        {
            if (FormOpenAlready("CreditCardRealization_new") == true) return;
            Finance.CreditCardRealization_new CreditCardRealization = new Finance.CreditCardRealization_new();
            CreditCardRealization.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
            CreditCardRealization.FormClosed += child_FormClosed;
            CreditCardRealization.MdiParent = this;
            CreditCardRealization.Show();
        }

        private void m_Trans_Service_Click(object sender, EventArgs e)
        {

        }


  



   





 






      

    



     











       
        //---- Do not Add menu items in here -------------------------//
        //---- Add those are relavent menu section code above --------//
    }
}
