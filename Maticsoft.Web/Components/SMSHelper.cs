using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Web.SMSService;
using Maticsoft.Services;
using Maticsoft.Common;

namespace Maticsoft.Web.Components
{
    public class SMSHelper
    {

        public static string SignSMS = ConfigHelper.GetConfigString("SMSSIGN");
        /// <summary>
        /// 发送短信接口
        /// </summary>
        /// <param name="content"></param>
        /// <param name="numbers"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static bool SendSMS(string content, string[] numbers,int priority=5)
        {
            ////获取短信接口的序列号，和自定义Key
            //string SerialNo = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            //string Key = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Key");
            //if (String.IsNullOrWhiteSpace(SerialNo) || String.IsNullOrWhiteSpace(Key))
            //{
            //    LogHelp.AddErrorLog("亿美短信接口缺少企业序列号或者自定义关键字Key", "亿美短信接口调用失败",HttpContext.Current.Request);
            //    return false;
            //}
            //SMSService.SDKClient sdkClient = new SDKClientClient();
            //sendSMSRequest smsRequest = new sendSMSRequest();
            //smsRequest.arg0 = SerialNo;
            //smsRequest.arg1 = Key;
            //smsRequest.arg3 = numbers;
            //smsRequest.arg4 = Common.Globals.HtmlEncode(content);
            //smsRequest.arg7 = priority;
            //sendSMSResponse smsResponse = sdkClient.sendSMS(smsRequest);
            //if (smsResponse.@return == 0)
            //{
            //    return true;
            //}
            //string msg= SendSMSException(smsResponse.@return);
            //LogHelp.AddErrorLog("亿美短信接口发送短信出现异常，【" + msg + "】", "亿美短信接口调用失败", HttpContext.Current.Request);
            return false;
        }

        public static void CheckSign()
        {
            if(string.IsNullOrEmpty(SignSMS))
            {
                SignSMS = "【1699健康商城】";
            }
        }

        public static bool SendSMS(string phone,string content,out string message)
        {
            CheckSign();
            content += SignSMS;

            try
            {
                IMessage jz = JZMessage.getInstance();
                var x = jz.SendSMS(phone, content);
                message = x;
                //Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("短信发送日志").Write("对手机号码:" + phone + "发送内容为" + content + "返回结果为" + x);
                if (x.Trim() == "1")
                {
                    message = "发送成功";
                    return true;
                }
                //Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("短信发送失败").Write("对手机号码:" + phone + "发送内容为" + content + "返回结果为" + x);
                return false;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                //LogHelp.AddErrorLog("建周短信接口发送短信出现异常，【" + ex.ToString() + "】", "建周短信接口调用失败", HttpContext.Current.Request);
                //Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("短信发送失败").Write("对手机号码:" + phone + "发送内容为" + content + "返回结果为" + message);
                //throw ex;
                return false;
            }
            
        }

        /// <summary>
        /// 	注册序列号
        /// </summary>
        /// <returns></returns>
        public static bool RegistEx()
        {
            //string SerialNo = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            //string Key = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Key");
            //string Pwd = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Pwd");
            //if (String.IsNullOrWhiteSpace(SerialNo) || String.IsNullOrWhiteSpace(Key))
            //{
            //    LogHelp.AddErrorLog("亿美短信接口缺少企业序列号或者自定义关键字Key", "亿美短信接口调用失败", HttpContext.Current.Request);
            //    return false;
            //}
            //SMSService.SDKClient sdkClient = new SDKClientClient();
            //registExRequest request = new registExRequest(SerialNo, Key, Pwd);
            //registExResponse response = sdkClient.registEx(request);
            //if (response.@return == 0)
            //{
            //    return true;
            //}
            //string msg = RegistExException(response.@return);
            //LogHelp.AddErrorLog("亿美短信注册序列号出现异常，【" + msg + "】", "亿美短信接口调用失败", HttpContext.Current.Request);
            return false;

        }

         /// <summary>
        /// 	注册序列号
        /// </summary>
        /// <returns></returns>
        public static bool Logout()
        {
            //string SerialNo = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            //string Key = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Key");
            //string Pwd = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Pwd");
            //if (String.IsNullOrWhiteSpace(SerialNo) || String.IsNullOrWhiteSpace(Key))
            //{
            //    LogHelp.AddErrorLog("亿美短信接口缺少企业序列号或者自定义关键字Key", "亿美短信接口调用失败", HttpContext.Current.Request);
            //    return false;
            //}
            //SMSService.SDKClient sdkClient = new SDKClientClient();
            //logoutRequest request = new logoutRequest(SerialNo, Key);
            //logoutResponse response = sdkClient.logout(request);
            //if (response.@return == 0)
            //{
            //    return true;
            //}
            //string msg = LogoutException(response.@return);
            //LogHelp.AddErrorLog("亿美短信注销序列号出现异常，【" + msg + "】", "亿美短信接口调用失败", HttpContext.Current.Request);
            return false;

        }
        
