using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Ms.Themes
{
    public partial class Add : PageBaseAdmin
    {
        Maticsoft.BLL.Ms.Theme bll=new BLL.Ms.Theme();
        Maticsoft.Model.Ms.Theme model=new Model.Ms.Theme();
        private string ImagePath = "/Upload/Shop/Theme/";  //模版预览图的位置
        private string FilePath = "/Upload/Shop/Theme/";  //模版文件的位置
        private string DedecompressionPath = "Areas/SNS/Themes/";   //压缩后的位置
        protected void Page_Load(object sender, EventArgs e)
        {  
            
            if (!IsPostBack)
            {
           
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.Name = txtFileName.Text.Trim();
            model.Description = txtDescription.Text.Trim();
            model.ThemeSize=Common.Globals.SafeInt(hidFileSize.Value,0);
            model.Author = txtAuthor.Text.Trim();
            if (!FileImage.HasFile)
            {
                Common.MessageBox.Show(this,"请上传预览图");
                return;
            }
            if (string.IsNullOrEmpty(hidFileUrl.Value))
            {
                Common.MessageBox.Show(this,"请上传模板文件");
                return;
            }

            #region 预览图
            string ImageName = FileImage.PostedFile.FileName;
            FileImage.SaveAs(Server.MapPath(ImagePath) + ImageName);
            model.PreviewPhotoSrc = ImagePath + ImageName; 
            #endregion

            #region 文件保存 含文件移动

            string finalPath = "";
            if (!Components.FileHelper.FileRemove(hidFileUrl.Value, FilePath, ref finalPath))
            {
                Common.MessageBox.ShowFailTip(this, "文件保存失败，请重试");
                return;
            }
            model.ZipPackageSrc = finalPath;
            ;
            #endregion

            if (Components.FileHelper.UnpackFiles(Server.MapPath(finalPath), Server.MapPath(DedecompressionPath))&&bll.Add(model) > 0)
            {
                Common.MessageBox.ShowAndRedirect(this,"添加成功","List.aspx");
            }
            Common.MessageBox.ShowFailTip(this,"上传失败,请重试");
        }
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        }
    }
}
