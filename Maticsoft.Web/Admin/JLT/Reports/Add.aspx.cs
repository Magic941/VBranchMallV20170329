using System;
using System.IO;
using System.Text;
using System.Web;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.Reports
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 664; } } //移动办公_日报管理_添加页
        Maticsoft.BLL.JLT.Reports bll = new BLL.JLT.Reports();
        Maticsoft.Model.JLT.Reports model = new Model.JLT.Reports();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Accounts.Bus.User user = CurrentUser;
            if (!string.IsNullOrWhiteSpace(user.UserType))
            {
                model.UserId = user.UserID;
            }
            else
            {
                MessageBox.ShowFailTip(this, "用户编号不存在！");
                return;
            }
            model.Title = txtTitle.Text;
            model.Content = txtContent.Text;
            model.Type = int.Parse(dropType.SelectedValue);
            model.CreatedDate = DateTime.Now;
            model.Remark = txtRemark.Text;
            int id = bll.Add(model);
            if (id > 0)
            {
                SaveFile(id, model.UserId, bll);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("新增简报：【{0}】", model.ID), this);
                MessageBox.ShowSuccessTip(this, "添加简报成功", "list.aspx");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        private void SaveFile(int id, int userId, Maticsoft.BLL.JLT.Reports bll)
        {
            StringBuilder strFileNames = new StringBuilder();
            string fileName1 = string.Empty;
            UpLoadFile(FileUpload1.PostedFile, userId, id, out fileName1);
            if (!string.IsNullOrWhiteSpace(fileName1))
            {
                strFileNames.Append(fileName1);
                strFileNames.Append("|");
            }
            string fileName2 = string.Empty;
            UpLoadFile(FileUpload2.PostedFile, userId, id, out fileName2);
            if (!string.IsNullOrWhiteSpace(fileName2))
            {
                strFileNames.Append(fileName2);
                strFileNames.Append("|");
            }
            string fileName3 = string.Empty;
            UpLoadFile(FileUpload3.PostedFile, userId, id, out fileName3);
            if (!string.IsNullOrWhiteSpace(fileName3))
            {
                strFileNames.Append(fileName3);
                strFileNames.Append("|");
            }
            string fileName4 = string.Empty;
            UpLoadFile(FileUpload4.PostedFile, userId, id, out fileName4);
            if (!string.IsNullOrWhiteSpace(fileName4))
            {
                strFileNames.Append(fileName4);
                strFileNames.Append("|");
            }
            string fileName5 = string.Empty;
            UpLoadFile(FileUpload5.PostedFile, userId, id, out fileName5);
            if (!string.IsNullOrWhiteSpace(fileName5))
            {
                strFileNames.Append(fileName5);
                strFileNames.Append("|");
            }
            string fileName6 = string.Empty;
            UpLoadFile(FileUpload6.PostedFile, userId, id, out fileName6);
            if (!string.IsNullOrWhiteSpace(fileName6))
            {
                strFileNames.Append(fileName6);
                strFileNames.Append("|");
            }
            string fileName7 = string.Empty;
            UpLoadFile(FileUpload7.PostedFile, userId, id, out fileName7);
            if (!string.IsNullOrWhiteSpace(fileName7))
            {
                strFileNames.Append(fileName7);
                strFileNames.Append("|");
            }
            string fileName8 = string.Empty;
            UpLoadFile(FileUpload8.PostedFile, userId, id, out fileName8);
            if (!string.IsNullOrWhiteSpace(fileName8))
            {
                strFileNames.Append(fileName8);
                strFileNames.Append("|");
            }
            string fileName9 = string.Empty;
            UpLoadFile(FileUpload9.PostedFile, userId, id, out fileName9);
            if (!string.IsNullOrWhiteSpace(fileName9))
            {
                strFileNames.Append(fileName9);
                strFileNames.Append("|");
            }
            string fileName10 = string.Empty;
            UpLoadFile(FileUpload10.PostedFile, userId, id, out fileName10);
            if (!string.IsNullOrWhiteSpace(fileName10))
            {
                strFileNames.Append(fileName10);
                strFileNames.Append("|");
            }

            string fileDatapath = string.Format("/Upload/JLT/Reports/{0}/{1}_", userId, id) + "{0}";

            string filename = strFileNames.ToString().TrimEnd('|');
            if (!string.IsNullOrWhiteSpace(filename))
            {
                bll.Update(id, filename, fileDatapath);
            }
        }

        private void UpLoadFile(HttpPostedFile file, int userId, int id, out string filename)
        {
            string fileDatapath = string.Format("/Upload/JLT/Reports/{0}/", userId);
            int filelength = file.ContentLength;
            filename = file.FileName;
            if (filelength < 1)
            {
                filename = "";
                return;
            }
            string ExtensionImage = Path.GetExtension(file.FileName).ToLower();
            if (ExtensionImage == ".exe" || ExtensionImage == ".bat")
            {
                filename = "";
                Common.MessageBox.ShowServerBusyTip(this, "文件格式不正确");
                return;
            }
            string savePath = Server.MapPath(fileDatapath);
            if (!Directory.Exists(savePath))
            {
                //不存在则自动创建文件夹
                Directory.CreateDirectory(Server.MapPath(fileDatapath));
            }
            string path = savePath + id + "_" + file.FileName;
            try
            {
                file.SaveAs(path);
            }
            catch (Exception)
            {
                filename = "";
            }
        }


    }
}