        /// <summary>
        /// 发送短信异常信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string SendSMSException(int code)
        {
            string msg = "";
            switch (code)
            {
                case 305:
                    msg = "服务器端返回错误，错误的返回值";
                    break;
                case 101 :
                case 103:
                    msg = "客户端网络故障";
                    break;
                case 307:
                    msg = "目标电话号码不符合规则，电话号码必须是以0、1开头";
                    break;
                case 997:
                    msg = "平台返回找不到超时的短信，该信息是否成功无法确定";
                    break;
                case 998:
                    msg = "由于客户端网络问题导致信息发送超时，该信息是否成功下发无法确定";
                    break;

                case -1:
                    msg = "系统异常";
                    break;
                case -2:
                    msg = "客户端异常";
                    break;
                case -101:
                    msg = "命令不被支持";
                    break;
                case -104:
                    msg = "请求超过限制";
                    break;
                case -117:
                    msg = "发送短信失败";
                    break;

                case -1104:
                    msg = "路由失败，请联系系统管理员";
                    break;
                case -9016:
                    msg = "发送短信包大小超出范围";
                    break;
                case -9017:
                    msg = "发送短信内容格式错误";
                    break;
                case -9018:
                    msg = "发送短信扩展号格式错误";
                    break;
                case -9019:
                    msg = "发送短信优先级格式错误";
                    break;
                case -9020:
                    msg = "发送短信手机号格式错误";
                    break;
                case -9021:
                    msg = "发送短信定时时间格式错误";
                    break;
                case -9022:
                    msg = "发送短信唯一序列值错误";
                    break;

                case -9001:
                    msg = "序列号格式错误";
                    break;
                case -9002:
                    msg = "密码格式错误";
                    break;
                case -9003:
                    msg = "客户端Key格式错误";
                    break;
                case -9025:
                    msg = "客户端请求sdk5超时";
                    break;
                default:
                    msg = "未知错误";
                    break;

            }
            return msg;
        }
        /// <summary>
        ///注册序列号异常信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string RegistExException(int code)
        {
            string msg = "";
            switch (code)
            {
                case 305:
                    msg = "服务器端返回错误，错误的返回值";
                    break;
                case 101:
                case 103:
                    msg = "客户端网络故障";
                    break;
                case 999:
                    msg = "操作频繁";
                    break;

                case -1:
                    msg = "系统异常";
                    break;
                case -2:
                    msg = "客户端异常";
                    break;
                case -101:
                    msg = "命令不被支持";
                    break;
                case -104:
                    msg = "请求超过限制";
                    break;
                case -110:
                    msg = "号码注册激活失败";
                    break;

                case -126:
                    msg = "路由信息失败";
                    break;
                case -190:
                    msg = "数据操作失败";
                    break;
                case -1100:
                    msg = "序列号错误，序列号不存在内存中，或尝试攻击的用户";
                    break;
                case -1103:
                    msg = "序列号Key错误";
                    break;
                case -1102:
                    msg = "序列号密码错误";
                    break;
                case -1104:
                    msg = "路由失败，请联系系统管理员";
                    break;
                case -1105:
                    msg = "注册号状态异常, 未用 1";
                    break;
                case -1107:
                    msg = "注册号状态异常, 停用 3";
                    break;

                case -1108:
                    msg = "注册号状态异常, 停止 5";
                    break;
                case -1901:
                    msg = "数据库插入操作失败";
                    break;

                case -9001:
                    msg = "序列号格式错误";
                    break;
                case -9002:
                    msg = "密码格式错误";
                    break;
                case -9003:
                    msg = "客户端Key格式错误";
                    break;
                case -9025:
                    msg = "客户端请求sdk5超时";
                    break;
                default:
                    msg = "未知错误";
                    break;

            }
            return msg;
        }
        /// <summary>
        /// 注销序列号异常信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string LogoutException(int code)
        {
            string msg = "";
            switch (code)
            {
                case 305:
                    msg = "服务器端返回错误，错误的返回值";
                    break;
                case 101:
                case 103:
                    msg = "客户端网络故障";
                    break;
                case 999:
                    msg = "操作频繁";
                    break;

                case -1:
                    msg = "系统异常";
                    break;
                case -2:
                    msg = "客户端异常";
                    break;
                case -101:
                    msg = "命令不被支持";
                    break;
                case -104:
                    msg = "请求超过限制";
                    break;
                case -122:
                    msg = "号码注销激活失败";
                    break;

                case -126:
                    msg = "路由信息失败";
                    break;
                case -190:
                    msg = "数据操作失败";
                    break;
                case -1100:
                    msg = "序列号错误，序列号不存在内存中，或尝试攻击的用户";
                    break;
                case -1103:
                    msg = "序列号Key错误";
                    break;
                case -1102:
                    msg = "序列号密码错误";
                    break;
                case -1104:
                    msg = "路由失败，请联系系统管理员";
                    break;
        
                case -1902:
                    msg = "数据库更新操作失败";
                    break;

                case -9001:
                    msg = "序列号格式错误";
                    break;
                case -9002:
                    msg = "密码格式错误";
                    break;
                case -9003:
                    msg = "客户端Key格式错误";
                    break;
                case -9025:
                    msg = "客户端请求sdk5超时";
                    break;
                default:
                    msg = "未知错误";
                    break;

            }
            return msg;
        }
    }
}