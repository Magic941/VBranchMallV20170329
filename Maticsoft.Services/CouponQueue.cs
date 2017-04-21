using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Maticsoft.Model.Shop.Coupon;


namespace Maticsoft.Services
{
    public class CouponQueue
    {
        private static readonly string path = "HaolinShop.Queue";

        static private int ThreadNumber = 5;  
        static private Thread[] ThreadArray = new Thread[ThreadNumber];

        private void StartThreads()
        {
            int counter;
            for (counter = 0; counter < ThreadNumber; counter++)
            {
                ThreadArray[counter] = new Thread(new ThreadStart(MSMQListen));
                ThreadArray[counter].Start();                
            }
        }

        private void MSMQListen()
        {
            var sc = new CouponServices();
            
            while (true)
            {
              var s = sc.ReceiveMessage<Shop_CouponRuleExt>(path);
              
            }
        }

    }
}
