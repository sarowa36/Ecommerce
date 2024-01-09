enum OrderStatus {
    PaymentFail,
    WaitingApprove,
    ApprovedAndPreparing,
    Ignored,
    OnCargo,
    Delivered,
}
function OrderStatusDescriber(p: OrderStatus):string {
    switch (p) {
        case OrderStatus.PaymentFail:
            return "Payment Failed";
        case OrderStatus.WaitingApprove:
            return "Waiting Approve";
        case OrderStatus.ApprovedAndPreparing:
            return "Your Order Approved And Preparing";
        case OrderStatus.Delivered:
            return "Your Order Delivered";
        case OrderStatus.Ignored:
            return "Your Order Ignored";
        case OrderStatus.OnCargo:
            return "Your Order On Cargo";
    }
}
export default OrderStatus;
export { OrderStatus,OrderStatusDescriber };