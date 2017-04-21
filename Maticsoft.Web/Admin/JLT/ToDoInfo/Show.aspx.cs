using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.ToDoInfo
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 681; } } //移动办公_待办管理_详细页
        Maticsoft.BLL.JLT.ToDoInfo bll = new BLL.JLT.ToDoInfo();
        Maticsoft.Model.JLT.ToDoInfo model = new Model.JLT.ToDoInfo();
        Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        public int ToDoId
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

        public int TypeID
        {
            get
            {
                int type = -1;
                string strtype = Request.Params["type"];
                if (!string.IsNullOrWhiteSpace(strtype))
                {
                    type = Globals.SafeInt(strtype, 0);
                }
                return type;
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

        public void ShowInfo()
        {
            model = bll.GetModel(ToDoId);
            ltlToDoID.Text = model.ID.ToString();
            user = new User(model.UserId);
            if (!String.IsNullOrWhiteSpace(user.UserType))
            {
                ltlToUserName.Text = user.UserName;
            }
            else
            {
                ltlUserName.Text = "";
            }
            ltlTitle.Text = model.Title;
            ltlContent.Text = model.Content;
            ltlCreatedDate.Text = model.CreatedDate.ToString();
            user = new User(model.CreatedUserId);
            if (!String.IsNullOrWhiteSpace(user.UserType))
            {
                ltlUserName.Text = user.UserName;
            }
            else
            {
                ltlUserName.Text = "";
            }
            ltlRemark.Text = model.Remark;
            int status = model.Status;
            int totype = model.ToType;
            switch (status)
            {
                case 0:
                    ltlStatus.Text = "未办";
                    break;
                case 1:
                    ltlStatus.Text = "已办";
                    break;
                case 2:
                    ltlStatus.Text = "未通过";
                    break;
                case 3:
                    ltlStatus.Text = "已通过";
                    break;
            }
            switch (totype)
            {
                case 0:
                    ltlToType.Text = "本人";
                    break;
                case 1:
                    ltlToType.Text = "下属";
                    break;
                case 2:
                    ltlToType.Text = "所有人";
                    break;
                case 3:
                    ltlToType.Text = "指定用户";
                    break;
            }


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
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (TypeID != 1)
            {
                Response.Redirect("list.aspx?status=" + Status);
            }
            else
            {
                Response.Redirect("MyToDoInfo.aspx?type=" + TypeID + "&status=" + Status);
            }
            
        }
    }
}