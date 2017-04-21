using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Ms
{
    public static class EnumHelper
    {
        /// <summary>
        /// 缩略图尺寸类型 0：CMS，1：SNS，2：Shop，3：Tao，4：COM
        /// </summary>
        public enum AreaType
        {
            CMS = 0,
            SNS = 1,
            Shop = 2,
            Tao = 3,
            COM = 4,
            Brands=5
        }
    }
}
