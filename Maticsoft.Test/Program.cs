using Maticsoft.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maticsoft.Services;
using Maticsoft.BLL.Shop.Card;
using System.Security.Cryptography;
using Maticsoft.Model.Shop;



namespace Maticsoft.Test
{
    class Program
    {
        private static readonly string path = "HaolinShop.MSMQMessaging";
        static void Main(string[] args)
        {
            CouponServices sc = CouponServices.GetInstance();
            sc.SendMessageQueue<string>(path, "队列消息测试");
            sc.SendMessageQueue<string>(path, "队列消息测试1");
            sc.SendMessageQueue<string>(path, "队列消息测试2");
            sc.SendMessageQueue<string>(path, "队列消息测试3");

            Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt ext;

            List<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt> t = new List<Model.Shop.Coupon.Shop_CouponRuleExt>();

            for (int i = 0; i < 50; i++)
            {
                ext = new Model.Shop.Coupon.Shop_CouponRuleExt();
                ext.UserID = i;
                ext.ClassID = 1;
                ext.CouponCount = 1;
                t.Add(ext);
            }

            sc.SendMessageQuque<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(t, path);

            //var c =    sc.ReceiveMessage<string>(path);

            //Console.WriteLine(c);


            Console.ReadKey();
            //Shop_CardUserInfo user = new Shop_CardUserInfo();
            //user.CardNo = "HL2000YAA444000007";
            //user.BirthDay = DateTime.Today;
            //user.CREATEDATE = DateTime.Today;
            //user.Address = "shanghai";
            //user.IsMainCard = "true";

            //UserCardLogic uc = new UserCardLogic();
            //uc.baseuri = "http://card.haolinchina.cn";


            //var helper = new APIHelper("http://192.168.0.181:8089");

            //helper.UpdateUserName("yantingzhen", "yantingzhen");
            //string message="";
            //var y = uc.CheckCardInfo("hlya000200027252", "26231306", out message);
            //Console.WriteLine(y);
            //var x = uc.CheckCardTypeInfo(y.CardTypeNo, out  message);
            //Console.WriteLine(x);

             //var x =  uc.GetCardBatch();
             //for (int i = 0; i < x.Count; i++)
             //{
             //    Console.WriteLine(x[i]);
             //}
            //var x =   uc.ActiveUserInfo(user);
           // test();

            //BLL.Shop.PromoteSales.GroupBuy buy = new BLL.Shop.PromoteSales.GroupBuy();
            //var z =buy.GetGroupBuyLimit(562275, 5571);
            //Console.WriteLine(z);


            //var x = EncryptPassword("1");
            //foreach (var item in x)
            //{
            //    Console.WriteLine(item); 
            //}

            //Console.ReadKey();

        }

        public static  void test()
        {
            IMessage message = JZMessage.getInstance();
            var x= message.SendSMS("15800800621", "hello world!【上海好电子商务有限公司】","sdk_haolin","haolin!23");
            Console.WriteLine(x);
            Console.ReadKey();
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        public static byte[] EncryptPassword(string password)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes = encoding.GetBytes(password);
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] cryptPassword = sha1.ComputeHash(hashBytes);
            return cryptPassword;
        }
    }
}
