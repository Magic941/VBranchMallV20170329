using System;
using System.Data;
using System.Collections.Generic;
namespace Maticsoft.IDAL.CMS
{
    /// <summary>
    /// 接口层Content
    /// </summary>
    public interface IContent
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ContentID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.CMS.Content model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.CMS.Content model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ContentID);
        bool DeleteList(string ContentIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.CMS.Content GetModel(int ContentID);
        Maticsoft.Model.CMS.Content DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        int GetRecordCount4Menu(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
        /// <summary>
        /// 调用存储过程获取上一条和下一条
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        DataSet GetNextAndUp(int classId,int contentId);


        #region MethodEx

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int UpdatePV(int ContentID);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool UpdateTotalSupport(int ContentID);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<Maticsoft.Model.CMS.Content> DataTableToListEx(DataTable dt);

        #region 批量处理审核状态
        /// 批量处理审核状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere);
        #endregion

        #region 根据ClassID判断是否存在该记录
        /// <summary>
        /// 根据ClassID判断是否存在该记录
        /// </summary>
        bool ExistsByClassID(int ClassID);
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListByView(string strWhere);
        #endregion

        #region 获得数据列表
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListByView(int Top, string strWhere, string filedOrder);
        #endregion


        #region 根据某字段获得前几行数据
        ///<summary>
        ///根据某字段获得前几行数据
        /// </summary>
        DataSet GetListByItem(int Top, string strWhere, string filedOrder);
        #endregion


        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.CMS.Content GetModelEx(int ContentID);
        #endregion

        #region 得到上一个ContentID
        /// <summary>
        /// 得到上一个ContentID
        /// </summary>
        int GetPrevID(int ContentID, int ClassId);
        #endregion

        #region 得到下一个ContentID
        /// <summary>
        /// 得到下一个ContentID
        /// </summary>
        int GetNextID(int ContentID,int ClassId);
        #endregion
        bool ExistTitle(string Title);

        DataSet GetListByPageEx(string strWhere, string orderby, int startIndex, int endIndex);

     DataSet GetHotCom(int diffDate, int top);

        bool UpdateFav(int ContentID);


        bool SetRecList(string ids);
        bool SetHotList(string ids);
        bool SetColorList(string ids);
        bool SetTopList(string ids);

        bool SetRec(int id,bool rec);
        bool SetHot(int id, bool rec);
        bool SetColor(int id, bool rec);
        bool SetTop(int id, bool rec);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetListEx(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获取记录总数
        /// </summary>
        int GetRecordCountEx(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.CMS.Content GetModelByClassID(int ClassID);



        DataSet GetWeChatList(int ClassID, string keyword, int Top);

        

        /// <summary>
        /// 获取顺序最大值
        /// </summary>
        /// <returns></returns>
        int GetMaxSeq();

        #endregion MethodEx
    }
}
