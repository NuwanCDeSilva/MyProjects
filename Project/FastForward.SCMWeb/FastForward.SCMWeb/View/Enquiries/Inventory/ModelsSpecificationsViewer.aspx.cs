using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class ModelsSpecificationsViewer : BasePage
    {
        protected List<ModelCatAndTypes> _modelDetails { get { return (List<ModelCatAndTypes>)Session["_modelDetails"]; } set { Session["_modelDetails"] = value; } }
        protected List<ModelPic> _ModelPic { get { return (List<ModelPic>)Session["_ModelPic"]; } set { Session["_ModelPic"] = value; } }
        protected List<VideoDetail> _VideoDetail { get { return (List<VideoDetail>)Session["_VideoDetail"]; } set { Session["_VideoDetail"] = value; } }

        protected List<ModelClsDef> _ModelClsDef { get { return (List<ModelClsDef>)Session["_ModelClsDef"]; } set { Session["_ModelClsDef"] = value; } }
        protected String virtualPath { get { return (String)Session["virtualPath"]; } set { Session["virtualPath"] = value; } }
        protected String physicalPath { get { return (String)Session["physicalPath"]; } set { Session["physicalPath"] = value; } }
        protected int imagenumber { get { return (int)Session["imagenumber"]; } set { Session["imagenumber"] = value; } }

        protected String soucePath { get { return (String)Session["soucePath"]; } set { Session["soucePath"] = value; } }

        /// <summary>
        /// Relative path to the Model Folder
        /// </summary>
        public string ImageFolderPath { get; set; }

        /// <summary>
        /// Title to be displayed on top of Model
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Get or Set the Admin Mode 
        /// </summary>
        public bool AdminMode { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //string FolderPath = ConfigurationManager.AppSettings["ModelImage"];
            //FileInfo _path = new FileInfo(FolderPath);
            //string FilePath = Server.MapPath(FolderPath);
            // string a = HttpContext.Current.Server.MapPath("~/c:/Model/");
            //string[] filePaths = Directory.GetFiles(@"C:\Model");
            //List<ListItem> files = new List<ListItem>();
            //foreach (string filePath in filePaths)
            //{
            //     string fileName = Path.GetFileName(filePath);     
            //     files.Add(new ListItem(fileName, @"C:/Model/" + fileName));
            //}
            //DataList1.DataSource = files;
            //DataList1.DataBind();
            if (!IsPostBack)
            {
                //Update the path
                UpdatePath();
                Clear();

            }

        }

        #region Modalpopup
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Model")
            {
                txtModel.Text = ID;
                lblvalue.Text = "";
                ModelDeatils();
                GetmodelDeatils(txtModel.Text, lblMainCat.Text);
                BindData();
                CopyFiles("@C:\\Model", physicalPath, "007418000[1.png");
                string fileName = txtModel.Text.Trim() + ".pdf";
                FileInfo info = new FileInfo("~/Temp/Model/PDF/" + fileName);
                if (info.Exists == false && Session["isfileexists"] == null)
                {
                    Session["isfileexists"] = txtModel.Text.Trim();
                    Response.Redirect(Request.RawUrl, false);
                }
                else
                {
                    Session["isfileexists"] = null;
                }
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)Session["SEARCH"];
                grdResult.DataBind();
                UserPopoup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        private void FilterData()
        {
            if (lblvalue.Text == "Model")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                Session["SEARCH"] = _result;
                UserPopoup.Show();
                return;
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private void DisplayMessage(String Msg, Int32 option)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);

            }

        }
        #endregion

        private void Clear()
        {
            _modelDetails = new List<ModelCatAndTypes>();
            imagenumber = 0;
            _ModelClsDef = new List<ModelClsDef>();
            _ModelPic = new List<ModelPic>();
            _VideoDetail = new List<VideoDetail>();
            txtModel.Text = string.Empty;
            lblCat1.Text = string.Empty;
            lblCat2.Text = string.Empty;
            lblCat3.Text = string.Empty;
            lblCat4.Text = string.Empty;
            lbldes.Text = string.Empty;
            lblMainCat.Text = string.Empty;
            DataList1.DataSource = new int[] { };
            DataList1.DataBind();
            grvideo.DataSource = new int[] { };
            grvideo.DataBind();
            txtUrl.Text = string.Empty;
            if (Session["isfileexists"] != null)
            {
                txtModel.Text = Session["isfileexists"].ToString();
                txtModel_TextChanged(null,null);
            }
            Session["Inactive"] = null;

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Binds the Data Before Rendering

            BindData();
            // View(null, null);
        }


        private void UpdatePath()
        {
            //use a default path
            virtualPath = "~/Temp/Model";
            physicalPath = Server.MapPath(virtualPath);

            //If ImageFolderPath is specified then use that path
            if (!string.IsNullOrEmpty(ImageFolderPath))
            {
                physicalPath = Server.MapPath(ImageFolderPath);
                virtualPath = ImageFolderPath;
            }

            soucePath = "@C:/Model";


        }

        /// <summary>
        /// Binds the ImageListView to current DataSource
        /// </summary>
        private void BindData()
        {
            ImageListView.DataSource = GetListOfImages(txtModel.Text);
            ImageListView.DataBind();

        }

        /// <summary>
        /// Gets list of Model
        /// </summary>
        /// <returns></returns>
        private List<string> GetListOfImages(string _MainCate)
        {
            var Model = new List<string>();

            try
            {
                var imagesFolder = new DirectoryInfo(physicalPath);
                foreach (var item in imagesFolder.EnumerateFiles())
                {
                    if (item is FileInfo)
                    {

                        string[] tokens = item.Name.Split('[');
                        string _modelname = tokens[0].ToString();
                        if (_modelname == _MainCate)
                        {
                            //add virtual path of the image to the Model list
                            Model.Add(string.Format("{0}/{1}", virtualPath, item.Name));
                            string[] tokens2 = tokens[1].ToString().Split('.');
                            imagenumber = Convert.ToInt32(tokens2[0].ToString());

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //log exception

            }

            return Model;

        }

        protected void titleLabel_Load(object sender, EventArgs e)
        {
            //var titleLabel = sender as Label;
            //if (titleLabel == null) return;

            //titleLabel.Text = Title;
        }

        /// <summary>
        /// Enables delete functionality for Admin Mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void deleteLinkButton_Load(object sender, EventArgs e)
        {
            //In case of AdminMode, we would want to show the delete button 

            //which is not visible by iteself for Non-Admin users
            if (AdminMode)
            {
                var deleteButton = sender as LinkButton;
                if (deleteButton == null) return;

                deleteButton.Visible = true;
            }

        }

        /// <summary>
        /// Redirects to the full image when the image is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void itemImageButton_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect(e.CommandArgument as string);
        }

        protected void ImageListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Remove":

                    if (txtDeleteconformmessageValue.Value == "0")
                    {
                        return;
                    }
                    var path = e.CommandArgument as string;
                    if (path != null)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(Server.MapPath(path));
                            fi.Delete();

                            //Display message
                            //Parent.Controls.Add(new Label()
                            //{

                            //    Text = GetFileName(path) + " deleted successfully!"

                            //});

                            GetListOfImages(txtModel.Text);
                        }
                        catch (Exception ex)
                        {
                            // Logger.Log(ex.Message);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Saves the Posted File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imageUpload_Load(object sender, EventArgs e)
        {
            ////Get the required controls
            //var imageUpload = sender as FileUpload;
            //if (imageUpload == null) return;

            //var parent = imageUpload.Parent;
            //if (parent == null) return;

            //var imageUploadStatus = parent.FindControl("imageUploadStatusLabel") as Label;
            //if (imageUploadStatus == null) return;


            ////If a file is posted, save it
            //if (this.IsPostBack)
            //{
            //    if (imageUpload.PostedFile != null && imageUpload.PostedFile.ContentLength > 0)
            //    {
            //        try
            //        {
            //            imageUpload.PostedFile.SaveAs(string.Format("{0}\\{1}",

            //                physicalPath, GetFileName(imageUpload.PostedFile.FileName)));
            //            imageUploadStatus.Text = string.Format(

            //                "Image {0} successfully uploaded!",

            //                imageUpload.PostedFile.FileName);
            //        }
            //        catch (Exception ex)
            //        {
            //            //Logger.Log(ex.Message);
            //            imageUploadStatus.Text = string.Format("Error uploading {0}!",

            //                imageUpload.PostedFile.FileName);
            //        }
            //    }
            //    else
            //    {
            //        imageUploadStatus.Text = string.Empty;
            //    }

            //}

        }

        private string GetFileName(string path)
        {
            DateTime timestamp = DateTime.Now;
            string fileName = string.Empty;
            try
            {
                if (path.Contains('\\')) fileName = path.Split('\\').Last();
                if (path.Contains('/')) fileName = path.Split('/').Last();
            }
            catch (Exception ex)
            {
                //Logger.Log(ex.Message);
            }
            return fileName;
        }


        //protected void Upload(object sender, EventArgs e)
        //{
        //    if (FileUpload.HasFile)
        //    {


        //        try
        //        {
        //            string strpath = System.IO.Path.GetExtension(FileUpload.FileName);
        //            if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".gif" && strpath != ".png")
        //            {
        //                DisplayMessage("Please select a model", 1);
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Only image formats (jpg, png, gif) are accepted ');", true);
        //                UserPopoup.Hide();
        //                return;
        //            }
        //            // string fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
        //            if (string.IsNullOrEmpty(txtModel.Text))
        //            {
        //                DisplayMessage("Please select a model", 1);
        //                return;
        //            }
        //            imagenumber++;
        //            string fileName = txtModel.Text.Trim() + "[" + imagenumber + ".png";
        //            FileUpload.PostedFile.SaveAs(Server.MapPath("~/Temp/Model/") + fileName);
        //            BindData();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Image successfully uploaded');", true);
        //            // Response.Redirect(Request.Url.AbsoluteUri);

        //            //virtualPath = "~/Temp/Model";
        //            //physicalPath = Server.MapPath(virtualPath);
        //            //var imagesFolder = new DirectoryInfo(physicalPath);
        //            //foreach (var item in imagesFolder.EnumerateFiles())
        //            //{
        //            //    if (item is FileInfo)
        //            //    {
        //            //        //add virtual path of the image to the Model list
        //            //        //imgsave.(string.Format("{0}/{1}", virtualPath, item.Name));
        //            //        imgsave.ImageUrl = item.DirectoryName + item.Name;
        //            //    }
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            //log exception

        //        }
        //    }
        //}

        /// <summary>
        /// Model
        /// </summary>
        /// <param name="_model"></param>
        private void GetmodelDeatils(string _model, string _maincat)
        {
            List<ModelPic> _picDetails = new List<ModelPic>();
            videoItem.Visible = false;
            grvideo.DataSource = new int[] { };
            grvideo.DataBind();
            _modelDetails = CHNLSVC.Inventory.Get_Model_cat_Type(_model, _maincat, out _picDetails);

            _ModelPic = _picDetails;
            List<Detail> _name = new List<Detail>();
            var value = _modelDetails.GroupBy(x => new { x.MCT_CLS_CAT, x.MCT_CLS_CAT_DES }).Select(group => new { Peo = group.Key, theCount = group.Count() });
            foreach (var _types in value)
            {
                Detail _obj = new Detail();
                _obj.Id = _types.Peo.MCT_CLS_CAT;
                _obj.Name = _types.Peo.MCT_CLS_CAT_DES;
                _name.Add(_obj);
            }

            if (_ModelPic.Count > 0)
            {
                var _FilterVideo = _ModelPic.Where(x => x.MMP_TP == "VIDEO").ToList();
                if (_FilterVideo.Count > 0)
                {
                    grvideo.DataSource = _FilterVideo;
                    grvideo.DataBind();
                    //foreach (ModelPic _video in _FilterVideo)
                    //{
                    //    txtUrl.Text = _video.MMP_PATH;
                    //}
                }
                else
                {
                    txtUrl.Text = string.Empty; ;
                }
            }
            else
            {
                txtUrl.Text = string.Empty; ;
            }
            DataList1.DataSource = _name;
            DataList1.DataBind();


        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, "Code", txtModel.Text.ToString());
            var newDataTable = _result.AsEnumerable()
                .Where(r => r.Field<string>("Code") == txtModel.Text.ToString()).Count();
            string fileName = txtModel.Text.Trim() + ".pdf";
            FileInfo info = new FileInfo("~/Temp/Model/PDF/" + fileName);
            if (newDataTable == 0)
            {
                //DisplayMessage("Please select a valid/active model", 1);
                //txtModel.Text = "";
                //Session["itmcheck"] = null;
               // return;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + "Please select a valid/active model" + "');", true);
                Session["Inactive"] = "true";
            }
            if (info.Exists == false && Session["isfileexists"] ==null)
            {
                Session["isfileexists"] = txtModel.Text.Trim();
                Response.Redirect(Request.RawUrl, false);
            }
            else
            {
                Session["isfileexists"] = null;
               
            }

            if (Session["Inactive"] == null)
            {
                ModelDeatils();
                GetmodelDeatils(txtModel.Text, lblMainCat.Text);
                BindData();
            }
            else
            {
                txtModel.Text = "";
            }
        

            //View(null, null);
        }

        protected void lbtnSearchModel_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                Session["SEARCH"] = _result;
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Model";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label catId = e.Item.FindControl("lblCategory") as Label;
            DataList List2 = e.Item.FindControl("DataList2") as DataList;
            DataList List3 = e.Item.FindControl("DataList3") as DataList;
            List<ModelPic> _Pic = new List<ModelPic>();
            _modelDetails = CHNLSVC.Inventory.Get_Model_cat_Type(txtModel.Text, lblMainCat.Text.Trim(), out _Pic);
            //tbl2
            _modelDetails = _modelDetails.Where(x => x.MCT_CLS_CAT == catId.Text).ToList();
            List2.DataSource = _modelDetails;
            List2.DataBind();
            List3.DataSource = _modelDetails;
            List3.DataBind();
        }



        public void ModelDeatils()
        {

            MasterItemModel _model = new MasterItemModel();
            List<MasterItemModel> listmodl = new List<MasterItemModel>();
          

            listmodl = CHNLSVC.General.GetItemModel(txtModel.Text);

            if (listmodl != null)
            {
                _model = listmodl.FirstOrDefault();
                lblMainCat.Text = _model.Mm_cat1;
                lblCat1.Text = _model.Mm_cat2;
                lblCat2.Text = _model.Mm_cat3;
                lblCat3.Text = _model.Mm_cat4;
                lblCat4.Text = _model.Mm_cat5;
                lbldes.Text = _model.Mm_desc;

            }
            else
            {
                DisplayMessage("Please select a valid model", 1);
                txtModel.Text = string.Empty;
            }


        }


        private void SaveModelDetails()
        {
            //_ModelPic = new List<ModelPic>();
            int Maindatalist = DataList1.Items.Count;
            for (int i = 0; i < Maindatalist; i++)
            {

                Label CID = DataList1.Items[i].FindControl("lblCategory") as Label;

                DataList nestedDataList3 = (DataList)(DataList1.Items[i].FindControl("DataList3"));
                DataList nestedDataList2 = (DataList)(DataList1.Items[i].FindControl("DataList2"));
                int count = nestedDataList3.Items.Count;
                for (int x = 0; x < count; x++)
                {

                    Label CTYID = nestedDataList2.Items[x].FindControl("lblCategorytype") as Label;
                    TextBox Des = nestedDataList3.Items[x].FindControl("txtCategorytypeDes") as TextBox;

                    string mcdclscat = CID.Text;
                    string mcdclstp = CTYID.Text;

                    ModelClsDef _Def = new ModelClsDef();
                    _Def.MCD_MODEL_CD = txtModel.Text.Trim();
                    _Def.MCD_CLS_CAT = mcdclscat;
                    _Def.MCD_CLS_TP = mcdclstp;
                    _Def.MCD_DEF = Des.Text;
                    _Def.MCD_CRE_BY = Session["UserID"].ToString();
                    _Def.MCD_MOD_BY = Session["UserID"].ToString();
                    _ModelClsDef.Add(_Def);
                }


            }
            var imagesFolder = new DirectoryInfo(physicalPath);
            if (imagesFolder == null)
            {
                DisplayMessage("Image Folder path is missing", 3);
                return;
            }
            foreach (var item in imagesFolder.EnumerateFiles())
            {
                if (item is FileInfo)
                {

                    string[] tokens = item.Name.Split('[');
                    string _modelname = tokens[0].ToString();
                    if (_modelname == txtModel.Text)
                    {
                        //add virtual path of the image to the Model list

                        string[] tokens2 = tokens[1].ToString().Split('.');
                        imagenumber = Convert.ToInt32(tokens2[0].ToString());
                        ModelPic _pic = new ModelPic();
                        _pic.MMP_ACT = 1;
                        _pic.MMP_CRE_BY = Session["UserID"].ToString();
                        _pic.MMP_MOD_BY = Session["UserID"].ToString();
                        _pic.MMP_MODEL = txtModel.Text.Trim().ToUpper();
                        _pic.MMP_PATH = imagesFolder + item.Name;
                        _pic.MMP_TP = "PIC";
                        var _filter = _ModelPic.Where(x => x.MMP_PATH == _pic.MMP_PATH).ToList();
                        if (_filter.Count == 0)
                        {
                            _ModelPic.Add(_pic);
                        }

                    }

                }
            }
            // if (!string.IsNullOrEmpty(txtUrl.Text))
            //{
            //if (grvideo.Rows.Count > 0)
            //{

            //    for (int i = 0; i < grvideo.Rows.Count; i++)
            //    {
            //        LinkButton pathlink = grvideo.Rows[i].FindControl("lbtnpath") as LinkButton;                   
            //        string path = pathlink.Text;

            //        var _filter = _ModelPic.Where(x => x.MMP_PATH == path);
            //        if (_filter == null)
            //        {
            //            ModelPic _pic = new ModelPic();
            //            _pic.MMP_ACT = 1;
            //            _pic.MMP_CRE_BY = Session["UserID"].ToString();
            //            _pic.MMP_MOD_BY = Session["UserID"].ToString();
            //            _pic.MMP_MODEL = txtModel.Text.Trim().ToUpper();
            //            _pic.MMP_PATH = path;
            //            _pic.MMP_TP = "VIDEO";
            //            _ModelPic.Add(_pic);
            //        }

            //    }


            //}

            ///}
            //foreach (DataRow _row in grvideo.Rows)
            //{
            //    ModelPic _pic = new ModelPic();
            //    _pic.MMP_ACT = 1;
            //    _pic.MMP_CRE_BY = Session["UserID"].ToString();
            //    _pic.MMP_MOD_BY = Session["UserID"].ToString();
            //    _pic.MMP_MODEL = txtModel.Text.Trim().ToUpper();
            //    _pic.MMP_PATH = _row[1].ToString();
            //    _pic.MMP_TP = "VIDEO";
            //    _ModelPic.Add(_pic);
            //}
            string err = string.Empty;
            Int32 result = CHNLSVC.Inventory.SaveModelClsDef(_ModelClsDef, _ModelPic, out err);
            if (result > 0)
            {
                DisplayMessage("Model Specification Sucessfully Saved", 3);
                Clear();
            }
            else
            {
                DisplayMessage(err, 4);
            }

        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            // try
            //  {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                string _Msg = "Please select a Model";
                DisplayMessage(_Msg, 1);
                return;
            }
            SaveModelDetails();
            // }
            //catch (Exception ex)
            //{
            //    string _Msg = "Error Occurred while processing";
            //    DisplayMessage(_Msg, 4);
            //}
        }


        public void CopyFiles(string sourcePath, string destinationPath, string sFileName)
        {
            try
            {
                string Sfilepthe = sourcePath + "\\" + sFileName;
                string Sdestination = destinationPath + "\\" + sFileName;
                if (File.Exists(Sfilepthe))
                {
                    File.Copy(Sfilepthe, Sdestination, true);
                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //VideoDetail _list = new VideoDetail();
                //_list.path = txtUrl.Text;

                //if (_VideoDetail.Count > 0)
                //{
                //    var _filter = _VideoDetail.Where(x => x.path == _list.path).ToList();
                //    if (_filter.Count > 0)
                //    {
                //        DisplayMessage("Entered Video has been already added ! ", 2);
                //        return;
                //    }
                //    else
                //    {

                //        _VideoDetail.Add(_list);
                //    }

                //}
                //else
                //{
                //    _VideoDetail.Add(_list);
                //}
                if (string.IsNullOrEmpty(txtUrl.Text))
                {
                    string _Msg = "Please enter  URL";
                    DisplayMessage(_Msg, 1);
                    return;
                }
                if (_ModelPic != null)
                {
                    if (_ModelPic.Count > 0)
                    {
                        var _filter = _ModelPic.Where(x => x.MMP_PATH == txtUrl.Text).ToList();
                        if (_filter.Count > 0)
                        {
                            DisplayMessage("Entered Video has been already added ! ", 2);
                            return;
                        }
                        else
                        {
                            ModelPic _pic = new ModelPic();
                            _pic.MMP_ACT = 1;
                            _pic.MMP_CRE_BY = Session["UserID"].ToString();
                            _pic.MMP_MOD_BY = Session["UserID"].ToString();
                            _pic.MMP_MODEL = txtModel.Text.Trim().ToUpper();
                            _pic.MMP_PATH = txtUrl.Text;
                            _pic.MMP_TP = "VIDEO";
                            _ModelPic.Add(_pic);

                        }

                    }
                    else
                    {

                        ModelPic _pic = new ModelPic();
                        _pic.MMP_ACT = 1;
                        _pic.MMP_CRE_BY = Session["UserID"].ToString();
                        _pic.MMP_MOD_BY = Session["UserID"].ToString();
                        _pic.MMP_MODEL = txtModel.Text.Trim().ToUpper();
                        _pic.MMP_PATH = txtUrl.Text;
                        _pic.MMP_TP = "VIDEO";
                        _ModelPic.Add(_pic);
                    }
                }
                grvideo.DataSource = _ModelPic.Where(x => x.MMP_TP == "VIDEO" && x.MMP_ACT == 1).ToList();
                grvideo.DataBind();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnser_Remove_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "0")
            {
                return;
            }
            if (grvideo.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _path = (row.FindControl("lbtnpath") as LinkButton).Text;
                if (_ModelPic != null)
                {
                    var _filter = _ModelPic.SingleOrDefault(x => x.MMP_PATH == _path);
                    if (_filter.MMP_LINE == 0)
                    {
                        _ModelPic.RemoveAll(x => x.MMP_PATH == _path);
                    }
                    else
                    {
                        _filter.MMP_ACT = 0;
                        //_ItemKitComponent.RemoveAll(x => x.MIKC_ACTIVE == 0);
                        //_ItemKitComponent = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                    }

                    grvideo.DataSource = _ModelPic.Where(x => x.MMP_TP == "VIDEO" && x.MMP_ACT == 1).ToList();
                    grvideo.DataBind();
                }
            }
        }

        protected void lbtnPLAY_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "0")
            {
                return;
            }
            if (grvideo.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _path = (row.FindControl("lbtnpath") as LinkButton).Text;
                txtUrl.Text = _path;

                videoItem.Attributes.Add("src", _path);
                videoItem.Visible = true;
            }
        }
        public class Detail
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Details { get; set; }
        }
        public class VideoDetail
        {
            public string Id { get; set; }
            public string path { get; set; }
            public int line { get; set; }
        }

        protected void View(object sender, EventArgs e)
        {
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            string fileName = txtModel.Text.Trim() + ".pdf";
            ltEmbed.Text = string.Format(embed, ResolveUrl("~/Temp/Model/PDF/" + fileName));
        }
        protected void lblUClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    try
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                    //ClearAll();
                    //ClearVariables();
                    //Response.Redirect(Request.RawUrl);
                    //Clear();
                    //DivsHide();
                }
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
            }

        }

        //protected void btnuploadPdf_click(object sender, EventArgs e)
        //{
        //    if (FileUpload1.HasFile)
        //    {


        //        try
        //        {
        //            string strpath = System.IO.Path.GetExtension(FileUpload1.FileName);
        //            if (strpath != ".pdf")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Only PDF formats  are accepted ');", true);

        //                return;
        //            }
        //            // string fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
        //            if (string.IsNullOrEmpty(txtModel.Text))
        //            {
        //                DisplayMessage("Please select a model", 1);
        //                return;
        //            }
        //            string fileName = txtModel.Text.Trim() + ".pdf";
        //            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Temp/Model/PDF/") + fileName);

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('PDF successfully uploaded');", true);
        //            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('upload completed');", true);



        //        }
        //        catch (Exception ex)
        //        {
        //            //log exception

        //        }
        //    }
        //}
    }
}