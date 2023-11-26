import { ModelBase } from "./ModelBase";
import { ProductModel } from "./ProductModel";

class OrderModel extends ModelBase<OrderModel> {
    id: string
    orderStatus: OrderStatus
    date: Date
    products: Array<ProductModel>
    addressName: string
    addressDetail: string
    addressCity: string
    addressZip: string
    addressCountry: string
    targetPerson: string
    targetPhone: string
    cargoAmount: number
    amount: number
    discount: number
    totalAmount: number
}
enum OrderStatus {
    waitingApprove="Order waiting approve",
    onDelivery="Order on delivery",
    deliverySuccess="Order is done",
    deliveryCancel="Order is cancalled"
}
export { OrderModel, OrderStatus };