enum OrderItemStatus{
    NotOnRefundProccess,
    OnRefundProccess,
    Refunded
}
function OrderItemStatusDescriber(p:OrderItemStatus):string{
    switch(p){
        case OrderItemStatus.NotOnRefundProccess:
            return "Order Item not on refund proccess";
        case OrderItemStatus.OnRefundProccess:
            return "Order Item on refund proccess";
        case OrderItemStatus.Refunded:
            return "Order Item is refunded";
    }
}
export default OrderItemStatus;
export {OrderItemStatus,OrderItemStatusDescriber}