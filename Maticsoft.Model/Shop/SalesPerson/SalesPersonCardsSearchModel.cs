/**  版本信息模板在安装目录下，可自行修改。
* Shop_Card.cs
*
* 功 能： N/A
* 类 名： Shop_Card
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/6/28 16:15:20   lcy    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海真好邻电子商务有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace Maticsoft.Model
{
    /// <summary>
    /// 营销员卡销售模型
    /// </summary>
    [Serializable]
    public class SalesPersonCardsSearchModel
    {
        public int CID { get; set; }
        public string CardNo { get; set; }

        public bool IsActivate { get; set; }
        public DateTime ActivateDate { get; set; }
        public int SalesId { get; set; }
        public DateTime InsureOrderStart { get; set; }
        public DateTime InsureOrderEnd { get; set; }
        public string Name { get; set; }
        public string Moble { get; set; }

        /// <summary>
        /// 证件号,包括单位号，身份证号，出生证号
        /// </summary>
        public string CardId { get; set; }
        public string InsureNo { get; set; }
        public string SalesName { get; set; }
    }

   public class SalesPersonQueryReturnModel
   {
       public int TotalRows { get; set; }

       public List<SalesPersonCardsSearchModel> Data { get; set; }
       
   }

     [Serializable]
    public class SalesPersonQueryModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string salesMobile { get; set; }

        public string salesPwd { get; set; }

        public string searchCardNo { get; set; }
        public string searchCardId { get; set; }
        public string searchInsureNo { get; set; }
        public int insureNoSearchType { get; set; }
        public int curPageIndex { get; set; }
        public int pageSize { get; set; }

    }
}

