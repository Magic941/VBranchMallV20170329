
namespace Maticsoft.Model.Shop.Order
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取的订单状态的类型
        /// </summary>
        /// <remarks>订单组合状态 1 等待付款   | 2 等待处理 | 3 取消订单 | 4 订单锁定 | 5 等待付款确认 | 6 正在处理 |7 配货中  |8 已发货 |9  已完成 </remarks>
        public enum OrderMainStatus
        {
            None = -1,
            /// <summary>
            /// 等待付款
            /// </summary>
            Paying = 1,

            /// <summary>
            /// 等待处理
            /// </summary>
            PreHandle = 2,

            /// <summary>
            /// 取消订单
            /// </summary>
            Cancel = 3,

            /// <summary>
            /// 订单锁定
            /// </summary>
            Locking = 4,

            /// <summary>
            /// 等待付款确认
            /// </summary>
            PreConfirm = 5,

            /// <summary>
            /// 正在处理
            /// </summary>
            Handling = 6,

            /// <summary>
            /// 配货中
            /// </summary>
            Shipping = 7,

            /// <summary>
            /// 已发货
            /// </summary>
            Shiped = 8,
            /// <summary>
            /// 已完成
            /// </summary>
            Complete = 9
        }
        /// <summary>
        /// [订单ShippingStatus字段]获取的配送状态的类型    //  配送状态 0 未发货 | 1 打包中 | 2 已发货 | 3 已确认收货 | 4 拒收退货中 | 5 拒收已退货
        /// </summary>
        public enum ShippingStatus
        {
            None = -1,
            /// <summary>
            /// 未发货
            /// </summary>
            UnShipped = 0,
            /// <summary>
            /// 打包中
            /// </summary>
            Packing = 1,

            /// <summary>
            /// 已发货
            /// </summary>
            Shipped = 2,

            /// <summary>
            /// 已确认收货
            /// </summary>
            ConfirmShip = 3,

            /// <summary>
            /// 拒收退货中
            /// </summary>
            RejectedReturning = 4,

            /// <summary>
            /// 拒收已退货
            /// </summary>
            RejectedReturned = 5

        }

        /// <summary>
        /// [订单PaymentStatus字段]支付状态    //  支付状态 0 未支付 | 1 等待确认 | 2 已支付 | 3 处理中 | 4 支付异常 
        /// </summary>
        public enum PaymentStatus
        {
            None = -1,
            /// <summary>
            /// 未支付
            /// </summary>
            Unpaid = 0,
            /// <summary>
            /// 等待确认
            /// </summary>
            PreConfirm = 1,

            /// <summary>
            /// 已支付
            /// </summary>
            Paid = 2,

            /// <summary>
            /// 处理中
            /// </summary>
            Handling = 3,

            /// <summary>
            /// 支付异常
            /// </summary>
            PayException = 4

        }

        /// <summary>
        /// [订单OrderStatus字段]订单状态    -4 系统锁定   | -3 后台锁定 | -2 用户锁定 | -1 死单（取消） | 0 未处理 | 1 进行中 |2 已完成 
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 系统锁定
            /// </summary>
            SystemLock = -4,
            /// <summary>
            /// 后台锁定
            /// </summary>
            AdminLock = -3,

            /// <summary>
            /// 用户锁定
            /// </summary>
            UserLock = -2,

            /// <summary>
            /// 死单
            /// </summary>
            Cancel = -1,

            /// <summary>
            /// 未处理
            /// </summary>
            UnHandle = 0,
            /// <summary>
            /// 进行中
            /// </summary>
            Handling = 1,

            /// <summary>
            /// 已完成
            /// </summary>
            Complete = 2

        }
        /// <summary>
        /// 网关类型
        /// </summary>
        public enum PaymentGateway
        {
            cod,
            bank,
            other
        }
        /// <summary>
        /// 订单操作名   客户创建订单  100 |系统取消订单  101  |系统支付订单  102 |
        ///  系统配货操作  103 |系统发货操作  104 | 系统完成订单  105  | 系统修改收货信息  106 | 系统变更应付金额 107
        /// 商家取消订单  110 | 商家配货操作  111 | 商家发货操作  112 | 商家完成订单  113 | 商家修改收货信息  114
        ///| 客户取消订单  120 | 客户支付订单  121  | 客户完成订单  122
        /// </summary>
        public enum ActionCode
        {
            #region 系统
            /// <summary>
            /// 系统取消订单
            /// </summary>
            SystemCancel = 101,

            /// <summary>
            /// 系统支付订单
            /// </summary>
            SystemPay = 102,

            /// <summary>
            /// 系统配货操作
            /// </summary>
            SystemPacking = 103,

            /// <summary>
            /// 系统发货操作
            /// </summary>
            SystemShipped = 104,
            /// <summary>
            /// 系统完成订单
            /// </summary>
            SystemComplete = 105,

            /// <summary>
            /// 系统修改收货信息
            /// </summary>
            SystemUpdateShip = 106,

            /// <summary>
            /// 系统变更应付金额
            /// </summary>
            SystemUpdateAmount = 107,
            #endregion

            #region 商家
            /// <summary>
            /// 商家取消订单
            /// </summary>
            SellerCancel = 110,

            /// <summary>
            /// 商家配货操作
            /// </summary>
            SellerPacking = 111,

            /// <summary>
            /// 商家发货操作
            /// </summary>
            SellerShipped = 112,
            /// <summary>
            /// 商家完成订单
            /// </summary>
            SellerComplete = 113,
            /// <summary>
            /// 商家修改收货信息
            /// </summary>
            SellerUpdateShip = 114,
            #endregion

            #region 客户
            /// <summary>
            /// 客户创建订单
            /// </summary>
            CustomersCreateOrder = 100,
            /// <summary>
            /// 客户取消订单
            /// </summary>
            CustomersCancel = 120,
            /// <summary>
            /// 客户支付订单
            /// </summary>
            CustomersPay = 121,
            /// <summary>
            /// 客户完成订单
            /// </summary>
            CustomersComplete = 122,
            #endregion

            #region 代理商
            /// <summary>
            /// 代理商取消订单
            /// </summary>
            AgentCancel = 130,

            /// <summary>
            /// 代理商配货操作
            /// </summary>
            AgentPacking = 131,

            /// <summary>
            /// 代理商发货操作
            /// </summary>
            AgentShipped = 132,
            /// <summary>
            /// 代理商完成订单
            /// </summary>
            AgentComplete = 133,

            /// <summary>
            ///代理商修改收货信息
            /// </summary>
            AgentUpdateShip = 134,
            #endregion

            #region 退货
            /// <summary>
            /// 申请退货
            /// </summary>
            ApplyReturnGoods = 140,
            /// <summary>
            /// 审核退货 - 通过
            /// </summary>
            ApproveReturnGoodsPass = 141,
            /// <summary>
            /// 审核退货 - 拒绝
            /// </summary>
            ApproveReturnGoodsRefuse = 142,
            /// <summary>
            /// 审核退款 - 通过
            /// </summary>
            ApproveReturnRefundPass=143,
            /// <summary>
            /// 审核退款 - 拒绝
            /// </summary>
            ApproveReturnRefundRefuse = 144,
            /// <summary>
            /// 退货方发货
            /// </summary>
            DeliverReturnGoods = 145,
            /// <summary>
            /// 商家确认收货
            /// </summary>
            ReceiptReturnGoods = 146, 
            /// <summary>
            /// 商家拒收退货
            /// </summary>
            RefuseReturnGoods = 147,
            /// <summary>
            /// 商家退款
            /// </summary>
            RefundReturn = 148,

            #endregion
        }

        /// <summary>
        /// 获取商家的订单状态
        /// </summary>
        /// <remarks>订单组合状态 1 等待处理  | 2 未完成 | 3 已完成 </remarks>
        public enum StoreOrderStatus
        {
            /// <summary>
            /// 等待处理
            /// </summary>
            PreHandle = 1,

            /// <summary>
            /// 未完成
            /// </summary>
            NotComplete = 2,

            /// <summary>
            /// 已完成
            /// </summary>
            Complete = 3
        }
        /// <summary>
        /// 提示页面
        /// </summary>
        public enum ReturnTipsType
        {
            /// <summary>
            /// 提示成功
            /// </summary> 
            Success = 0,
            /// <summary>
            /// 失败
            /// </summary>
            Fail = 1,
            /// <summary>
            /// 警告
            /// </summary>
            Warning = 2,
            /// <summary>
            /// 疑问
            /// </summary>
            Doubt = 3
        }

        

        #region  自定义状态订单状态(LiYongQin)
        /// <summary>
        /// [退货单Status字段]申请状态   |-3拒绝 | -2锁定 |  -1取消申请  |   0未处理  |  1处理中| 2 完成|
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// 拒绝
            /// </summary>
            Refuse = -3,

            /// <summary>
            /// 锁定
            /// </summary>
            Lock = -2,

            /// <summary>
            /// 取消
            /// </summary>
            Cancel = -1,

            /// <summary>
            /// 未处理
            /// </summary>
            UnHandle = 0,
            /// <summary>
            /// 处理中
            /// </summary>
            Handling = 1,

            /// <summary>
            /// 已完成
            /// </summary>
            Complete = 2
        }

        /// <summary>
        /// [订单RefundStatus字段和退款单RefundStatus字段共用]退款状态 0 未退款 | 1 申请退款 | 2 退款中 | 3 已退款 | 4 拒绝退款
        /// </summary>
        public enum RefundStatus
        {
            /// <summary>
            /// 未退款
            /// </summary>
            UnRefund = 0,
            /// <summary>
            /// 申请退款
            /// </summary>
            Apply = 1,

            /// <summary>
            /// 退款中
            /// </summary>
            Refunding = 2,

            /// <summary>
            /// 已退款
            /// </summary>
            Refunds = 3,

            /// <summary>
            /// 拒绝
            /// </summary>
            Refuse = 4
        }


        //退货状态   //0 未发货  |    1 已发货   |   2 已收货  |   3 返程中  |  4 客户确定收货
        public enum LogisticStatus
        {
            /// <summary>
            /// 未发货
            /// </summary>
            NODelivery = 0,
            /// <summary>
            /// 已发货
            /// </summary>
            Shipped = 1,
            /// <summary>
            /// 已收货
            /// </summary>
            Receipt = 2,
            /// <summary>
            /// 返程中
            /// </summary>
            Returnjourney = 3,
            /// <summary>
            /// 客户确定收货
            /// </summary>
            storage = 4,
            /// <summary>
            /// 拒绝维修
            /// </summary>
            RefuseRepair = 5,
            /// <summary>
            /// 拒绝调货
            /// </summary>
            RefuseAdjustable = 6
        }

        /// <summary>
        /// 获取申请类型，1退货 2退款
        /// </summary>
        public enum ReturnGoodsType
        { 
            /// <summary>
            /// 非退货退款
            /// </summary>
            None = 0,
            /// <summary>
            /// 退货
            /// </summary>
            Goods =1,
            /// <summary>
            /// 退款
            /// </summary>
            Money =2,
            /// <summary>
            /// 调货
            /// </summary>
            ExchangeGoods= 3,
            /// <summary>
            /// 维修
            /// </summary>
            Repair =4,

        }
        /// <summary>
        /// 获取组合状态
        /// </summary>
        public enum MainStatus
        {
            /// <summary>
            ///  等待审核
            /// </summary>
            Auditing = 1,
            /// <summary>
            ///  取消申请
            /// </summary>
            Cancel = 2,
            /// <summary>
            /// 拒绝
            /// </summary>
            Refuse = 3,
            /// <summary>
            ///  正在处理
            /// </summary>
            Handling = 4,
            /// <summary>
            /// 已发货
            /// </summary>
            Packing = 5,
            /// <summary>
            ///已收货
            /// </summary>
            Returning = 6,
            /// <summary>
            ///等待退款
            /// </summary>
            WaitingRefund = 7,
            /// <summary>
            /// 已退款
            /// </summary>
            Refunding = 8,
            /// <summary>
            /// 已完成
            /// </summary>
            Complete = 9,
            /// <summary>
            /// (退货/维修/调货)审核通过
            /// </summary>
            Kiss = 10,
            /// <summary>
            /// 拒收退货
            /// </summary>
            Return = 11,
            /// <summary>
            /// 拒绝维修
            /// </summary>
            RefuseRepair = 12,
            /// <summary>
            /// 拒绝调货
            /// </summary>
            RefuseAdjustable = 13,
            /// <summary>
            /// 返程中
            /// </summary>
            journey = 14,
            /// <summary>
            /// 客户确认收货
            /// </summary>
            storage = 15
        }
        #endregion

        /// <summary>
        /// 订单细项 退货状态
        /// </summary>
        public enum OrderItemReturnStatus
        {
            /// <summary>
            /// 没有退货 
            /// </summary>
            NoReturn = 0,
            /// <summary>
            /// 部分退货
            /// </summary>
            Part = 1,
            /// <summary>
            /// 全部退货
            /// </summary>
            All = 2
        }

        public enum ReturnApproveStatus
        {
            /// <summary>
            /// 等待审核
            /// </summary>
            NoApprove = 0,
            /// <summary>
            /// 审核不通过
            /// </summary>
            NoPass = 1,
            /// <summary>
            /// 审核通过
            /// </summary>
            Pass = 2
        }

        public enum ReturnAccountStatus
        {
            /// <summary>
            /// 没有退款
            /// </summary>
            NoReturn = 0,
            /// <summary>
            /// 退款失败
            /// </summary>
            ReturnFail = 1,
            /// <summary>
            /// 退款成功
            /// </summary>
            ReturnSuccess = 2
        }

        /// <summary>
        /// 订单明细中的 Returnstatus 0 未申请，1未处理，2处理中，3完成，4拒绝，5取消，6异常
        /// </summary>
        public enum Returnstatus
        { 
            /// <summary>
            /// 未申请
            /// </summary>
            NoApply = 0,
            /// <summary>
            /// 未处理
            /// </summary>
            Untreated = 1,
            /// <summary>
            /// 处理中
            /// </summary>
            Handding = 2,
            /// <summary>
            /// 完成
            /// </summary>
            Complete = 3,
            /// <summary>
            /// 拒绝
            /// </summary>
            Refuse = 4,
            /// <summary>
            /// 取消
            /// </summary>
            Cancel = 5,
            /// <summary>
            /// 异常
            /// </summary>
            Faild = 6,
            
        }
    }
        /// <summary>
        /// 统计模式
        /// </summary>
        public enum StatisticMode
        {
            /// <summary>
            /// 按天统计
            /// </summary>
            Day = 0,

            /// <summary>
            /// 按月统计
            /// </summary>
            Month = 1,

            /// <summary>
            /// 按年统计
            /// </summary>
            Year = 2
        }
}
