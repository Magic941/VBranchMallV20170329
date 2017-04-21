using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Team
{
    /// <summary>
    /// SumFormula:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
  
    public class SalesPersonBillboard
    {
        public SalesPersonBillboard()
        { }
        //Id, SalesName, SalesMobile, BatchNo, CreatedDate, BillboardType, Result, Num
        /// <summary>
        /// Id
        /// </summary>		

        public int Id
        {
            set;
            get;
        }

        public string SalesName
        {
            set;
            get;
        }

        public string SalesMobile
        {
            set;
            get;
        }

        public int BatchNo
        {
            set;
            get;
        }

        public DateTime CreatedDate
        {
            set;
            get;
        }

        public int BillboardType
        {
            set;
            get;
        }

        public string Result
        {
            set;
            get;
        }

        public int Num
        {
            set;
            get;
        }
    }
}
