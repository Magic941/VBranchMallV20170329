using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Team
{
    /// <summary>
    /// SalesPersonIncome:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>

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

        public int StoreType { get; set; }
    }
    public enum SalesType
    {
        /// <summary>
        /// 省旗舰店
        /// </summary>
        Pflagship = 5,

        /// <summary>
        /// 市旗舰店
        /// </summary>
        Ciflagship = 51,

        /// <summary>
        /// 县级旗舰店
        /// </summary>
        Cflagship = 52,

        /// <summary>
        /// 服务店
        /// </summary>
        Svship = 4,

        /// <summary>
        /// 分销店
        /// </summary>
        Dship = 3,

        /// <summary>
        /// 个人微店
        /// </summary>
        Pship = 2,
        /// <summary>
        /// Vip会员
        /// </summary>
        Vship = 21,

        /// <summary>
        /// 大学生创业店
        /// </summary>
        Uship = 22
    }


}
