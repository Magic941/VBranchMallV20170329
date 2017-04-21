using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.Reports
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 666; } } //移动办公_日报管理_详细页
        Maticsoft.BLL.JLT.Reports bll = new BLL.JLT.Reports();
        Maticsoft.Model.JLT.Reports model = new Model.JLT.Reports();
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }
        public int infoID
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
            model = bll.GetModel(infoID);
            ltlReportId.Text = model.ID.ToString();
            user = new Maticsoft.Accounts.Bus.User(model.UserId);
            ltlUserName.Text = user.UserName;
            ltlContent.Text = model.Content;
            ltlCreatedDate.Text = model.CreatedDate.ToString();
            ltlRemark.Text = model.Remark;

            string hrefLink = "<a href='{0}' target='_blank' title='{1}'>查看/下载</a>";
            StringBuilder strFilePth = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(model.FileNames) && !string.IsNullOrWhiteSpace(model.FileDataPath))
            {
                string[] names = model.FileNames.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < names.Length; i++)
                {
                    string path = Maticsoft.Common.Globals.HostPath(Request.Url) +
                                  string.Format(model.FileDataPath, names[i]);
                    strFilePth.AppendFormat(hrefLink, path, names[i]);
                    strFilePth.Append("<br/>");
                }
                this.litFile.Text = strFilePth.ToString();
            }
            else
            {
                this.litFile.Text = "无";
            }
            int type = model.Type;
            switch (type)
            {
                case 0:
                    ltlType.Text = "文字";
                    break;
                case 1:
                    ltlType.Text = "图片";
                    break;
                case 2:
                    ltlType.Text = "声音";
                    break;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}