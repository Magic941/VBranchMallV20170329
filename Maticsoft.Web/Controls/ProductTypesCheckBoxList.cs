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
    /// ��Ʒ�����б� ����CheckBoxList
    /// </summary>
    public class ProductTypesCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 7;
        private System.Web.UI.WebControls.RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            BLL.Shop.Products.ProductType productTypes = new BLL.Shop.Products.ProductType();
            foreach (Model.Shop.Products.ProductType model in productTypes.GetProductTypes())
            {
                base.Items.Add(new ListItem(model.TypeName, model.TypeId.ToString()));
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
