using Maticsoft.Payment.Core;
using Maticsoft.Payment.Model;

namespace Maticsoft.Payment.PaymentInterface.WeChat
{
    using System.Web;
    using System.Globalization;
    using System;
    using System.Text;
    using System.IO;
    using Maticsoft.Payment.Handler;
    using Maticsoft.Payment.PaymentInterface.WeChat.Utils;
    using Maticsoft.Payment.PaymentInterface.WeChat.Models.UnifiedMessage;
    using System.Collections.Generic;
    using System.Reflection;
     
    internal class WeChatRequest : PaymentRequest
    {
        private string gatewayUrl = "https://gw.tenpay.com/gateway/pay.htm";
        private string sign_type = "MD5";
        private string input_charset = "UTF-8";
        private string sign_key_index = "1";
        private string service_version = "1.0";

        //支付参数
        private string partner;//商户号
        private string out_trade_no;// sp_billno);		//商家订单号
        private string total_fee;// (money * 100).ToString()); //商品金额,以分为单位
        private string return_url;//交易完成后跳转的URL
        private string notify_url;//接收财付通通知的URL
        private string body;//商品描述
        private string bank_type = "WX";  //银行类型 固定为 "WX"
        private string spbill_create_ip;  //用户的公网ip，不是商户服务器IP
        private string fee_type = "1";//币种，1人民币

        //业务可选参数
        private string attach = "WeChat";//附加数据，原样返回
        //private string product_fee = "0";//商品费用，必须保证transport_fee + product_fee=total_fee
        //private string transport_fee = "0";               //物流费用，必须保证transport_fee + product_fee=total_fee
        //private string time_start;//订单生成时间，格式为yyyyMMddHHmmss
        //private string time_expire;//订单失效时间，格式为yyyymmddhhmmss
        //private string buyer_id;//买方财付通账号
        //private string goods_tag;//商品标记
        private string trade_mode = "1";//交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
        //private string transport_desc;              //物流说明
        //private string trans_type = "1";//交易类型，1实物交易，2虚拟交易
        //private string agentid;//平台ID
        //private string agent_type;//代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
        //private string seller_id;//卖家商户号，为空则等同于partner

        private string key = "";
        private string key2 = "";
        private string token = "";
        private string appid = "";
        private string userOpenId = "";

        private GatewayInfo getGateway;

        public WeChatRequest(PayeeInfo payee, GatewayInfo gateway, TradeInfo trade)
        {
            if (gateway.DataList == null || gateway.DataList.Count < 3)
            {
                throw new ArgumentNullException("GATEWAYINFO:DATALIST NO APPID");
            }
            
            this.appid = gateway.DataList[2];
            this.token = trade.Token;
            this.key = payee.PrimaryKey;
            this.partner = payee.SellerAccount;
            this.key2 = payee.SecondKey;
            //this.time_start = trade.Date.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            this.body = trade.Subject;         //String(64)
            this.spbill_create_ip = getRealIp();
            this.out_trade_no = trade.OrderId.Replace(";",string.Empty); // 由于微信支付不支持商户订单号中带";",需要把这个字符过滤掉
            this.return_url = gateway.ReturnUrl;
            this.notify_url = gateway.NotifyUrl;
            this.total_fee = Convert.ToInt32((decimal)(trade.TotalMoney * 100M)).ToString(CultureInfo.InvariantCulture);
            this.getGateway = gateway;
            this.userOpenId = GetUserOpenId();
        }

        #region

