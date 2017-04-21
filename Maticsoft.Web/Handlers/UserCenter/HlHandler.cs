
using Maticsoft.BLL.Shop.Card;
using Maticsoft.Json;
using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Maticsoft.Web.Handlers.UserCenter
{
    public class HlHandler : IHttpHandler, IRequiresSessionState
    {
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";
        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            
            try
            {
                switch (action)
                {
                    case "ACTIVE": ActiveHlCard(context) ; break;
                    case "GETAGREEMENT": GetAgreement(context); break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                json.Put(SHOP_KEY_DATA, ex.Message);
                context.Response.Write(json.ToString());
            }

        }


        public void GetAgreement(HttpContext context)
        {
            string cardTypeNo = context.Request.Form["cardTypeNo"];
            UserCardLogic uc = new UserCardLogic();
            string msg = "";
            Shop_CardType cardType = uc.CheckCardTypeInfo(cardTypeNo, out msg);
            if (cardType != null)
            {
                JsonObject json = new JsonObject();
                json.Put("IsMorePerson", cardType.IsMorePerson);
                json.Put("PersonNum", cardType.PersonNum);
                json.Put("Agreement", cardType.Agreement);
                
                context.Response.Write(json.ToString());
            }
            else
            {
                context.Response.Write("{\"msg\":\"" + msg + "\",\"Agreement\":\"\"}");
            }
        }

        public void ActiveHlCard(HttpContext context)
        {
            string msg = "";
            string HlCard = context.Request.Form["CardNum"];
            string CardPwd = context.Request.Form["CardPwd"];
            //string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(CardPwd, "md5").ToLower();

            UserCardLogic uc = new UserCardLogic();

            Shop_Card cardInfo = uc.CheckCardInfo(HlCard, CardPwd, out msg);
            if (cardInfo!=null)
            {
                cardInfo.Password = CardPwd;
            }
            context.Session["Shop_Card"] = cardInfo;
            
            
            
            if (cardInfo != null)
            {
                if (cardInfo.IsActivate)
                {
                    context.Response.Write("{\"msg\":\"该卡已激活,不能重复激活.\",\"result\":\"\"}");
                }
                else if (!cardInfo.IsLock)
                {
                    context.Response.Write("{\"msg\":\"该卡未解锁,不能激活.\",\"result\":\"\"}");
                }
                else
                {
                    Shop_CardType cardType = uc.CheckCardTypeInfo(cardInfo.CardTypeNo, out msg);
                    if (cardType != null)
                    {
                        context.Session["Shop_CardType"] = cardType;
                        if (cardType.RegisterType)
                        {
                            context.Response.Write("{\"msg\":\"" + msg + "\",\"result\":\"simple\",\"SalesName\":\"" + cardInfo.SalesName + "\",\"CardSysId\":" + cardInfo.Id + "}");
                        }
                        else
                        {
                            context.Response.Write("{\"msg\":\"" + msg + "\",\"result\":\"normal\",\"SalesName\":\"" + cardInfo.SalesName + "\",\"CardSysId\":" + cardInfo.Id + "}");
                        }
                    }
                    else
                    {
                        context.Response.Write("{\"msg\":\"" + msg + "\",\"result\":\"\"}");
                    }
                }
            }
            else
            {
                context.Response.Write("{\"msg\":\"" + msg + "\",\"result\":\"\"}");
            }
        }
    }
}