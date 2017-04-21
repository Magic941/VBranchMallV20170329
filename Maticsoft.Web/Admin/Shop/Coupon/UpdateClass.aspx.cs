using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class UpdateClass : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 411; } } //Shop_优惠券分类管理_编辑页
        Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new CouponClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }

        public int ClassId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Coupon.CouponClass classModel = classBll.GetModel(ClassId);
            tName.Text = classModel.Name;
            txtSequence.Text = classModel.Sequence.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Coupon.CouponClass classModell = classBll.GetModel(ClassId);
            string name = this.tName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入分类名称");
                return;
            }
            classModell.Name = name;

            classModell.Sequence = Common.Globals.SafeInt(this.txtSequence.Text, 0);

            if (classBll.Update(classModell))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "操作失败！");
            }
        }


    }
}