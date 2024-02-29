enum OrderStatus {
    PaymentFail,
    WaitingApprove,
    ApprovedAndPreparing,
    Ignored,
    OnCargo,
    Delivered,
    Cancelled
}
function OrderStatusDescriber(p: OrderStatus): string {
    switch (p) {
        case OrderStatus.PaymentFail:
            return "Payment Failed";
        case OrderStatus.WaitingApprove:
            return "Waiting Approve";
        case OrderStatus.ApprovedAndPreparing:
            return "Order Approved And Preparing";
        case OrderStatus.Delivered:
            return "Order Delivered";
        case OrderStatus.Ignored:
            return "Order Ignored";
        case OrderStatus.OnCargo:
            return "Order On Cargo";
            case OrderStatus.Cancelled:
            return "Order Cancelled";
    }
}
export default OrderStatus;
export { OrderStatus, OrderStatusDescriber };