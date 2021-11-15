using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Drawing.Imaging;

namespace FF.WindowsERPClient.Services
{
    public partial class ImageUpload : Base
    {
        private List<ImageUploadDTO> oMainList = new List<ImageUploadDTO>();
        private Int32 gblJObLine = 0;
        private string GblJobNum = string.Empty;
        private string GblSerNum = string.Empty;
        private Int32 GblIsNewJob = 0;
        private bool isStart = true;
        private string fileNmae;
        private Int32 _imgLine = 0;
        List<ImageUploadDTO> oItemsGetting = null;

        private Service_Chanal_parameter _scvParam = null;

        public void setUploadJobImgList(List<ImageUploadDTO> _list)
        {
            oMainList = _list;
        }


        public ImageUpload(String Jobnumber, Int32 jobLine,string serialno,Int32 _isNewJob)
        {
            InitializeComponent();

            dgvFiles.AutoGenerateColumns = false;
            dgvJobDetails.AutoGenerateColumns = false;

            GblJobNum = Jobnumber;
            GblSerNum = serialno;   //kapila 16/2/2016
            gblJObLine = jobLine;
            GblIsNewJob = _isNewJob;    //kapila 17/2/2016
            txtJobNo.Text = GblJobNum;

            dgvFiles.DataSource = new List<ImageUploadDTO>();
            dgvFiles.DataSource = oMainList;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP:
                    {
                        if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10816))
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator + "GET_ALL_JOBS" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "3,6,4,5.1" + seperator + BaseCls.GlbUserID + seperator);
                            break;
                        }
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void ImageUpload_Load(object sender, EventArgs e)
        {
            clearAll();
            btnView_Click(null, null);
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearchFile_Click(null, null);
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                string job = txtJobNo.Text;
                clearAll();
                txtJobNo.Text = job;
                getJobJetails();
            }
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick(null, null);
        }

        private void getJobJetails()
        {
            DataTable DtDetails = new DataTable();
            string stage = string.Empty;
            Int32 IsCusExpected = 0;

            stage = "3,2,6,4,5.1,5";

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10816))
            {
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, "GET_ALL_JOBS");
            }
            else
            {
                DtDetails = CHNLSVC.CustService.GetJObsFOrWIP(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf, BaseCls.GlbUserID);
            }

            if (DtDetails.Rows.Count > 0)
            {
                DataView dv = DtDetails.DefaultView;
                dv.Sort = "jbd_jobline";
                DataTable sortedDT = dv.ToTable();
                dgvJobDetails.DataSource = sortedDT;

                if (sortedDT.Rows.Count == 1)
                {
                    dgvJobDetails_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                }
            }
            else
            {
                MessageBox.Show("Please enter valid job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            clearAll();
        }

        private void clearAll()
        {
            dgvJobDetails.DataSource = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1800"), Convert.ToDateTime("01-01-1800"), txtJobNo.Text, "0", 0, string.Empty, BaseCls.GlbUserDefProf);
            txtFilePath.Clear();
            pictureBox1.Image = null;
            dgvFiles.DataSource = new List<ImageUploadDTO>();
            _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_scvParam == null)
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)");
                this.Enabled = false;
            }
            else
            {
                _scvParam.SP_DOC_SIZE = (_scvParam.SP_DOC_SIZE == "") ? "2" : _scvParam.SP_DOC_SIZE;

                txtSavePath.Text = _scvParam.SP_DOC_SAVE_PATH;
                txtFileSize.Text = _scvParam.SP_DOC_SIZE + " MB";
            }
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image file";
            theDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(theDialog.FileName);
                txtFilePath.Text = theDialog.FileName;
            }
        }

        private void dgvJobDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                for (int i = 0; i < dgvJobDetails.Rows.Count; i++)
                {
                    dgvJobDetails.Rows[i].Cells["select"].Value = false;
                }
                dgvJobDetails.Rows[e.RowIndex].Cells["select"].Value = true;

                gblJObLine = Convert.ToInt32(dgvJobDetails.Rows[e.RowIndex].Cells["JobLine"].Value);
            }
        }

        private void dgvJobDetails_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        public static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));
            gfx.Dispose();
            return bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
                pictureBox2.Refresh();
            }
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox2.Refresh();
            }
        }

        private void btnZin_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox2.Image != null)
                {
                    Image tempImage = pictureBox1.Image;
                    Size newSize = new Size((int)(pictureBox1.Image.Width + 20), (int)(pictureBox1.Image.Height + 20));
                    pictureBox1.Image = new Bitmap(tempImage, newSize);
                    pictureBox1.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnZOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox2.Image != null)
                {
                    Image tempImage = pictureBox1.Image;
                    Size newSize = new Size((int)(pictureBox1.Image.Width - 20), (int)(pictureBox1.Image.Height - 20));
                    pictureBox1.Image = new Bitmap(tempImage, newSize);
                    pictureBox1.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            //comented kapila on 17/2/2016
            //if (String.IsNullOrEmpty(txtJobNo.Text))
            //{
            //    MessageBox.Show("Please enter job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtJobNo.Focus();
            //    return;
            //}
            //if (gblJObLine == 0)
            //{
            //    MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvJobDetails.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select image to upload", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFilePath.Focus();
                return;
            }

            FileInfo oFileInfo = new FileInfo(txtFilePath.Text);
            //need to verify the file size

            _scvParam.SP_DOC_SIZE = (_scvParam.SP_DOC_SIZE == "") ? "2" : _scvParam.SP_DOC_SIZE;

            if (GetFileSize(oFileInfo.Length) >= Convert.ToDouble(_scvParam.SP_DOC_SIZE))
            {
                MessageBox.Show("Slected image file size is too large.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (oMainList.FindAll(x => x.ImagePath == txtFilePath.Text).Count > 0)
            {
                MessageBox.Show("This image is already added.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFilePath.Clear();
                return;
            }

            ImageUploadDTO oItem = new ImageUploadDTO();
            oItem.ImagePath = txtFilePath.Text.Trim();
            oItem.JobLine = gblJObLine;
            oItem.JobNumber = txtJobNo.Text.Trim();
            oItem.image = ReadFile(txtFilePath.Text.Trim());
            oItem.FileName = oFileInfo.Name;
            oItem.SerialNo = GblSerNum;
            _imgLine = _imgLine + 1;
            oItem.ImageLine = _imgLine;
            oMainList.Add(oItem);

            dgvFiles.DataSource = new List<ImageUploadDTO>();
            dgvFiles.DataSource = oMainList;

            txtFilePath.Clear();
            if (MessageBox.Show("Do you want to add new item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                btnSearchFile_Click(null, null);
            }
            else
            {
                toolStrip1.Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFiles.SelectedRows.Count > 1)
                {
                    string imgPath = dgvFiles.SelectedRows[0].Cells["ImagePathD2"].Value.ToString();
                    pictureBox2.Image = new Bitmap(imgPath);
                }
                else
                {
                    if (dgvFiles.Rows.Count == 1)
                    {
                        string imgPath = dgvFiles.Rows[0].Cells["ImagePathD2"].Value.ToString();
                        pictureBox2.Image = new Bitmap(imgPath);
                    }
                }
                if (pictureBox2.Image != null)
                {
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox2.Refresh();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void dgvFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                try
                {
                    string imgPath = dgvFiles.Rows[e.RowIndex].Cells["ImagePathD2"].Value.ToString();
                    panel1.AutoScroll = true;
                    pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;

                    ImageUploadDTO oImage = oItemsGetting.Find(x => x.ImagePath == imgPath);

                    if (File.Exists(imgPath))
                    {
                        pictureBox2.Image = new Bitmap(imgPath);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pictureBox2.Image = ByteToImage(oImage.image);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    String[] srr = imgPath.Split('\\');
                    fileNmae = srr[srr.Length - 1];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtJobNo.Text) && GblIsNewJob==1)
            {
                JobEntry _frmJob = new JobEntry();
                _frmJob.setUploadImgList(oMainList);
                this.Close();
            }
            else
            {
                if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    string err;
                    int result = CHNLSVC.CustService.ServiceSaveImages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, oMainList, out err);
                    if (result > -1)
                    {
                        MessageBox.Show("Images saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();
                        return;
                    }
                    else
                    {
                        if (result == -1)
                        {
                            MessageBox.Show(err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        MessageBox.Show("Error Occurred" + "\n" + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            txtFilePath.Clear();
            pictureBox1.Image = null;
            string err;
            ImageUploadDTO Input = new ImageUploadDTO();
            Input.JobNumber = txtJobNo.Text;
            Input.JobLine = gblJObLine;

            oItemsGetting = CHNLSVC.CustService.GetImages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, Input, out err);
            dgvFiles.DataSource = new List<ImageUploadDTO>();
            dgvFiles.DataSource = oItemsGetting;

            if (oItemsGetting.Count == 0)
            {
                if (isStart == false)
                {
                    MessageBox.Show("No images to view", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isStart = false;
                }

                txtFilePath.Clear();
                pictureBox1.Image = null;

                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (pictureBox2.SizeMode == PictureBoxSizeMode.AutoSize)
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            else if (pictureBox2.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.Normal;
            }
            else if (pictureBox2.SizeMode == PictureBoxSizeMode.Normal)
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (pictureBox2.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (pictureBox2.SizeMode == PictureBoxSizeMode.Zoom)
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            pictureBox2.Refresh();
        }

        private void btnLocalCopy_Click(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10820))
            //{
            //    MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10820", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //try
            //{
            //    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //    saveFileDialog1.InitialDirectory = @"C:\";
            //    saveFileDialog1.Title = "Save text Files";
            //    saveFileDialog1.DefaultExt = "txt";
            //    saveFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            //    saveFileDialog1.FilterIndex = 2;
            //    saveFileDialog1.RestoreDirectory = true;
            //    saveFileDialog1.FileName = fileNmae;

            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        //Image oImage = pictureBox2.Image;
            //        //oImage.Save(saveFileDialog1.FileName);
            //        pictureBox2.Image.Save(saveFileDialog1.FileName,ImageFormat.Jpeg);
            //        MessageBox.Show("Image saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        string imgPath = dgvFiles.SelectedRows[0].Cells["ImagePathD2"].Value.ToString();
            //        ImageUploadDTO oImageUploadDTO1 = oItemsGetting.Find(x => x.ImagePath == imgPath);

            //        //Bitmap b = new Bitmap(pictureBox2.Image);
            //        //b.Save(fileNmae);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10820))
            {
                MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10820", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = fileNmae;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string imgPath = dgvFiles.SelectedRows[0].Cells["ImagePathD2"].Value.ToString();

                    ImageUploadDTO oImage = oItemsGetting.Find(x => x.ImagePath == imgPath);

                    saveImage2(saveFileDialog1.FileName, ByteToImage(oImage.image));
                    //ExportToBmp(saveFileDialog1.FileName, pictureBox2.Image.Width, pictureBox2.Image.Height);
                    ////////////////////
                    MessageBox.Show("Image saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10820))
            {
                MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10820", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Title = "Save text Files";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = fileNmae;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string imgPath = dgvFiles.SelectedRows[0].Cells["ImagePathD2"].Value.ToString();

                    ImageUploadDTO oImage = oItemsGetting.Find(x => x.ImagePath == imgPath);

                    saveImage2(saveFileDialog1.FileName, ByteToImage(oImage.image));
                    //ExportToBmp(saveFileDialog1.FileName, pictureBox2.Image.Width, pictureBox2.Image.Height);
                    ////////////////////
                    MessageBox.Show("Image saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ExportToBmp(string path, Int32 width, Int32 heidth)
        {
            using (var bitmap = new Bitmap(width, heidth))
            {
                Rectangle oRect = new Rectangle(0, 0, width, heidth);

                pictureBox2.DrawToBitmap(bitmap, oRect);
                ImageFormat imageFormat = null;

                var extension = Path.GetExtension(path);
                switch (extension)
                {
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".jpeg":
                    case ".JPG":
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        imageFormat = ImageFormat.Gif;
                        break;
                    default:
                        throw new NotSupportedException("File extension is not supported");
                }

                bitmap.Save(path, imageFormat);
            }
        }

        private void saveImage2(string path, Image img)
        {
            //            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream);
            System.Drawing.Image imageToBeResized = img;
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            //int maxHeight = 240;
            //int maxWidth = 320;
            //imageHeight = (imageHeight * maxWidth) / imageWidth;
            //imageWidth = maxWidth;

            //if (imageHeight > maxHeight)
            //{
            //    imageWidth = (imageWidth * maxHeight) / imageHeight;
            //    imageHeight = maxHeight;
            //}

            Bitmap bitmap = new Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            byte[] image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            fs.Write(image, 0, image.Length);
        }

        private double GetFileSize(double bytes)
        {
            const int byteConversion = 1024;
            return Math.Round(bytes / Math.Pow(byteConversion, 2), 2);
        }
    }
}