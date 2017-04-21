using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.BLL.SysManage
{
    public partial class OrderAutoAction
    {
        public static OrderAutoAction _autoAction = null;
        public static int TimerInterval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["TimerInterval"]);
        public System.Timers.Timer OrderAutoCompleteTime = new System.Timers.Timer(TimerInterval*60*1000);
        private static object symObj = new object();
        private OrderAutoAction()
        {
            OrderAutoCompleteTime.Elapsed += new System.Timers.ElapsedEventHandler(OrderAutoCompleteTime_Elapsed);
            OrderAutoCompleteTime.Enabled = true;
            //OrderAutoCompleteTime.Start();
            OrderAutoCompleteTime.AutoReset = true;
        }

        public static OrderAutoAction GetInstance()
        {
            if (_autoAction!=null)
            {
                lock (symObj)
                {
                    if (_autoAction!=null)
                    {
                        _autoAction = new OrderAutoAction();
                    }
                }
            }
            return _autoAction;
        }


        void OrderAutoCompleteTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (symObj)
            {
                try
                {
                    string checkTime = System.Configuration.ConfigurationManager.AppSettings["EveryDayCheckTime"];
                    string[] times = checkTime.Split('-');
                    //订单自动完成
                    if (DateTime.Now >= DateTime.Parse(times[0]) && DateTime.Now <= DateTime.Parse(times[1]))
                    {
                        double TimeLimit = double.Parse(System.Configuration.ConfigurationManager.AppSettings["AutoCompleteTimeLimit"]);
                        Maticsoft.BLL.Shop.Order.OrderAction _OrderInfoBll = new BLL.Shop.Order.OrderAction();
                        List<Maticsoft.Model.Shop.Order.OrderAction> OrderList = _OrderInfoBll.GetNoCompleteOrder();
                        if (OrderList.Count != 0)
                        {
                            foreach (var item in OrderList)
                            {
                                DateTime LimitTime = item.ActionDate.AddDays(TimeLimit);
                                if (LimitTime < DateTime.Now)
                                {
                                    Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
                                    long orderId = Maticsoft.Common.Globals.SafeLong(item.OrderId, 0);
                                    Maticsoft.Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
                                    if (orderInfo != null)
                                    {
                                        BLL.Shop.Order.OrderManage.SystemCompleteOrder(orderInfo, null);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogTxt.GetInstance("订单自动完成日志").Write(ex.Message);
                }
                //订单自动取消
                Maticsoft.BLL.Shop.Order.Orders _orderBll = new BLL.Shop.Order.Orders();
                List<Maticsoft.Model.Shop.Order.OrderInfo> _orderList = _orderBll.GetListByStatus(Model.Shop.Order.EnumHelper.OrderMainStatus.Paying);

                if (_orderList.Count != 0)
                {
                    double TimeLimit = double.Parse(System.Configuration.ConfigurationManager.AppSettings["PaymentTimeLimit"]);
                    try
                    {
                        foreach (var item in _orderList)
                        {
                            DateTime LimitTime = item.CreatedDate.AddMinutes(TimeLimit);
                            if (LimitTime < DateTime.Now)
                            {
                                Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
                                long orderId = Maticsoft.Common.Globals.SafeLong(item.OrderId, 0);
                                Maticsoft.Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
                                if (orderInfo != null)
                                {
                                    BLL.Shop.Order.OrderManage.SystemCancelOrder(orderInfo, null);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogTxt.GetInstance("订单自动取消日志").Write(ex.Message);
                    }
                }
            }

        }
    }
}
