namespace Maticsoft.Payment.Model
{
    public enum OrderActions
    {
        BUYER_CANCEL = 8,
        BUYER_CANCEL_REFUND = 3,
        BUYER_CONFIRM_GOODS = 4,
        BUYER_MODIFY_DELIVER_ADDRESS = 5,
        BUYER_MODIFY_PAYMENT_MODE = 6,
        BUYER_MODIFY_SHIPPING_MODE = 7,
        BUYER_PAY = 1,
        BUYER_REFUND = 2,
        SELLER_ACCEPT_REFUND = 0x12,
        SELLER_CLOSE_TRADE = 14,
        SELLER_CONFIRM_PAY = 12,
        SELLER_FINISH_TRADE = 0x13,
        SELLER_MODIFY_TRADE = 15,
        SELLER_PACK_GOODS = 11,
        SELLER_REJECT_REFUND = 0x11,
        SELLER_SEND_GOODS = 10
    }
}