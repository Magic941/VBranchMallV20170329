using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
  public  class DAShopInquiry : DataAccessBase
    {
        /// <summary>
        /// 创建InquiryInfo数据层接口。
        /// </summary>
      public static Maticsoft.IDAL.Shop.Inquiry.IInquiryInfo CreateInquiryInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Inquiry.InquiryInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Inquiry.IInquiryInfo)objType;
        }

        /// <summary>
        /// 创建InquiryItem数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Inquiry.IInquiryItem CreateInquiryItem()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Inquiry.InquiryItem";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Inquiry.IInquiryItem)objType;
        }

    }
}
