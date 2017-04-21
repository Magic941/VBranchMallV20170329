using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
namespace Maticsoft.BLL.Members
{
    /// <summary>
    /// 积分记录
    /// </summary>
    public partial class PointsDetail
    {
        private readonly IPointsDetail dal = DAMembers.CreatePointsDetail();
        public PointsDetail()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DetailID)
        {
            return dal.Exists(DetailID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.PointsDetail model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.PointsDetail model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int DetailID)
        {

            return dal.Delete(DetailID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string DetailIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(DetailIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.PointsDetail GetModel(int DetailID)
        {

            return dal.GetModel(DetailID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.PointsDetail GetModelByCache(int DetailID)
        {

            string CacheKey = "PointsDetailModel-" + DetailID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(DetailID);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.PointsDetail)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.PointsDetail> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.PointsDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.PointsDetail> modelList = new List<Maticsoft.Model.Members.PointsDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.PointsDetail model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region 扩展方法
        /// <summary>
        /// 日常操作获取积分
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userid"></param>
        /// <param name="desc"></param>
        /// <param name="extdata"></param>
        /// <returns></returns>
        public int AddPoints(int actionId, int userid, string desc, string extdata = "")
        {
            PointsRule ruleBll = new PointsRule();

            Model.Members.PointsRule ruleModel = ruleBll.GetModel(actionId, userid);
            Model.Members.PointsDetail pointModel = new Model.Members.PointsDetail();
            string isEnable = SysManage.ConfigSystem.GetValueByCache("PointEnable");
            if (!isLimit(ruleModel, userid) && isEnable == "true")
            {
                //添加积分详细信息
                pointModel.CreatedDate = DateTime.Now;
                pointModel.Description = desc;
                pointModel.ExtData = extdata;
                pointModel.Score = ruleModel.Score;
                pointModel.RuleId = ruleModel.RuleId;
                pointModel.UserID = userid;
                pointModel.Type = 0;
                if (dal.AddDetail(pointModel))
                {
                    return ruleModel.Score;
                }
            }
            return 0;
        }
        /// <summary>
        /// 是否限制
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool isLimit(Maticsoft.Model.Members.PointsRule Rule, int userid)
        {
            //根据规则获取限制条件
            Maticsoft.BLL.Members.PointsLimit limitBll = new PointsLimit();
            if (Rule != null)
            {
                if (Rule.LimitID < 0)
                {
                    return false;
                }
                Maticsoft.Model.Members.PointsLimit limitModel = limitBll.GetModel(Rule.LimitID);
                int count = GetCount(userid, limitModel.CycleUnit, limitModel.Cycle, Rule.RuleId);
                if (count >= limitModel.MaxTimes)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据条件限制获取
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="unit"></param>
        /// <param name="cycle"></param>
        /// <returns></returns>
        public int GetCount(int userid, string unit, int cycle, int RuleId)
        {
            return dal.GetRecordCount(" userid=" + userid + " and RuleId=" + RuleId + " and datediff( " + unit + ", CreatedDate, GETDATE())<" + cycle);
        }
        /// <summary>
        /// 添加积分消费明细(例如：积分礼品兑换)
        /// </summary>
        /// <returns></returns>
        public bool UsePoints(int userid, int score, string desc, string extdata = "")
        {
            Maticsoft.Model.Members.PointsDetail pointModel = new Model.Members.PointsDetail();
            pointModel.CreatedDate = DateTime.Now;
            pointModel.Description = desc;
            pointModel.ExtData = extdata;
            pointModel.Score = score;
            pointModel.RuleId = -1;
            pointModel.Type = 1;
            pointModel.UserID = userid;
            return dal.AddDetail(pointModel);
        }
        /// <summary>
        /// 通过消费获取积分（例如：购买某件商品获得与商品价格一定比率的积分，比率可以通过配置表配置）
        /// </summary>
        public bool AddPointsByBuy(int userid, int score, string desc, string extdata = "")
        {
            Maticsoft.Model.Members.PointsDetail pointModel = new Model.Members.PointsDetail();
            pointModel.CreatedDate = DateTime.Now;
            pointModel.Description = desc;
            pointModel.ExtData = extdata;
            pointModel.Score = score;
            pointModel.RuleId = 0;
            pointModel.Type = 0;
            pointModel.UserID = userid;
            return dal.AddDetail(pointModel);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.PointsDetail> GetListByPageEX(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetSignCount(int userId)
        {
            return dal.GetSignCount(userId);
        }
        public List<Maticsoft.Model.Members.PointsDetail> GetSignListByPage(int userId, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetSignListByPage(userId, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 线下充值
        /// </summary>
        public bool AddPointsByOffline(int userid, int score, string desc = "线下充值", string extdata = "")
        {
            Maticsoft.Model.Members.PointsDetail pointModel = new Model.Members.PointsDetail();
            pointModel.CreatedDate = DateTime.Now;
            pointModel.Description = desc;
            pointModel.ExtData = extdata;
            pointModel.Score = score;
            pointModel.RuleId = 0;
            pointModel.Type = score > 0 ? 0 : 1;
            pointModel.UserID = userid;
            return dal.AddDetail(pointModel);
        }
        #endregion
    }
}

