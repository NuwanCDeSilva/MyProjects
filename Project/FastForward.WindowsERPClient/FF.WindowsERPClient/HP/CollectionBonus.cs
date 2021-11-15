using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.HP
{
    public partial class CollectionBonus : Form
    {

        #region properties

        HP.CollectionBonusDefinition _hpCollectionBonusDefinition;
        HP.GracePeriodDefinition _hpGracePeriodDefinition;
        HP.ManagerCreation _hpManagerCreation;
        HP.CollectionBonusAdjusment _hpCollectionBonusAdjusment;
        HP.DisqulifiedBonus _hpDisqulifiedBonus;
        HP.DisregardAmount _hpDisregardAmount;
        HP.HandlingOverAccounts _hpHandlingOverAccounts;
        #endregion

        public CollectionBonus()
        {
            InitializeComponent();
            _hpCollectionBonusDefinition = null;
            _hpGracePeriodDefinition = null;
            _hpManagerCreation = null;
            _hpCollectionBonusAdjusment = null;
            _hpDisqulifiedBonus = null;
            _hpDisregardAmount = null;
            _hpHandlingOverAccounts = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes) {
                this.Close();
            }
        }


        private void chkCollectionBonusDefinition_CheckedChanged(object sender, EventArgs e)
        {
          
            if (chkCollectionBonusDefinition.Checked) {
                CollectionBonusDefinition form = (CollectionBonusDefinition)GetOpenedForm<CollectionBonusDefinition>();
                if (form==null)
                {
                    _hpCollectionBonusDefinition = new CollectionBonusDefinition();
                   _hpCollectionBonusDefinition.MdiParent =(Form)this.Parent.Parent;
                    _hpCollectionBonusDefinition.Show();
                   
                }
                else
                {
                    {
                        if (_hpCollectionBonusDefinition.WindowState == FormWindowState.Minimized)
                            _hpCollectionBonusDefinition.WindowState = FormWindowState.Normal;
                        _hpCollectionBonusDefinition.Focus();
                        _hpCollectionBonusDefinition.BringToFront();
                    }
                }
            }
        }

        public static Form GetOpenedForm<T>() where T : Form
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.GetType() == typeof(T))
                {
                    return openForm;
                }
            }
            return null;
        }

        private void chkCollectionBonousDateDifinition_CheckedChanged(object sender, EventArgs e)
        {

            if (chkCollectionBonousDateDifinition.Checked)
            {
                GracePeriodDefinition form = (GracePeriodDefinition)GetOpenedForm<GracePeriodDefinition>();
                if (form == null)
                {
                    _hpGracePeriodDefinition = new GracePeriodDefinition();
                    _hpGracePeriodDefinition.MdiParent = (Form)this.Parent.Parent;
                    _hpGracePeriodDefinition.Show();
                }
                else
                {
                    if (_hpGracePeriodDefinition.WindowState == FormWindowState.Minimized)
                        _hpGracePeriodDefinition.WindowState = FormWindowState.Normal;
                    _hpGracePeriodDefinition.Focus();
                    _hpGracePeriodDefinition.BringToFront();
                }
            }
        }

        private void chkManagerCollection_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManagerCollection.Checked)
            {
                ManagerCreation form = (ManagerCreation)GetOpenedForm<ManagerCreation>();
                if (form == null)
                {
                    _hpManagerCreation = new ManagerCreation();
                    _hpManagerCreation.MdiParent = (Form)this.Parent.Parent;
                    _hpManagerCreation.Show();
                }
                else
                {
                    if (_hpManagerCreation.WindowState == FormWindowState.Minimized)
                        _hpManagerCreation.WindowState = FormWindowState.Normal;
                    _hpManagerCreation.Focus();
                    _hpManagerCreation.BringToFront();
                }
            }
        }

        private void chkAmountDisregard_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAmountDisregard.Checked) {
                DisregardAmount form = (DisregardAmount)GetOpenedForm<DisregardAmount>();
                if (form == null)
                {
                    _hpDisregardAmount = new DisregardAmount();
                    _hpDisregardAmount.MdiParent = (Form)this.Parent.Parent;
                    _hpDisregardAmount.Show();
                }
                else {
                    if (_hpDisregardAmount.WindowState == FormWindowState.Minimized)
                        _hpDisregardAmount.WindowState = FormWindowState.Normal;
                    _hpDisregardAmount.Focus();
                    _hpDisregardAmount.BringToFront();
                }
            }
        }

        private void chkSalesTarget_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void CollectionBonus_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_hpCollectionBonusDefinition != null)
                _hpCollectionBonusDefinition.Close();
            if (_hpGracePeriodDefinition != null)
                _hpGracePeriodDefinition.Close();
            if (_hpManagerCreation != null)
                _hpManagerCreation.Close();
            if (_hpCollectionBonusAdjusment != null)
                _hpCollectionBonusAdjusment.Close();
            if (_hpDisqulifiedBonus != null)
                _hpDisqulifiedBonus.Close();
            if (_hpDisregardAmount != null)
                _hpDisregardAmount.Close();
            if (_hpHandlingOverAccounts != null)
                _hpHandlingOverAccounts.Close();
        }

        private void chkCollectionBonusAdjusment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCollectionBonusAdjusment.Checked)
            {
                CollectionBonusAdjusment form = (CollectionBonusAdjusment)GetOpenedForm<CollectionBonusAdjusment>();
                if (form == null)
                {
                    _hpCollectionBonusAdjusment = new CollectionBonusAdjusment();
                    _hpCollectionBonusAdjusment.MdiParent = (Form)this.Parent.Parent;
                    _hpCollectionBonusAdjusment.Show();
                }
                else
                {
                    if (_hpCollectionBonusAdjusment.WindowState == FormWindowState.Minimized)
                    {
                        _hpCollectionBonusAdjusment.WindowState = FormWindowState.Normal;
                    }
                    _hpCollectionBonusAdjusment.Focus();
                    _hpCollectionBonusAdjusment.BringToFront();
                }
            }
        }

        private void chkDisqulifiedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisqulifiedAmount.Checked) {
                DisqulifiedBonus form = (DisqulifiedBonus)GetOpenedForm<DisqulifiedBonus>();
                if (form == null)
                {
                    _hpDisqulifiedBonus = new DisqulifiedBonus();
                    _hpDisqulifiedBonus.MdiParent = (Form)this.Parent.Parent;
                    _hpDisqulifiedBonus.Show();
                }
                else {
                    if (_hpDisqulifiedBonus.WindowState == FormWindowState.Minimized)
                    {
                        _hpDisqulifiedBonus.WindowState = FormWindowState.Normal;
                    }
                    _hpDisqulifiedBonus.Focus();
                    _hpDisqulifiedBonus.BringToFront();
                }
            }
        }

        private void chkHandlingOverAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHandlingOverAccount.Checked) {
                HandlingOverAccounts form = (HandlingOverAccounts)GetOpenedForm<HandlingOverAccounts>();
                if (form == null)
                {
                    _hpHandlingOverAccounts = new HandlingOverAccounts();
                    _hpHandlingOverAccounts.MdiParent = (Form)this.Parent.Parent;
                    _hpHandlingOverAccounts.Show();
                }
                else
                {
                    if (_hpHandlingOverAccounts.WindowState == FormWindowState.Minimized)
                    {
                        _hpHandlingOverAccounts.WindowState = FormWindowState.Normal;
                    }
                    _hpHandlingOverAccounts.Focus();
                    _hpHandlingOverAccounts.BringToFront();


                    //hpr_disr_val_ref
                    //hpr_disr_pc_defn
                    //hpr_hand_over_ac
                }
            }
        }

        private void CollectionBonus_Load(object sender, EventArgs e)
        {
            this.ActiveControl = chkCollectionBonusDefinition;
        }
    }
}