        private string GetUserOpenId()
        {
            string ret = string.Empty;
            try
            {
                object obj = HttpContext.Current.Session["WeChat_UserName"];
                if (obj == null || string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    ret = null == HttpContext.Current.Request.Cookies["WeChat_UserName"] ? string.Empty : HttpContext.Current.Request.Cookies["WeChat_UserName"].Value;
                    #region 获取用户名(写日志时使用)
                    string strUserName = string.Empty;
                    try
                    {
                        object objUser = HttpContext.Current.Session["UserInfo"];
                        if (null != objUser)
                        {
                            Type userType = objUser.GetType();
                            PropertyInfo property = userType.GetProperty("UserName");
                            if (null != property) strUserName = property.GetValue(objUser, null).ToString();
                        }
                    }
                    catch (Exception ex) { }
                    #endregion
                    Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("用户:" + strUserName + ",从Cookie中获取用户OpenID:" + ret);
                }
                else
                {
                    ret = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("获取用户OpenID异常!" + ex.Message);
            }
            return ret;
        }

        #endregion

        private static string getRealIp()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        public override void SendRequest()
        {
            string userid = "0";

         
            string action = HttpContext.Current.Request.QueryString["action"];
            if (string.IsNullOrWhiteSpace(action))
            {
                action = "show";
                //HttpContext.Current.Response.Write("NO ACTION");
                //HttpContext.Current.Response.End();
                //return;
            }
            action = action.ToLower();
            #region
            //Utils.WxPayHelper wxPayHelper = new Utils.WxPayHelper();
            ////先设置基本信息
            //wxPayHelper.SetAppId(this.appid);
            //wxPayHelper.SetAppKey(this.key2);
            //wxPayHelper.SetPartnerKey(this.key);
            //wxPayHelper.SetSignType("SHA1");
            //设置请求package信息
            
            //wxPayHelper.SetParameter("appid", this.appid); //商品描述
            //wxPayHelper.SetParameter("auth_code", auth_code);
            //wxPayHelper.SetParameter("body", body); //商品描述
            //wxPayHelper.SetParameter("device_info", device_info);
            //wxPayHelper.SetParameter("mch_id", partner); //partner 商户号
            //wxPayHelper.SetParameter("nonce_str", Maticsoft.Payment.PaymentInterface.WeChat.Utils.CommonUtil.CreateNoncestr());
            //wxPayHelper.SetParameter("out_trade_no", out_trade_no); //商家订单号
            //wxPayHelper.SetParameter("spbill_create_ip", spbill_create_ip);//用户的公网ip，不是商户服务器IP
            //wxPayHelper.SetParameter("total_fee", total_fee); //商品金额,以分为单位
            
            //string jsonStr = wxPayHelper.CreateBizPackage();
            //this.getGateway.Data = Globals.GetBasa64Str(jsonStr);
            //ErrorLogTxt.GetInstance("微信支付_请求日志").Write("请求参数:" + jsonStr);

            //System.Console.Out.WriteLine("生成app支付package:");
            //System.Console.Out.WriteLine(wxPayHelper.CreateAppPackage("test"));
            //System.Console.Out.WriteLine("生成jsapi支付package:");
            //string jsApiPackage = wxPayHelper.CreateBizPackage();
            //System.Console.Out.WriteLine(jsApiPackage);
            //System.Console.Out.WriteLine("生成原生支付url:");
            //System.Console.Out.WriteLine(wxPayHelper.CreateNativeUrl("abc"));
            //System.Console.Out.WriteLine("生成原生支付package:");
            //System.Console.Out.WriteLine(wxPayHelper.CreateNativePackage("0", "ok"));

            #endregion

            //DONE: 输出JS进行网关交互

            string result = string.Empty;
            switch (action)
            {
                case "show":
                    #region 方法一
                    Configuration.GatewayProvider provider = Configuration.PayConfiguration.GetConfig().Providers["wechat"] as Configuration.GatewayProvider;
                    if (null != provider)
                    {
                        this.RedirectToGateway(string.Format(CultureInfo.InvariantCulture, HttpContext.Current.Server.HtmlDecode(provider.Attributes["urlFormat"]), this.out_trade_no, this.getGateway.Data));
                    }
                    else
                    {
                        HttpContext.Current.Session["PaymentReturn_OrderId"] = new System.Text.RegularExpressions.Regex(@"\D").Replace(this.out_trade_no, string.Empty); // 将订单ID保存至Session中
                        HttpContext.Current.Response.Redirect(Maticsoft.Payment.Core.Globals.FullPath(@"/m0/PayResult/Fail"));
                        //result = "{\"STATUS\":\"ERROR\",\"DATA\":\"INVALID PREPAYID\"}";
                    }
                    #endregion

                    #region 方法二
                    //Configuration.GatewayProvider provider = Configuration.PayConfiguration.GetConfig().Providers["wechat"] as Configuration.GatewayProvider;
                    //if (provider != null)
                    //{
                    //    UnifiedPrePayMessage retInfo = Maticsoft.Payment.BLL.PaymentModeManage.GetWeiXinPrepayOrder(this.out_trade_no);
                    //    if (null == retInfo || string.IsNullOrWhiteSpace(retInfo.Prepay_Id))
                    //    {
                    //        // 判断openid是否为空
                    //        this.userOpenId = GetUserOpenId();
                    //        if (!string.IsNullOrWhiteSpace(this.userOpenId))
                    //        {
                    //            UnifiedWxPayHelper helper = UnifiedWxPayHelper.CreateUnifiedHelper(this.appid, this.partner, this.key);
                    //            retInfo = UnifiedWxPayHelper.UnifiedPrePay(helper.CreatePrePayPackage(body, this.out_trade_no, total_fee, spbill_create_ip, notify_url, this.userOpenId));
                    //            if (retInfo == null || !retInfo.ReturnSuccess || !retInfo.ResultSuccess || string.IsNullOrEmpty(retInfo.Prepay_Id))
                    //            {
                    //                Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("获取PrepayId失败:" + (!retInfo.ReturnSuccess ? retInfo.Return_Msg : (!retInfo.ResultSuccess ? retInfo.Err_Code_Des : string.Empty)) + ",参数=>body:" + body + ",out_trade_no:" + out_trade_no + ",total_fee:" + total_fee + ",spbill_create_ip:" + spbill_create_ip + ",notify_url:" + notify_url + ",userOpenId:" + this.userOpenId);
                    //                HttpContext.Current.Session["PaymentReturn_OrderId"] = new System.Text.RegularExpressions.Regex(@"\D").Replace(this.out_trade_no, string.Empty); // 将订单ID保存至Session中
                    //                HttpContext.Current.Response.Redirect(Maticsoft.Payment.Core.Globals.FullPath(@"/m0/PayResult/Fail"));
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                retInfo.Out_Trade_No = this.out_trade_no;
                    //                // 保存预支付订单
                    //                if (Maticsoft.Payment.BLL.PaymentModeManage.SaveWeiXinPrepayOrder(retInfo))
                    //                {
                    //                    this.RedirectToGateway(string.Format(CultureInfo.InvariantCulture, HttpContext.Current.Server.HtmlDecode(provider.Attributes["urlFormat"]), this.out_trade_no, this.getGateway.Data));
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("预支付订单保存失败!商户订单号:" + this.out_trade_no + ",服务器返回预支付单号:" + retInfo.Prepay_Id);
                    //                    HttpContext.Current.Response.Redirect(Maticsoft.Payment.Core.Globals.FullPath(@"/m0/PayResult/Fail"));
                    //                    return;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("用户OpenID已过期");
                    //            HttpContext.Current.Response.Redirect(Maticsoft.Payment.Core.Globals.FullPath(@"/m0/PayResult/Fail"));
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        this.RedirectToGateway(string.Format(CultureInfo.InvariantCulture, HttpContext.Current.Server.HtmlDecode(provider.Attributes["urlFormat"]), this.out_trade_no, this.getGateway.Data));
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    HttpContext.Current.Response.Redirect(Maticsoft.Payment.Core.Globals.FullPath(@"/m0/PayResult/Fail"));
                    //    return;
                    //}
                    #endregion

                    break;
                case "bizpackage":
                    #region 方法一
                    
                    bool bolCanNext = true;
                    UnifiedPrePayMessage rtnPrepayModel = null;

                    #region 获取预支付订单号
                    // 判断用户OpenID是否为空
                    this.userOpenId = GetUserOpenId();
                    if (!string.IsNullOrWhiteSpace(this.userOpenId))
                    {
                        UnifiedWxPayHelper helper = UnifiedWxPayHelper.CreateUnifiedHelper(this.appid, this.partner, this.key);
                        rtnPrepayModel = UnifiedWxPayHelper.UnifiedPrePay(helper.CreatePrePayPackage(body, this.out_trade_no, total_fee, spbill_create_ip, notify_url, this.userOpenId));

                        if (null == rtnPrepayModel)
                        {
                            Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("获取PrepayId失败:预支付订单创建异常!");
                            bolCanNext = false;
                            result = "{\"STATUS\":\"ERROR\",\"DATA\":\"预支付订单创建异常,请再支付一次试试!\"}";
                        }
                        else
                        {
                            // 需要区分如果提示订单重复,就从数据库中取
                            if (rtnPrepayModel.ReturnSuccess && rtnPrepayModel.ResultSuccess)
                            {
                                rtnPrepayModel.Out_Trade_No = this.out_trade_no;
                                // 将预支付订单保存至数据库中
                                if (!Maticsoft.Payment.BLL.PaymentModeManage.SaveWeiXinPrepayOrder(rtnPrepayModel))
                                {
                                    Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("新预支付订单保存失败!商户订单号:" + this.out_trade_no + ",服务器返回预支付单号:" + rtnPrepayModel.Prepay_Id);
                                    bolCanNext = false;
                                    result = "{\"STATUS\":\"ERROR\",\"DATA\":\"预支付订单保存失败!\"}";
                                }
                            }
                            else
                            {
                                #region 如果提示"商户订单号重复",改为从数据库中查询
                                //if (rtnPrepayModel.ReturnSuccess && !rtnPrepayModel.ResultSuccess && "" == rtnPrepayModel.Err_Code)
                                //{
                                //    try
                                //    {
                                //        Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("预支付订单已存在:参数=>body:" + body + ",out_trade_no:" + this.out_trade_no + ",total_fee:" + total_fee + ",spbill_create_ip:" + spbill_create_ip + ",notify_url:" + notify_url + ",userOpenId:" + this.userOpenId + ",结果=>return_code:" + rtnPrepayModel.Return_Code + ",return_msg:" + rtnPrepayModel.Return_Msg + ",result_code:" + rtnPrepayModel.Result_Code + ",err_code:" + rtnPrepayModel.Err_Code + ",err_code_desc:" + rtnPrepayModel.Err_Code_Des);
                                //        rtnPrepayModel = Maticsoft.Payment.BLL.PaymentModeManage.GetWeiXinPrepayOrder(this.out_trade_no);
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("本地查询预支付订单异常:" + ex.Message + ",参数=>body:" + body + ",out_trade_no:" + this.out_trade_no + ",total_fee:" + total_fee + ",spbill_create_ip:" + spbill_create_ip + ",notify_url:" + notify_url + ",userOpenId:" + this.userOpenId + ",结果=>return_code:" + rtnPrepayModel.Return_Code + ",return_msg:" + rtnPrepayModel.Return_Msg + ",result_code:" + rtnPrepayModel.Result_Code + ",err_code:" + rtnPrepayModel.Err_Code + ",err_code_desc:" + rtnPrepayModel.Err_Code_Des);
                                //        bolCanNext = false;
                                //        result = "{\"STATUS\":\"ERROR\",\"DATA\":\"预支付订单查询异常(" + ex.Message + ")\"}";
                                //    }
                                //}
                                //else
                                //{
                                //    Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("获取PrepayId失败:" + (!rtnPrepayModel.ReturnSuccess ? rtnPrepayModel.Return_Msg : (!rtnPrepayModel.ResultSuccess ? rtnPrepayModel.Err_Code_Des : string.Empty)) + ",参数=>body:" + body + ",out_trade_no:" + this.out_trade_no + ",total_fee:" + total_fee + ",spbill_create_ip:" + spbill_create_ip + ",notify_url:" + notify_url + ",userOpenId:" + this.userOpenId + ",结果=>return_code:" + rtnPrepayModel.Return_Code + ",return_msg:" + rtnPrepayModel.Return_Msg + ",result_code:" + rtnPrepayModel.Result_Code + ",err_code:" + rtnPrepayModel.Err_Code + ",err_code_desc:" + rtnPrepayModel.Err_Code_Des);
                                //    bolCanNext = false;
                                //    result = "{\"STATUS\":\"ERROR\",\"DATA\":\"" + (!rtnPrepayModel.ReturnSuccess ? rtnPrepayModel.Return_Msg : (!rtnPrepayModel.ResultSuccess ? rtnPrepayModel.Err_Code_Des : string.Empty)) + "\"}";
                                //}
                                #endregion

                                Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("获取PrepayId失败:" + (!rtnPrepayModel.ReturnSuccess ? rtnPrepayModel.Return_Msg : (!rtnPrepayModel.ResultSuccess ? rtnPrepayModel.Err_Code_Des : string.Empty)) + ",参数=>body:" + body + ",out_trade_no:" + this.out_trade_no + ",total_fee:" + total_fee + ",spbill_create_ip:" + spbill_create_ip + ",notify_url:" + notify_url + ",userOpenId:" + this.userOpenId + ",结果=>return_code:" + rtnPrepayModel.Return_Code + ",return_msg:" + rtnPrepayModel.Return_Msg + ",result_code:" + rtnPrepayModel.Result_Code + ",err_code:" + rtnPrepayModel.Err_Code + ",err_code_desc:" + rtnPrepayModel.Err_Code_Des);
                                bolCanNext = false;
                                result = "{\"STATUS\":\"ERROR\",\"DATA\":\"" + (!rtnPrepayModel.ReturnSuccess ? rtnPrepayModel.Return_Msg : (!rtnPrepayModel.ResultSuccess ? rtnPrepayModel.Err_Code_Des : string.Empty)) + "\"}";
                            }
                        }
                    }
                    else
                    {
                        Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("用户OpenID已过期!");
                        bolCanNext = false;
                        result = "{\"STATUS\":\"ERROR\",\"DATA\":\"用户数据已过期!请退出微商城,从微信公众号重新进入微商城后再试!\"}";
                    }
                    #endregion

                    if (bolCanNext)
                    {
                        Utils.WxPayHelper wxPayHelper = new Utils.WxPayHelper();
                        //先设置基本信息
                        wxPayHelper.SetAppId(this.appid);
                        wxPayHelper.SetAppKey(this.key2);
                        wxPayHelper.SetPartnerKey(this.key);
                        wxPayHelper.SetSignType("MD5");
                        result = "{\"STATUS\":\"SUCCESS\",\"DATA\":" + wxPayHelper.CreateBizPackage(rtnPrepayModel.Prepay_Id) + "}";
                    }

                    #endregion

                    #region 方法二
                    //bool bolCanNext = true;
                    //UnifiedPrePayMessage rtnPrepayModel = Maticsoft.Payment.BLL.PaymentModeManage.GetWeiXinPrepayOrder(out_trade_no);
                    //if (null != rtnPrepayModel && !string.IsNullOrWhiteSpace(rtnPrepayModel.Prepay_Id))
                    //{
                    //    #region 判断预支付订单日期是否已经过期,如果过期,重新创建预支付订单

                    //    // 根据预支付订单号获取预支付订单创建日期
                    //    DateTime dtCreated;
                    //    if (CommonUtil.GetPrepayCreatedDate(rtnPrepayModel.Prepay_Id, out dtCreated))
                    //    {
                    //        // 如果当前日期大于预支付订单创建日期,则重新创建预支付订单
                    //        if (DateTime.Now.Date > dtCreated.Date)
                    //        {
                    //            // 判断用户OpenID是否为空
                    //            this.userOpenId = GetUserOpenId();
                    //            if (!string.IsNullOrWhiteSpace(this.userOpenId))
                    //            {
                    //                string trade_no = rtnPrepayModel.Out_Trade_No;
                    //                UnifiedWxPayHelper helper = UnifiedWxPayHelper.CreateUnifiedHelper(this.appid, this.partner, this.key);
                    //                rtnPrepayModel = UnifiedWxPayHelper.UnifiedPrePay(helper.CreatePrePayPackage(body, trade_no, total_fee, spbill_create_ip, notify_url, this.userOpenId));
                    //                if (rtnPrepayModel == null || !rtnPrepayModel.ReturnSuccess || !rtnPrepayModel.ResultSuccess || string.IsNullOrEmpty(rtnPrepayModel.Prepay_Id))
                    //                {
                    //                    Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("重新获取PrepayId失败:" + (!rtnPrepayModel.ReturnSuccess ? rtnPrepayModel.Return_Msg : (!rtnPrepayModel.ResultSuccess ? rtnPrepayModel.Err_Code_Des : string.Empty)) + ",参数=>body:" + body + ",out_trade_no:" + trade_no + ",total_fee:" + total_fee + ",spbill_create_ip:" + spbill_create_ip + ",notify_url:" + notify_url + ",userOpenId:" + this.userOpenId);
                    //                    HttpContext.Current.Session["PaymentReturn_OrderId"] = new System.Text.RegularExpressions.Regex(@"\D").Replace(this.out_trade_no, string.Empty); // 将订单ID保存至Session中
                    //                    bolCanNext = false;
                    //                    //result = "{\"STATUS\":\"ERROR\",\"DATA\":\"" + (!rtnPrepayModel.ReturnSuccess ? rtnPrepayModel.Return_Msg : (!rtnPrepayModel.ResultSuccess ? rtnPrepayModel.Err_Code_Des : string.Empty)) + "\"}";
                    //                }
                    //                else
                    //                {
                    //                    rtnPrepayModel.Out_Trade_No = trade_no;
                    //                    // 更新预支付订单
                    //                    if (!Maticsoft.Payment.BLL.PaymentModeManage.SaveWeiXinPrepayOrder(rtnPrepayModel))
                    //                    {
                    //                        Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("新预支付订单保存失败!商户订单号:" + this.out_trade_no + ",服务器返回预支付单号:" + rtnPrepayModel.Prepay_Id);
                    //                        bolCanNext = false;
                    //                        result = "{\"STATUS\":\"ERROR\",\"DATA\":\"新预支付订单保存失败!\"}";
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("用户OpenID已过期!");
                    //                bolCanNext = false;
                    //                result = "{\"STATUS\":\"ERROR\",\"DATA\":\"用户数据已过期!请退出微商城,从微信公众号重新进入微商城后再试!\"}";
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Maticsoft.Payment.Handler.ErrorLogTxt.GetInstance("微信支付异常日志").Write("预支付订单号不合法!");
                    //        bolCanNext = false;
                    //        result = "{\"STATUS\":\"ERROR\",\"DATA\":\"预支付订单号不合法!\"}";
                    //    }
                    //    #endregion

                    //    if (bolCanNext)
                    //    {
                    //        Utils.WxPayHelper wxPayHelper = new Utils.WxPayHelper();
                    //        //先设置基本信息
                    //        wxPayHelper.SetAppId(this.appid);
                    //        wxPayHelper.SetAppKey(this.key2);
                    //        wxPayHelper.SetPartnerKey(this.key);
                    //        wxPayHelper.SetSignType("MD5");
                    //        result = "{\"STATUS\":\"SUCCESS\",\"DATA\":" + wxPayHelper.CreateBizPackage(rtnPrepayModel.Prepay_Id) + "}";
                    //    }
                    //    else
                    //    {
                    //        if(string.IsNullOrWhiteSpace(result)) result = "{\"STATUS\":\"ERROR\",\"DATA\":\"新预支付订单创建失败!请退出微商城,从微信公众号重新进入微商城后再试!\"}";
                    //    }
                    //}
                    //else
                    //{
                    //    result = "{\"STATUS\":\"ERROR\",\"DATA\":\"INVALID PREPAYID\"}";
                    //}
                    #endregion

                    break;
                default:
                    result = "{\"STATUS\":\"ERROR\",\"DATA\":\"NotImplemented\"}";
                    break;
            }
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(result);
        }

        #region 订单查询
        //public static UnifiedOrderQueryMessage QueryOrder(string orderNo)
        //{
        //    //这里应先判断服务器 订单支付状态，如果接到通知，并已经支付成功，就不用 执行下面的查询了
        //    UnifiedWxPayHelper helper = UnifiedWxPayHelper.CreateUnifiedHelper("wxa4c0d49cf7e3b529", "10060751", "1234567890qwertyuiopasdfghjklzxc");
        //    UnifiedOrderQueryMessage message = UnifiedWxPayHelper.UnifiedOrderQuery(helper.CreateOrderQueryXml(orderNo));
        //    //此处主动查询的结果，只做查询用（不能作为支付成功的依据）
        //    return message;
        //}
        #endregion

        public static string GetDateTimeDiffInfo(DateTime dtBegin, DateTime dtEnd, string strMsg)
        {
            return strMsg + ">> 开始时间:" + dtBegin.ToString() + ",结束时间:" + dtEnd.ToString() + ",时间差为:" + (dtEnd - dtBegin).ToString();
        }
    }
}

