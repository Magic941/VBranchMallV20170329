namespace Maticsoft.Payment.Model
{
    public enum TradeTypes
    {
        NotSet,
        SelfHelpRecharge,   //自助充值
        BackgroundRecharge, //后台充值
        SaveAccount,    //帐号存入
        DrawAccount,    //帐号提现
        TakePercentage, //比例提现
        Consume,    //消费
        DrawRequest,    //提现请求
        RefundOrder //订单退款
    }
}