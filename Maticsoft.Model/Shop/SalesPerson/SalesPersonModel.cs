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
namespace Maticsoft.Model
{
    /// <summary>
    /// 营销人员，通过手机号和密码登录后获得该对象
    /// </summary>
    [Serializable]
    public class SalesPersonModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string SalesName { get; set; }

        public string Mobile { get; set; }

        public int SysId { get; set; }

        public string StoreName { get; set; }

        public string AreaName { get; set; }

        /// <summary>
        /// 店铺类型
        /// </summary>
        public int StoreType { get; set; }

        /// <summary>
        /// 级别名称，方便查看其分润标准
        /// </summary>
        public string SalesPersonLevelName { get; set; }

        /// <summary>
        /// 级别编号
        /// </summary>
        public int SalesPersonLevel { get; set; }


        public string SeachLevelType { get; set; }

        public string SalesPersonPwd { get; set; }
    }
}

