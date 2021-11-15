using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.UserControls
{
    public partial class uc_MsgInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        public void SetMessage(CommonUIDefiniton.MessageType _msgType, string _meassageText)
        {
            this.Visible = true;
            lblmsg.Text = _meassageText;
            imgMsg.Height = 28;
            imgMsg.Width = 28;
            switch (_msgType)
            {
                case CommonUIDefiniton.MessageType.Error:
                    {
                        imgMsg.ImageUrl = "~/Images/error_icon.png";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        break;
                    }
                case CommonUIDefiniton.MessageType.warning:
                    {
                        imgMsg.ImageUrl = "~/Images/warning.gif";

                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        break;
                    }
                case CommonUIDefiniton.MessageType.Information:
                    {
                        imgMsg.ImageUrl = "~/Images/information.png";
                        lblmsg.ForeColor = System.Drawing.Color.Blue;
                        break;
                    }
                case CommonUIDefiniton.MessageType.Critical:
                    {
                        imgMsg.ImageUrl = "";
                        break;
                    }
                default:
                    break;
            }
        }

        public void Clear()
        {
            imgMsg.ImageUrl = string.Empty;
            lblmsg.Text = string.Empty;

        }


    }
}