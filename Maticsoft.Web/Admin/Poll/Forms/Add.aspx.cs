using System;
namespace Maticsoft.Web.Forms
{
    public partial class Add : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Master.TabTitle = "��Ϣ��ӣ�����ϸ��д������Ϣ";
        }
        protected override int Act_PageLoad { get { return 354; } } //�ͷ�����_�ʾ����_���ҳ
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Poll.ErrorFormsNameNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Poll.ErrorFormsExplainNull);
                return;
            }
            string Name = this.txtName.Text;
            string Description = this.txtDescription.Text;

            Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms();
            model.Name = Name;
            model.Description = Description;

            Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
            int id = bll.Add(model);
            if (id > 0)
            {
                Response.Redirect("../Topics/Index.aspx?fid=" + id.ToString());
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "����ʧ�ܣ�");
            }
        }

    }
}
