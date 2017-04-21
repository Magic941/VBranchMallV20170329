using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maticsoft.Services;
using Maticsoft.Model;
using Maticsoft.DALFactory;
using Maticsoft.IDAL;
using Maticsoft.IDAL.Members;

namespace Maticsoft.BLL
{

    public class CallApi
    {
        private readonly IShop_CardUserInfo userInfoDal = DAShopCard.CreateShop_CardUserInfo();
        private readonly IUsers userDal = DAMembers.CreateUsers();
        private readonly IUsersExp userExpDal = DAMembers.CreateUsersExp();

        /// <summary>
        /// 我的收入
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="sumRows">总记录数</param>
        /// <param name="Page">当前页码</param>
        /// <param name="PageSize">页容量</param>
        /// <returns>我的收入</returns>
        public List<Maticsoft.Model.Team.AppInCome> getMyInCome(string phone, ref int sumRows, int Page = 0, int PageSize = 0)
        {
            //营销员API
            //string salesNo = "";
            //Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
            //Maticsoft.Model.Members.UsersExpModel modelExp = new Maticsoft.Model.Members.UsersExpModel();
            //model = userDal.GetModel(phone);
            //modelExp = userExpDal.GetModel(model.UserID);
            //int userOldType = Convert.ToInt32(modelExp.UserOldType);
            //if (userOldType > 1)
            //{
            //    salesNo = modelExp.SalesNo;
            //}
            //else
            //{
            //    string cardNo = userInfoDal.GetDefaultCardNo(userName);
            //    string cardUri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
            //    var cardApiHelper = new APIHelper(cardUri);
            //    salesNo = cardApiHelper.GetSalesNoByCardNo(cardNo);
            //    if (salesNo.Contains("Error:"))
            //    {
            //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("读取GetSalesNoApi").Write(salesNo + DateTime.Now.ToString());
            //        return null;
            //    }
            //}

            //我的收入API
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["B2BURL"];
            var teamApiHelper = new TeamAPI(baseuri);
            List<Maticsoft.Model.Team.AppInCome> myInComes = teamApiHelper.GetMyInComeList(phone);

            //处理分页
            int sumPage = 0;
            sumRows = myInComes.Count;
            if (Page > 0 & PageSize > 0)
            {
                if (myInComes.Count % PageSize == 0)
                {
                    sumPage = myInComes.Count / PageSize;
                }
                else
                {
                    sumPage = myInComes.Count / PageSize + 1;
                }

                int int_StartRow = (Page - 1) * PageSize;
                int int_EndRow = (Page) * PageSize;

                List<Maticsoft.Model.Team.AppInCome> tempMyInCome = new List<Model.Team.AppInCome>();
                for (int i = int_StartRow; i < int_EndRow; i++)
                {
                    if (myInComes.Count > i)
                    {
                        tempMyInCome.Add(myInComes[i]);
                    }
                }
                return tempMyInCome;
            }
            return myInComes;
        }

        /// <summary>
        /// 我的粉丝
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="sumRows">总记录数</param>
        /// <param name="Page">当前页码</param>
        /// <param name="PageSize">页容量</param>
        /// <returns>我的粉丝</returns>
        public List<Maticsoft.Model.Team.AppMember> getMyMembers(string saleMobile, ref int sumRows, int Page = 0, int PageSize = 0)
        {
            //营销员API
            //string salesNo = "";
            //Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
            //Maticsoft.Model.Members.UsersExpModel modelExp = new Maticsoft.Model.Members.UsersExpModel();
            //model = userDal.GetModel(userName);
            //modelExp = userExpDal.GetModel(model.UserID);
            //int userOldType = Convert.ToInt32(modelExp.UserOldType);
            //if (userOldType > 1)
            //{
            //    salesNo = modelExp.SalesNo;
            //}
            //else
            //{
            //    string cardNo = userInfoDal.GetDefaultCardNo(userName);
            //    string cardUri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
            //    var cardApiHelper = new APIHelper(cardUri);
            //    salesNo = cardApiHelper.GetSalesNoByCardNo(cardNo);
            //    if (salesNo.Contains("Error:"))
            //    {
            //        Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("读取GetSalesNoApi").Write(salesNo + DateTime.Now.ToString());
            //        return null;
            //    }
            //}

            //我的粉丝API
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["B2BURL"];
            var teamApiHelper = new TeamAPI(baseuri);
            List<Maticsoft.Model.Team.AppMember> myMembers = teamApiHelper.GetMyMembers(saleMobile);
            int sumPage = 0;
            sumRows = myMembers.Count;

            //处理分页
            if (Page > 0 & PageSize > 0)
            {
                if (myMembers.Count % PageSize == 0)
                {
                    sumPage = myMembers.Count / PageSize;
                }
                else
                {
                    sumPage = myMembers.Count / PageSize + 1;
                }

                int int_StartRow = (Page-1) * PageSize;
                int int_EndRow = (Page) * PageSize;

                List<Maticsoft.Model.Team.AppMember> tempMember = new List<Model.Team.AppMember>();
                for (int i = int_StartRow; i < int_EndRow;i++ )
                {
                    if (myMembers.Count > i)
                    {
                        tempMember.Add(myMembers[i]);
                    }
                }
                return tempMember;
            }

            return myMembers;
        }

        /// <summary>
        /// 自动激活
        /// </summary>
        /// <param name="SalesMobile">推荐手机号</param>
        /// <param name="Mobile">手机号</param>
        /// <param name="Name">姓名</param>
        /// <param name="UserNo">用户名</param>
        /// <returns></returns>
        public bool AutoActive(ref string msg,string SalesMobile, string Mobile, string Name, string UserNo)
        {
            //SalesMobile = "";
            Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("激活API日志").Write("推荐手机号：" + SalesMobile + " ，手机号：" + Mobile + " ，姓名：" + Name + " ，用户名：" + UserNo);

            if (string.IsNullOrEmpty(SalesMobile))
            {
                SalesMobile = "8888";
            }
            
            bool isActive = false;
            string baseuri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
            var cardApiHelper = new APIHelper(baseuri);
            Maticsoft.Model.Shop_CardUserInfo userInfo = cardApiHelper.ShowAutoActive(SalesMobile, Mobile, Name, UserNo);

            if (userInfo.UserInfoStatus != 4)
            {
                userInfoDal.Add(userInfo);
                msg = "自动激活与分配卡成功";
                isActive = true;
            }
            else
            {
                msg = "没找到合适的推荐人或分配卡失败。";
                return isActive;
            }
            return isActive;
        }
    }
}
