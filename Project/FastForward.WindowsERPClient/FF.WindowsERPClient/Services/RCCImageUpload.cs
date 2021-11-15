using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using FF.BusinessObjects;
using System.IO;

namespace FF.WindowsERPClient.Services
{
    public partial class RCCImageUpload : Base
    {
        List<ImageUploadDTO> oMainList = new List<ImageUploadDTO>();
        private Int32 gblJObLine = 0;
        private string GblJobNum = string.Empty;
        private bool isStart = true;
        private string GblSerLoc = "";

        private Service_Chanal_parameter _scvParam = null;

        public RCCImageUpload(String Jobnumber, Int32 jobLine,string SerLoc)
        {
            InitializeComponent();

            dgvFiles.AutoGenerateColumns = false;
            dgvJobDetails.AutoGenerateColumns = false;

            GblJobNum = Jobnumber;
            gblJObLine = jobLine;
            txtJobNo.Text = GblJobNum;
            GblSerLoc = SerLoc;
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
                default:
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
            //DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, Convert.ToDateTime("01-01-1911"), Convert.ToDateTime("31-12-2999"), txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);

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
            //txtJobNo.Clear();
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
            pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipY);
            pictureBox2.Refresh();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            pictureBox2.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox2.Refresh();
        }

        private void btnZin_Click(object sender, EventArgs e)
        {
            Image tempImage = pictureBox1.Image;
            Size newSize = new Size((int)(pictureBox1.Image.Width + 20), (int)(pictureBox1.Image.Height + 20));
            pictureBox1.Image = new Bitmap(tempImage, newSize);
            pictureBox1.Refresh();
        }

        private void btnZOut_Click(object sender, EventArgs e)
        {
            Image tempImage = pictureBox1.Image;
            Size newSize = new Size((int)(pictureBox1.Image.Width - 20), (int)(pictureBox1.Image.Height - 20));
            pictureBox1.Image = new Bitmap(tempImage, newSize);
            pictureBox1.Refresh();

        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtJobNo.Text))
            {
                MessageBox.Show("Please enter job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJobNo.Focus();
                return;
            }
            if (gblJObLine == 0)
            {
                MessageBox.Show("Please select a job item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvJobDetails.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select image to upload", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFilePath.Focus();
                return;
            }

            FileInfo oFileInfo = new FileInfo(txtFilePath.Text);
            //need to verify the file size


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
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.Refresh();
        }

        private void dgvFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string imgPath = dgvFiles.Rows[e.RowIndex].Cells["ImagePathD2"].Value.ToString();
                panel1.AutoScroll = true;
                pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox2.Image = new Bitmap(imgPath);
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
            if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string err;
                int result = CHNLSVC.CustService.ServiceSaveImages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, GblSerLoc, oMainList, out err);
                if (result > -1)
                {
                    MessageBox.Show("Images saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearAll();
                    return;
                }
                else
                {
                    MessageBox.Show("Error Occurred" + "\n" + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
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

            List<ImageUploadDTO> oItems = CHNLSVC.CustService.GetImages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, GblSerLoc, Input, out err);
            dgvFiles.DataSource = new List<ImageUploadDTO>();
            dgvFiles.DataSource = oItems;

            if (oItems.Count == 0)
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
    }
}