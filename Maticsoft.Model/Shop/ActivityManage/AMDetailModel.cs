using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.ActivityManage
{
    [Serializable]
    public partial class AMDetailModel
    {
        public AMDetailModel() { }
        #region Model
        public int _amdid;
        public int _amid;
        public decimal _amdunitvalue;
        public decimal _amdratevalue;
        //public decimal _amdneedprice;
        //public int _amdnum;
        //public decimal _amddiscount;
        //public decimal _amdcutprice;
        public string _amdtype;
        #endregion
        /// <summary>
        /// 活动明细ID
        /// </summary>
        public int AMDId {
            set { _amdid = value; }
            get { return _amdid; }
        }
        /// <summary>
        /// 关联Shop_ActivityManage表Id
        /// </summary>
        public int AMId {
            set { _amid = value; }
            get { return _amid; }
        }
        /// <summary>
        /// 满足金额
        /// </summary>
        public decimal AMDUnitValue{
            set { _amdunitvalue = value; }
            get { return _amdunitvalue; }
        }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal AMDRateValue {
            set { _amdratevalue = value; }
            get { return _amdratevalue; }
        }
        /// <summary>
        /// 类型 备用
        /// </summary>
        public string AMDType {
            set { _amdtype = value; }
            get { return _amdtype; }
        }
    }
}
