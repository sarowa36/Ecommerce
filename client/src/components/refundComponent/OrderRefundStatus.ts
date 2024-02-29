enum OrderRefundStatus {
    WaitingApprove,
ApprovedAndWaitingDelivery,
Ignored,
Accepted,
Canceled
}
function OrderRefundStatusDescriber(p: OrderRefundStatus): string {
    switch (p) {
        case OrderRefundStatus.WaitingApprove:
            return "Waiting Approve";
        case OrderRefundStatus.ApprovedAndWaitingDelivery:
            return "Refund Approved And Waiting Delivery";
        case OrderRefundStatus.Accepted:
            return "Refund Accepted";
        case OrderRefundStatus.Ignored:
            return "Refund Ignored";
        case OrderRefundStatus.Canceled:
            return "Refund Canceled";
    }
}
export default OrderRefundStatus;
export { OrderRefundStatus, OrderRefundStatusDescriber };