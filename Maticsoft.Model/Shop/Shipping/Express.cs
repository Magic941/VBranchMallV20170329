using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Security.Cryptography;

namespace Maticsoft.Model.Shop.Shipping
{
    public class ExpressTrace
    {
        /// <summary>
        /// 监控状态:polling:监控中，shutdown:结束，abort:中止，updateall：重新推送。其中当快递单为已签收时status=shutdown，当message为“3天查询无记录”或“60天无变化时”status= abort ，对于stuatus=abort的状度，需要增加额外的处理逻辑
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 包括got、sending、check三个状态，由于意义不大，已弃用，请忽略
        /// </summary>
        public string billstatus { get; set; }

        /// <summary>
        /// 监控状态相关消息，如:3天查询无记录，60天无变化
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 签名字符串，=MD5(param+salt)，param+salt为订阅参数
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 接收时间
        /// </summary>
        public string receivetime { get; set; }

        /// <summary>
        /// 最新查询结果
        /// </summary>
        public LastResult lastResult { get; set; }
    }

    public class LastResult
    {
        /// <summary>
        /// 消息体
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 快递单当前签收状态，包括0在途中、1已揽收、2疑难、3已签收、4退签、5同城派送中、6退回、7转单等7个状态
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 通讯状态，请忽略
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 快递单明细状态标记，暂未实现，请忽略
        /// </summary>
        public string condition { get; set; }

        /// <summary>
        /// 是否签收标记，明细状态请参考state字段
        /// </summary>
        public string ischeck { get; set; }

        /// <summary>
        /// 快递公司编码,一律用小写字母
        /// </summary>
        public string com { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string nu { get; set; }

        /// <summary>
        /// 最新快递数据
        /// </summary>
        public List<LastData> data { get; set; }
    }

    public class LastData
    {
        /// <summary>
        /// 物流动态信息
        /// </summary>
        public string context { get; set; }

        /// <summary>
        /// 物流动态发生时间
        /// </summary>
        public string time { get; set; }

        /// <summary>
        /// 物流动态发生时间（格式化后）
        /// </summary>
        public string ftime { get; set; }

        /// <summary>
        /// 行政区划编码,如："330100000000"
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 行政区划地址，如："浙江,杭州市"
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// 状态，如："在途"
        /// </summary>
        public string status { get; set; }
    }

    public class SubscriptionResult
    {
        /// <summary>
        /// 订阅结果："true"表示成功，false表示失败
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 订阅状态，状态值如下：
        /// 200: 提交成功 
        /// 701: 拒绝订阅的快递公司 
        /// 700: 订阅方的订阅数据存在错误（如不支持的快递公司、单号为空、单号超长等）
        /// 600: 您不是合法的订阅者（即授权Key出错） 
        /// 500: 服务器错误（即快递100的服务器出理间隙或临时性异常，有时如果因为不按规范提交请求，比如快递公司参数写错等，也会报此错误）
        /// 501:重复订阅
        /// </summary>
        public string returnCode { get; set; }

        /// <summary>
        /// 订阅结果说明
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 订阅结果接收时间
        /// </summary>
        public string receivetime { get; set; }
    }

    public class Shop_Express
    {
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressCode { get; set; }

        /// <summary>
        /// 发货城市
        /// </summary>
        public string FromAddress { get; set; }

        /// <summary>
        /// 收货城市
        /// </summary>
        public string ToAddress { get; set; }

        /// <summary>
        /// 快递类型
        /// </summary>
        public string EType { get; set; }

        /// <summary>
        /// 快递名称
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 快递信息
        /// </summary>
        public string ExpressContent { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 是否解析行政区域编码：0：不解析，1：解析
        /// </summary>
        public string ResultV2 { get; set; }

        /// <summary>
        /// 签收状态
        /// </summary>
        public string IsCheck { get; set; }

        /// <summary>
        /// 签名字符串
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否使用签名
        /// </summary>
        public string UseSign { get; set; }
    }

    /// <summary>
    /// 公用方法类
    /// </summary>
    public class comm
    {
        /// <summary>
        /// 将json字符串转化为方法实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T jsonObject = (T)ser.ReadObject(ms);
            ms.Close();
            return jsonObject;
        }

        /// <summary>
        /// 将信息写入到txt文件
        /// </summary>
        /// <param name="Value">写入文件内容</param>
        public static void Write(string Value)
        {
            string strDestination = @"/values.txt";
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + strDestination;
            string dirPath = Path.GetDirectoryName(filePath);
            if (!File.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            FileStream fs = new FileStream(filePath, FileMode.Append);
            byte[] data = System.Text.Encoding.Default.GetBytes(Value);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }

        public static string GetConfig(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null || !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[key].ToString().Trim()))
            {
                return ConfigurationManager.AppSettings[key].ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        public static string Encryption4MD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.UTF8.GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
    }
}
