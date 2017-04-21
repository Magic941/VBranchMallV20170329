using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Components.Filters;
using Maticsoft.Web.Components.Setting.CMS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Shop.Card;
using Maticsoft.BLL;
using System.Text;
using Maticsoft.Model;
using Newtonsoft.Json;
using Maticsoft.Web.Models;
using Maticsoft.Services;

using Maticsoft.Model.CMS;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    /// <summary>
    /// 卡激活
    /// </summary>
    public class CardActivateController : CMSControllerBase
    {

        /// <summary>
        /// 激活起始页页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //XYZ_APIHelper helper = new XYZ_APIHelper("http://58.240.26.203", "6026829999", "boyangde","abcd9999");

            //helper.Test();

            Session["Shop_Card"] = null;
            return View();
        }

        /// <summary>
        /// 每步业务逻辑独立,在客户端来跳转,每个页面检查卡信息,不合格则不让向下一步
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult GetCard(string cardNo, string pwd)
        {
            UserCardLogic uc = new UserCardLogic();
            string msg;
            Maticsoft.Model.Shop_Card cardInfo = uc.CheckCardInfo(cardNo, pwd, out msg);
            if (cardInfo != null)
            {
                if (cardInfo.CardStatus == 4)
                {
                    return Json(new { IsSuccess = false, Message = cardInfo.CREATER });
                }
                else
                {
                    cardInfo.PasswordOrigin = pwd;
                    Session["Shop_Card"] = cardInfo;
                    return Json(new { IsSuccess = true, Message = "卡获取成功,激活下一步" });
                }
            }

            return Json(new { IsSuccess = false, Message = msg });
        }

        /// <summary>
        /// 第一步主要展示卡的信息,协议和文档（暂时不看）
        /// </summary>
        /// <returns></returns>
        public ActionResult CardActivateStep1()
        {
            var cardtype = ((Maticsoft.Model.Shop_Card)Session["Shop_Card"]).CardSelfType;
            return View(cardtype);

        }

        /// <summary>
        /// 第二步激活数据提交
        /// </summary>
        /// <returns></returns>
        public ActionResult CardActivateStep2()
        {
            var cardtype = ((Maticsoft.Model.Shop_Card)Session["Shop_Card"]).CardSelfType;
            return View(cardtype);

            //var cardtype = new Maticsoft.Model.Shop_CardType();
            //return View(cardtype);
        }

        /// <summary>
        ///驾乘卡激活
        /// </summary>
        /// <returns></returns>
        public ActionResult CardActivateStep21()
        {
            var cardInfo = ((Maticsoft.Model.Shop_Card)Session["Shop_Card"]);
            return View(cardInfo);
        }

        /// <summary>
        /// 接收基本资料并保存基本资料,服务端检查资料的合法性，成功后到预览页面
        /// </summary>, List<Insurants> cardInsurants
        /// <param name="cardApplicant">投保人</param>
        /// <param name="cardInsurants">被保人</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CardActivateStep2SubmitData(Maticsoft.Model.ShopCardUserInfo2 cardApplicant)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cardApplicant.CardInsurantsTxt))
                {
                    return Json(new { IsSuccess = false, Message = "被保人信息不能为空!" });
                }

                UserCardLogic uc = new UserCardLogic();
                var card = (Maticsoft.Model.Shop_Card)Session["Shop_Card"];
                var cardtype = card.CardSelfType;

                Maticsoft.Model.Shop_CardUserInfo userinfo = new Maticsoft.Model.Shop_CardUserInfo();

                userinfo.CardInsurants = cardApplicant.CardInsurantsTxt;

                userinfo.CardInsurantList = new List<Insurants>();
                userinfo.CardInsurantList = JsonConvert.DeserializeObject<List<Insurants>>(cardApplicant.CardInsurantsTxt);
                if (userinfo.CardInsurantList.Count == 0)
                {
                    return Json(new { IsSuccess = false, Message = "被保人信息不能为空!" });
                }
                userinfo.Sex = cardApplicant.Sex;
                userinfo.ActiveDate = DateTime.Now;
                userinfo.Name = cardApplicant.Name;
                userinfo.BackPerson = card.SalesName;
                userinfo.CardId = cardApplicant.CardId;
                userinfo.BirthDay = cardApplicant.BirthDay;
                userinfo.CardNo = card.CardNo;
                userinfo.CREATEDATE = DateTime.Now;
                userinfo.Email = cardApplicant.Email;
                userinfo.CardTypeNo = cardtype.TypeNo;
                userinfo.CardTypeName = cardtype.TypeName;
                userinfo.Moble = cardApplicant.Moble;
                userinfo.Address = cardApplicant.Address;
                userinfo.UserName = card.CardNo;
                //加密的密码
                userinfo.Password = card.Password;
                //未加密码前的密码
                userinfo.PasswordOrigin = card.PasswordOrigin;
                string result = "";

                result = uc.ActiveUserInfo(userinfo, true);

                string errorMsg = "";
                string content = "您的健康卡:" + userinfo.CardNo + "已经激活!";
                //返回1代表激活成功
                if (result == "1")
                {
                    //根据用户名查找 userInfo.UserId
                    Maticsoft.Accounts.Bus.User u = new Maticsoft.Accounts.Bus.User();
                    int userid = u.GetUserByName(userinfo.UserName).UserID;

                    if (!string.IsNullOrEmpty(userinfo.CardId))
                    {
                        Maticsoft.Web.Components.SMSHelper.SendSMS(userinfo.Moble, content, out errorMsg);
                    }
                    //用来模拟登录
                    Session["ShopCardUserInfo"] = userinfo;
                    return Json(new { IsSuccess = true, Message = result });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "激活失败,原因是：" + result });
                }

            }
            else
            {
                StringBuilder message = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    if (item != null)
                    {
                        if (item.Errors.Count > 0)
                        {
                            foreach (var er in item.Errors)
                            {
                                message.Append(er.ErrorMessage).Append(">");
                            }
                        }
                    }
                }
                return Json(new { IsSuccess = false, Message = "数据检查失败!原因是：" + message.ToString() });
            }
        }


        /// <summary>
        /// 驾乘卡
        /// </summary>
        /// <param name="cardApplicant"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CardActivateStep21SubmitData(Maticsoft.Model.DriveCardInfoModel cardApplicant)
        {
            if (ModelState.IsValid)
            {
                var cardInfo = ((Maticsoft.Model.Shop_Card)Session["Shop_Card"]);
                var cardType = cardInfo.CardSelfType;
                cardApplicant.Cards = JsonConvert.DeserializeObject<List<CardInfo>>(cardApplicant.CardsTxt);

                if (cardApplicant.Cards == null || cardApplicant.Cards.Count == 0)
                {
                    return Json(new { IsSuccess = false, Message = "激活投保卡号不能为空!" });
                }
                if (cardApplicant.ApplicantType == 1)
                {
                    //个人数据检查
                    if (string.IsNullOrEmpty(cardApplicant.Name))
                    {
                        return Json(new { IsSuccess = false, Message = "个人投保用户名不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.CardID))
                    {
                        return Json(new { IsSuccess = false, Message = "个人投保身份证不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.Mobile))
                    {
                        return Json(new { IsSuccess = false, Message = "个人投保手机不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.LPNumber))
                    {
                        return Json(new { IsSuccess = false, Message = "个人投保车牌号不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.VINumber))
                    {
                        return Json(new { IsSuccess = false, Message = "个人投保车架号不能为空!" });
                    }
                    if (cardApplicant.SeatsNumber < 1)
                    {
                        return Json(new { IsSuccess = false, Message = "个人投保车座位数是必选项!" });
                    }
                }
                else
                {
                    //个人数据检查
                    if (string.IsNullOrEmpty(cardApplicant.EnterpriseName))
                    {
                        return Json(new { IsSuccess = false, Message = "企业投保企业名称不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.EnterpriseCode))
                    {
                        return Json(new { IsSuccess = false, Message = "企业投保组织机构代码证不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.Mobile))
                    {
                        return Json(new { IsSuccess = false, Message = "企业投保联系人手机不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.LPNumber))
                    {
                        return Json(new { IsSuccess = false, Message = "企业投保车牌号不能为空!" });
                    }

                    if (string.IsNullOrEmpty(cardApplicant.VINumber))
                    {
                        return Json(new { IsSuccess = false, Message = "企业投保车架号不能为空!" });
                    }
                    if (cardApplicant.SeatsNumber < 1)
                    {
                        return Json(new { IsSuccess = false, Message = "企业投保车座位数是必选项!" });
                    }
                }

                cardApplicant.ActivateDate = DateTime.Now;
                cardApplicant.Batch = cardInfo.Batch;
                cardApplicant.CardTypeNo = cardInfo.CardTypeNo;
                cardApplicant.CREATEDATE = DateTime.Now;
                cardApplicant.SalesSysId = cardInfo.SalesId;
                cardApplicant.SalesName = cardInfo.SalesName;


                UserCardLogic uc = new UserCardLogic();
                //激活成功后
                var result = uc.ActiveUserInfo2(cardApplicant);
                string content = "您的驾乘卡已经激活!";
                if (result == "1")
                {                 
                    if (!string.IsNullOrEmpty(cardApplicant.Mobile))
                    {
                        string errorMsg;
                        Maticsoft.Web.Components.SMSHelper.SendSMS(cardApplicant.Mobile, content, out errorMsg);
                    }
                    Maticsoft.Model.Shop_CardUserInfo userinfo = new Maticsoft.Model.Shop_CardUserInfo();
                    userinfo.UserName = cardApplicant.Cards[0].CardNo;
                    userinfo.PasswordOrigin = cardApplicant.Cards[0].Password;


                    //用来模拟登录
                    Session["ShopCardUserInfo"] = userinfo;
                    return Json(new { IsSuccess = true, Message = "激活成功!" });
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = "激活失败,原因是：" + result });
                }

            }
            return Json(new { IsSuccess = true, Message = "激活成功!" });
        }

        /// <summary>
        /// 激活第4步，成功提示，并自动登录连接到系统,设置其它登录为空
        /// </summary>
        /// <returns></returns>
        public ActionResult CardActivateStep4()
        {
            try
            {
                Session["Shop_Card"] = null;
                var userInfo = Session["ShopCardUserInfo"] as Maticsoft.Model.Shop_CardUserInfo;
                //用来模拟登录
                ViewBag.ShopCardUserInfo = userInfo;
                return View(userInfo);
            }
            catch (Exception e)
            {
                Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("激活后异常").Write(e.Message);
            }
            return View();
        }

        /// <summary>
        /// 查看激活卡
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewActivatedCard(string cardNo, string PWD)
        {

            try
            {
                var p = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PWD, "md5").ToLower();
                UserCardLogic uc = new UserCardLogic();
                var userinfo = uc.GetActiveUserInfos(cardNo, p);

                if (userinfo != null)
                {
                    if (typeof(V_DriveCardInfo).IsInstanceOfType(userinfo))
                    {
                        Maticsoft.Services.ErrorLogTxt.GetInstance("激活卡查询").Write("查到激活信息-驾乘卡！");
                        return View("ViewActivatedCardII", userinfo);
                    }
                    else
                    {
                        var m = (Maticsoft.Model.Shop_CardUserInfo)userinfo;
                        Maticsoft.Services.ErrorLogTxt.GetInstance("激活卡查询").Write("查到激活信息-意外卡！" + m.Name);
                        return View("ViewActivatedCard", m);
                    }
                }

                Maticsoft.Services.ErrorLogTxt.GetInstance("激活卡查询").Write("查询返回空！");
                return View("ViewActivatedCardNull", userinfo);
            }
            catch (Exception e)
            {
                Maticsoft.Services.ErrorLogTxt.GetInstance("激活卡查询").Write("视图加载错误,原因是：！"+e.Message);
                return View("ViewActivatedCardNull", null);
            }
        }

        /// <summary>
        /// 读取PDF文档显示在自已的页面中
        /// </summary>
        /// <param name="pdfFileUrl"></param>
        /// <returns></returns>
        public FileStreamResult ReadPDFCard(string pdfFileUrl)
        {

            System.IO.FileStream fs = new System.IO.FileStream(pdfFileUrl, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            return File(fs, "application/pdf");

        }

        /// <summary>
        /// 获取工作类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetJobTypes(string insuranceCompanyCode)
        {
            UserCardLogic uc = new UserCardLogic();
            var allitems = uc.GetJobTypes(insuranceCompanyCode);
            return Json(allitems, JsonRequestBehavior.AllowGet);

        }
    }
}