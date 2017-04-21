/**
* Menu.cs
*
* 功 能： N/A
* 类 名： Menu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/17 12:25:29   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Maticsoft.Json.Conversion;
using Maticsoft.Json;

namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// Menu
	/// </summary>
	public partial class Menu
	{
        private readonly IMenu dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IMenu)new WeChat.SQLServerDAL.Core.Menu() : null;
		public Menu()
		{}
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
        public bool Exists(int MenuId)
        {
            return dal.Exists(MenuId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.Menu model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.Menu model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int MenuId)
        {

            return dal.Delete(MenuId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string MenuIdlist)
        {
            return dal.DeleteList(MenuIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.Menu GetModel(int MenuId)
        {

            return dal.GetModel(MenuId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.Menu GetModelByCache(int MenuId)
        {

            string CacheKey = "MenuModel-" + MenuId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MenuId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.Menu)objModel;
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
        public List<Maticsoft.WeChat.Model.Core.Menu> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.Menu> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.Menu> modelList = new List<Maticsoft.WeChat.Model.Core.Menu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.Menu model;
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

	    #region  ExtensionMethod

        public bool UpdateSeq(int seq, int menuId)
	    {
	        return dal.UpdateSeq(seq, menuId);
	    }

	    /// <summary>
        /// 获取Json菜单
        /// </summary>
        /// <returns></returns>
	    public static Maticsoft.WeChat.Model.Core.JsonMenu  GetJsonMenu(string openId,bool IsAuto)
	    {
             string appId=Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId",openId);
            Maticsoft.WeChat.BLL.Core.Menu bll=new Menu();
            DataSet ds = bll.GetList(-1, "Status=1 and OpenId='" + openId+"'", " Sequence ");
	        List<Maticsoft.WeChat.Model.Core.Menu> AllMenu = bll.DataTableToList(ds.Tables[0]);
            Maticsoft.WeChat.Model.Core.JsonMenu menu = new JsonMenu();
           //顶级菜单
	        List<Maticsoft.WeChat.Model.Core.Menu> MainMenu =
	            AllMenu.Where(c => c.ParentId == 0).OrderBy(c => c.Sequence).Take(3).ToList();
                  string requestUrl="https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect";
                  string returnUrl = "http://" + Common.Globals.DomainFullName + "/wcreturn.aspx";
                  string baseUrl = "http://" + Common.Globals.DomainFullName;
            foreach (var item in MainMenu)
            {
                #region 多级菜单
                if (item.HasChildren)
	            {
                    Maticsoft.WeChat.Model.Core.MultiBtn multi=new MultiBtn();
	                multi.name = item.Name;
	                List<Maticsoft.WeChat.Model.Core.Menu> childList =
                        AllMenu.Where(c => c.ParentId == item.MenuId).OrderBy(c => c.Sequence).Take(5).ToList();
                    #region 加子菜单
                    if (childList != null && childList.Count > 0)
	                {
	                    foreach (var child in childList)
	                    {
                            switch (child.Type)
	                        {
                                case "click":
                                    Maticsoft.WeChat.Model.Core.ClickBtn clickBtn=new ClickBtn();
	                                clickBtn.key = child.MenuKey;
	                                clickBtn.name = child.Name;
                                    multi.sub_button.Add(clickBtn);
                                    break;
                                case "view":
                                    Maticsoft.WeChat.Model.Core.ViewBtn viewBtn = new ViewBtn();
                                    viewBtn.url = IsAuto?(child.MenuUrl.Contains("http://") ? child.MenuUrl :
                                        String.Format(requestUrl, appId, Common.Globals.UrlEncode(returnUrl), openId + "|" + Common.Globals.UrlEncode(child.MenuUrl))) : (child.MenuUrl.Contains("http://") ? child.MenuUrl : baseUrl + child.MenuUrl);
                                    viewBtn.name = child.Name;
                                    multi.sub_button.Add(viewBtn);
                                    break;
                                default:
                                    Maticsoft.WeChat.Model.Core.ViewBtn viewBtn2 = new ViewBtn();
                                    viewBtn2.url = IsAuto?(child.MenuUrl.Contains("http://") ? child.MenuUrl :
                                        String.Format(requestUrl, appId, Common.Globals.UrlEncode(returnUrl), openId + "|" + Common.Globals.UrlEncode(child.MenuUrl))) : (child.MenuUrl.Contains("http://") ? child.MenuUrl : baseUrl + child.MenuUrl);
                                    viewBtn2.name = child.Name;
                                    multi.sub_button.Add(viewBtn2);
                                    break;
	                        }
	                    }
                    }
                    #endregion
                    menu.button.Add(multi);
                }
                #endregion
                #region 简单菜单
                else
	            {
                    switch (item.Type)
                        {
                            case "click":
                                Maticsoft.WeChat.Model.Core.ClickBtn clickBtn = new ClickBtn();
                                clickBtn.key = item.MenuKey;
                                clickBtn.name = item.Name;
                                menu.button.Add(clickBtn);
                                break;
                            case "view":
                                Maticsoft.WeChat.Model.Core.ViewBtn viewBtn = new ViewBtn();
                                viewBtn.url = IsAuto?(item.MenuUrl.Contains("http://") ? item.MenuUrl :
                                    String.Format(requestUrl, appId, Common.Globals.UrlEncode(returnUrl), openId + "|" + Common.Globals.UrlEncode(item.MenuUrl))) : (item.MenuUrl.Contains("http://") ? item.MenuUrl : baseUrl + item.MenuUrl);
                                viewBtn.name = item.Name;
                                menu.button.Add(viewBtn);
                                break;
                            default:
                                Maticsoft.WeChat.Model.Core.ViewBtn viewBtn2 = new ViewBtn();
                                viewBtn2.url =  IsAuto?(item.MenuUrl.Contains("http://") ? item.MenuUrl :
                                    String.Format(requestUrl, appId, Common.Globals.UrlEncode(returnUrl), openId + "|" + Common.Globals.UrlEncode(item.MenuUrl))) :(item.MenuUrl.Contains("http://") ? item.MenuUrl : baseUrl + item.MenuUrl);
                                viewBtn2.name = item.Name;
                                menu.button.Add(viewBtn2);
                                break;
                        }
                }
                #endregion
            }
	        return menu;
	    }



        public int GetSequence(string openId)
	    {
            return dal.GetSequence(openId);
	    }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEx(Maticsoft.WeChat.Model.Core.Menu model)
	    {
            return dal.AddEx(model);
	    }

	    public bool DeleteEx(int menuId)
	    {
	        return dal.DeleteEx(menuId);
	    }

        #region 创建菜单
        public static  int CreateMenu(string access_token, string openId,bool isAuto)
        {
            StreamReader reader = null;
            string posturl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
            //创建菜单
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
                request.Method = "POST";
                Maticsoft.WeChat.Model.Core.JsonMenu menu = Maticsoft.WeChat.BLL.Core.Menu.GetJsonMenu(openId, isAuto);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string postData = System.Text.RegularExpressions.Regex.Unescape(jss.Serialize(menu));
                byte[] postdata = Encoding.GetEncoding("UTF-8").GetBytes(postData);
                request.ContentLength = postdata.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(postdata, 0, postdata.Length);
                newStream.Close();
                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();//得到结果
                Maticsoft.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(content);
                int code = Common.Globals.SafeInt(jsonObject["errcode"].ToString(), 0);
                string errmsg = jsonObject["errmsg"].ToString();
                return code;
         
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }

        }
        #endregion 

        public List<Maticsoft.WeChat.Model.Core.Menu> GetMenuList(string openId)
        {
            return GetModelList(" OpenId='" + openId + "'");
        }

        public  Maticsoft.WeChat.Model.Core.Menu GetMenu(string key)
        {
            return dal.GetMenu(key);
        }
        #endregion  ExtensionMethod
    }
}

