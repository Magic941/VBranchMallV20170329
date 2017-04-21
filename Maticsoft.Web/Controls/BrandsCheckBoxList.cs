/*----------------------------------------------------------------
// Copyright (C) 2012 ����׿Խ ��Ȩ���С� 
//
// �ļ�����ProductTypesCheckBoxList.cs
// �ļ�����������  Rock   ��Ʒ�����б�
// 
// ������ʶ��
// �޸ı�ʶ��
// �޸�������
//
// �޸ı�ʶ��
// �޸�������
//----------------------------------------------------------------*/

using System.Web.UI.WebControls;

namespace Maticsoft.Web.Controls
{
    /// <summary>
    /// Ʒ�����Ͷ�ѡCheckBox ����CheckBoxList
    /// </summary>
    public class BrandsCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 7;
        private System.Web.UI.WebControls.RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            BLL.Shop.Products.BrandInfo bll = new BLL.Shop.Products.BrandInfo();
            foreach (Model.Shop.Products.BrandInfo model in bll.GetBrands())
            {
                base.Items.Add(new ListItem(model.BrandName, model.BrandId.ToString()));
            }
        }

        public override int RepeatColumns
        {
            get
            {
                return this.repeatColumns;
            }
            set
            {
                this.repeatColumns = value;
            }
        }

        public override System.Web.UI.WebControls.RepeatDirection RepeatDirection
        {
            get
            {
                return this.repeatDirection;
            }
            set
            {
                this.repeatDirection = value;
            }
        }
    }
}
