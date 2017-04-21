using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.UserRank
{
    public partial class UpdateRank : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.UserRank bll = new BLL.Members.UserRank();
        protected override int Act_PageLoad { get { return 305; } } //用户管理_会员等级管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                    ShowInfo(GradeID);
            }
        }

        public int GradeID
        {
            get
            {
                int _gradeID = -1;
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    _gradeID = Common.Globals.SafeInt(Request.Params["id"], -1);
                }
                return _gradeID;
            }
        }

        private void ShowInfo(int GradeID)
        {
            Maticsoft.Model.Members.UserRank model = bll.GetModel(GradeID);
            this.txtLevel.Text = model.RankLevel.ToString();
            this.txtName.Text = model.Name;
            this.txtMinRange.Text = model.PointMin.ToString();
            this.txtMaxRange.Text = model.PointMax.ToString();
            this.txtDesc.Text = model.Description;
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtLevel.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级级别！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级名称！");
                return;
            }
            if (Globals.SafeInt(this.txtLevel.Text, 0) > 20)
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入0-20之间正确的等级级别！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtMinRange.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分下限！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtMaxRange.Text))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "请输入等级积分上限！");
                return;
            }
            string Name = this.txtName.Text;
            int MinRange = int.Parse(this.txtMinRange.Text);
            int MaxRange = int.Parse(this.txtMaxRange.Text);
            int level = Common.Globals.SafeInt(this.txtLevel.Text, 0);

            Maticsoft.Model.Members.UserRank model = bll.GetModel(GradeID);
            model.Name = Name;
            model.RankLevel = level;
            model.PointMin = MinRange;
            model.PointMax = MaxRange;
            model.Description = this.txtDesc.Text;
            

            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改用户等级（" + model.RankId + "）成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "RankList.aspx");
            }
            else
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                Maticsoft.Common.MessageBox.ShowFailTip(this, "系统忙，请稍后再试！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改用户等级（" + model.RankId + "）失败", this);
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("RankList.aspx");
        }
    }
}