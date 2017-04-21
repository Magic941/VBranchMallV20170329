using System;
using System.IO;
using System.Text;
using System.Web;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.JLT.ToDoInfo
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 680; } } //移动办公_待办管理_编辑页
        private Maticsoft.BLL.JLT.ToDoInfo bll = new BLL.JLT.ToDoInfo();
        private Maticsoft.Model.JLT.ToDoInfo model = new Model.JLT.ToDoInfo();
        private User user = new User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int Status
        {
            get
            {
                int status = 0;
                string strstatus = Request.Params["status"];
                if (!string.IsNullOrWhiteSpace(strstatus))
                {
                    status = Globals.SafeInt(strstatus, 0);
                }
                return status;
            }
        }

        public int InfoID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!String.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        public void ShowInfo()
        {
            if (bll.Exists(InfoID))
            {
                model = bll.GetModel(InfoID);
                ltlToDoID.Text = model.ID.ToString();
                txtTitle.Text = model.Title;
                txtContent.Text = model.Content;
                user = new Maticsoft.Accounts.Bus.User(model.UserId);
                ltlUserName.Text = user.UserName;
                dropStatus.SelectedValue = model.Status.ToString();
                hdCreatedDate.Value = model.CreatedDate.ToString();
                hdToType.Value = model.ToType.ToString();
                hdCreatedID.Value = model.CreatedUserId.ToString();
                if (!String.IsNullOrWhiteSpace(model.ToUserId))
                {
                    hdToUserId.Value = model.ToUserId;
                }

                string hrefLink = "<a href='{0}' target='_blank' title='{1}' id='a_{2}' class='a_class'>查看/下载</a>&nbsp;&nbsp;<a id='del_{2}' class='del_class' i='{2}' n='{1}'>删除</a>";
                StringBuilder strFilePth = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(model.FileNames) && !string.IsNullOrWhiteSpace(model.FileDataPath))
                {
                    HiddenField_Old.Value = model.FileNames;
                    HiddenField_New.Value = model.FileNames;
                    string[] names = model.FileNames.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < names.Length; i++)
                    {
                        string path = Maticsoft.Common.Globals.HostPath(Request.Url) +
                                      string.Format(model.FileDataPath, names[i]);
                        strFilePth.AppendFormat(hrefLink, path, names[i],i);
                        
                        strFilePth.Append("<br/>");
                    }
                    this.litFile.Text = strFilePth.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.JLT.ToDoInfo model = bll.GetModel(InfoID);
            model.ID = Globals.SafeInt(ltlToDoID.Text, -1);
            model.Title = txtTitle.Text.Trim();
            model.Content = txtContent.Text.Trim();
            user = new Maticsoft.Accounts.Bus.User(ltlUserName.Text);
            model.UserId = user.UserID;
            model.UserName = user.UserName;
            model.CreatedUserId = Globals.SafeInt(hdCreatedID.Value, -1);
            if (!String.IsNullOrWhiteSpace(hdToType.Value))
            {
                model.ToUserId = hdToType.Value;
            }
            model.Status = int.Parse(dropStatus.SelectedValue);
            model.CreatedDate = Globals.SafeDateTime(hdCreatedDate.Value, DateTime.Now);
            model.ReviewedUserID = CurrentUser.UserID;
            model.ReviewedDate = DateTime.Now;
            model.ToType = int.Parse(hdToType.Value);
            if (bll.Update(model))
            {
                SaveFile(model.ID, model.UserId, bll, model.FileNames);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑待办：【{0}】", model.Title), this);
                MessageBox.ShowSuccessTip(this, "编辑待办成功！", "list.aspx");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx?status=" + Status);
        }


        private void SaveFile(int id, int userId, Maticsoft.BLL.JLT.ToDoInfo bll,string old_fineName)
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

            string fileDatapath = string.Format("/Upload/JLT/ToDos/{0}/{1}_", userId, id) + "{0}";

            string filename = string.Empty;
            if (this.HiddenField_New.Value == this.HiddenField_Old.Value)
            {
                filename = string.IsNullOrWhiteSpace(old_fineName) ? strFileNames.ToString().TrimEnd('|') : strFileNames.ToString() + old_fineName;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(strFileNames.ToString()))
                {
                    filename = string.IsNullOrWhiteSpace(old_fineName) ? strFileNames.ToString().TrimEnd('|') : strFileNames.ToString() + HiddenField_Old.Value;
                }
                else if (!string.IsNullOrWhiteSpace(this.HiddenField_Old.Value))
                {
                    filename = HiddenField_Old.Value;
                }
            }

            if (!string.IsNullOrWhiteSpace(filename))
            {
                bll.Update(id, filename, fileDatapath);
            }
            else
            {
                bll.Update(id, "", "");
            }
        }

        private void UpLoadFile(HttpPostedFile file, int userId, int id, out string filename)
        {
            string fileDatapath = string.Format("/Upload/JLT/ToDos/{0}/", userId);
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