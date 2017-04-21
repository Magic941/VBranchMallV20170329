using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.Reports
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 665; } } //移动办公_日报管理_编辑页
        Maticsoft.BLL.JLT.Reports bll = new BLL.JLT.Reports();
        Maticsoft.Model.JLT.Reports model = new Model.JLT.Reports();
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int ReportId
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
            if (bll.Exists(ReportId))
            {
                model = bll.GetModel(ReportId);
                user = new Maticsoft.Accounts.Bus.User(model.UserId);
                ltlUserName.Text = user.UserName;
                txtContent.Text = model.Content;
                dropType.SelectedValue = model.Type.ToString();
                hdCreatedDate.Value = model.CreatedDate.ToString();
                txtRemark.Text = model.Remark;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            user = new Maticsoft.Accounts.Bus.User(ltlUserName.Text);
            model.UserId = user.UserID;
            model.ID = ReportId;
            model.Content = txtContent.Text.Trim();
            model.Type = int.Parse(dropType.SelectedValue);
            string oldpath = Server.MapPath(bll.GetModel(ReportId).FileDataPath);
            if (!String.IsNullOrWhiteSpace(oldpath))
            {
                if (File.Exists(oldpath))
                {
                    File.Delete(oldpath);
                }
            }
            if (!String.IsNullOrWhiteSpace(upFilePath.FileName))
            {
                model.FileDataPath = UpLoadFile(upFilePath.PostedFile.FileName);
            }
            else
            {
                string filename = oldpath.Substring(oldpath.LastIndexOf("\\") + 1);
                model.FileDataPath = UpLoadFile(filename);
            }
            model.Remark = txtRemark.Text.Trim();
            model.CreatedDate = Globals.SafeDateTime(hdCreatedDate.Value, DateTime.Now);
            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑简报：【{0}】", model.ID), this);
                MessageBox.ShowSuccessTip(this, "修改简报成功！", "list.aspx");
            }
        }
        public string UpLoadFile(string filename)
        {
            string path = Server.MapPath(@"\Upload\JLT\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = filename;
            string filetype = "." + filepath.Substring(filepath.LastIndexOf(".") + 1);
            string newfilename = DateTime.Now.Date.ToShortDateString().Replace("/", "") +
                                 DateTime.Now.ToLongTimeString().Replace(":", "") +
                                 DateTime.Now.Millisecond.ToString() + filetype;
            string savePath = path + newfilename;
            string webPath = @"\Upload\JLT\" + newfilename;
            upFilePath.PostedFile.SaveAs(savePath);
            return webPath;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}