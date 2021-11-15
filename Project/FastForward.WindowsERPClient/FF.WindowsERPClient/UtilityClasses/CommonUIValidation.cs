using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.UtilityClasses
{
    public static class CommonUIValidation
    {
        public enum validationType
        {
            Required,
            Clear,
            Enable,
            Disable,
            Visible,
            Hide
        }

        // Textbox
        public static bool TextBoxvalidation(TextBox[] txt, validationType type)
        {
            bool result = true;

            // Required
            if (type == validationType.Required)
            {
                foreach (var itm in txt)
                {
                    if (itm.Text == "")
                    {
                        MessageBox.Show("Field cannot be blank", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                    }
                }
            }

            // Clear
            else if (type == validationType.Clear)
            {
                foreach (var itm in txt)
                {
                    itm.Text = "";
                }
            }

            // Enable
            else if (type == validationType.Enable)
            {
                foreach (var itm in txt)
                {
                    itm.Enabled = true;
                }
            }

            // Disable
            else if (type == validationType.Disable)
            {
                foreach (var itm in txt)
                {
                    itm.Enabled = false;
                }
            }

            return result;
        }

        //Label
        public static bool Labelvalidation(Label[] lbl, validationType type)
        {
            bool result = true;

            // Required
            if (type == validationType.Required)
            {
                foreach (var itm in lbl)
                {
                    if (itm.Text == "")
                    {
                        MessageBox.Show("Label cannot be blank", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        result = false;
                    }
                }
            }

            // Clear
            else if (type == validationType.Clear)
            {
                foreach (var itm in lbl)
                {
                    itm.Text = "";
                }
            }

            // Enable
            else if (type == validationType.Enable)
            {
                foreach (var itm in lbl)
                {
                    itm.Enabled = true;
                }
            }

            // Disable
            else if (type == validationType.Disable)
            {
                foreach (var itm in lbl)
                {
                    itm.Enabled = false;
                }
            }

            return result;
        }

        //Button
        public static bool Buttonvalidation(Button[] btn, validationType type)
        {
            bool result = true;

            // Visible
            if (type == validationType.Visible)
            {
                foreach (var itm in btn)
                {
                    itm.Visible = true;
                }
            }

            // Hide
            if (type == validationType.Hide)
            {
                foreach (var itm in btn)
                {
                    itm.Visible = false;
                }
            }

            // Enable
            else if (type == validationType.Enable)
            {
                foreach (var itm in btn)
                {
                    itm.Enabled = true;
                }
            }

            // Disable
            else if (type == validationType.Disable)
            {
                foreach (var itm in btn)
                {
                    itm.Enabled = false;
                }
            }

            return result;
        }

   
    }
}
