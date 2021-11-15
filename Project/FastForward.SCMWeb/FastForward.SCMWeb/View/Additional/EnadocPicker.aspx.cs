using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System.Security.Cryptography;
using System.Configuration;
namespace FastForward.SCMWeb.View.Additional
{
    public partial class EnadocPicker : BasePage
    {
        
        private string UserID;
        private string UserCompanyCode;
        private string UserDefLoca;
        private string UserDefProf;
        static string key  = "A!9HHhi%XjjYY4YP2@Nob009X";
        public string EnadocUrl{ get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EnadocUrl = ConfigurationManager.AppSettings["EnadocUrl"];
                UserID = Session["UserID"].ToString();
                UserCompanyCode = Session["UserCompanyCode"].ToString();
                UserDefLoca = Session["UserDefLoca"].ToString();
                UserDefProf = Session["UserDefProf"].ToString();
                string text = "" + UserID + " " + UserCompanyCode + " " + UserDefLoca + " " + UserDefProf + "";

                var cipher = Encrypt(text);


                try
                {
                    if (!IsPostBack)
                    {
                        string _errString = string.Empty;
                        //Generated the Enadoc Hash Key = UserID+Company+LocationCode+ProfitCenterCode+DateTime
                        //Save it in sec_user table
                        //

                        var effect = CHNLSVC.CommonSearch.SetEnadocHash(cipher, UserID);
                        Response.Write("<script>");
                        Response.Write("window.open('" + EnadocUrl + "/GetLibrary?hash=" + cipher + "','_blank')");
                        Response.Write("</script>");

                        //Response.Redirect("~/View/ADMIN/Home.aspx");

                    }
                }
                catch (Exception err)
                {
                    Response.Redirect("~/Error.aspx?Error=" + err.Message.ToString() + "");
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;
                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }


        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAsk.Visible = false;
        }

    }
}