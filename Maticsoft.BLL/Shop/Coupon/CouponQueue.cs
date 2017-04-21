using Maticsoft.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Maticsoft.BLL.Shop.Coupon
{
    public class CouponQueue
    {
        private static readonly string path = "HaolinShop.Queue";
        private static object locker = new object();
        public static readonly CouponQueue queue = new CouponQueue();

        static private int ThreadNumber = 1;  
        static private Thread[] ThreadArray = new Thread[ThreadNumber];

        public void StartThreads()
        {
            //int counter;
            //for (counter = 0; counter < ThreadNumber; counter++)
            //{
            //    ThreadArray[counter] = new Thread(new ThreadStart(MSMQListen));
            //    ThreadArray[counter].Start();                
            //}
            ThreadPool.QueueUserWorkItem(new WaitCallback(MSMQListen));
        }

        private void MSMQListen(object para)
        {
   
           var ext = new Shop_CouponRuleExt();


            while (true)
            {
                //var x = sc.ReceiveAllMessage<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(path);

                //Maticsoft.Common.ErrorLogTxt.GetInstance("优惠券队列读取日志11111").Write(x.Count.ToString());
                var sc = Maticsoft.Services.CouponServices.GetInstance();
                var s = sc.ReceiveMessage<Maticsoft.Model.Shop.Coupon.Shop_CouponRuleExt>(path);

                lock (locker)
                {

                    Maticsoft.Common.ErrorLogTxt.GetInstance("优惠券队列读取日志").Write("读取队列用户id为:" + s.UserID);

                    try
                    {
                        if (s != null)
                        { 
                           var r =  ext.SetUserCoupon(s.UserID, s.CouponCount, s.ClassID);
                           Maticsoft.Common.ErrorLogTxt.GetInstance("优惠券发送结果00000").Write("用户id为: "+s.UserID+"_优惠券类别为: "+s.ClassID+"_优惠劵数量为: "+s.CouponCount+"_执行结果为: "+r.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        Maticsoft.Common.ErrorLogTxt.GetInstance("优惠券读取队列异常444444").Write("异常信息为: " + e.Message);
                        throw e;
                    }

                }
            }
        }

    }
}
