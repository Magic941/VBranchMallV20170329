/*----------------------------------------------------------------
// Copyright (C) 2012 ����׿Խ ��Ȩ���С� 
//
// �ļ�����ProductAccessorieEx.cs
// �ļ�����������
// 
// ������ʶ��
// �޸ı�ʶ��
// �޸�������
//
// �޸ı�ʶ��
// �޸�������
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.Model.Shop.Products
{
   public partial class ProductAccessorie
    {
        private string _skuId;

        public string SkuId
        {
            get { return _skuId; }
            set { _skuId = value; }
        }
    }
}
