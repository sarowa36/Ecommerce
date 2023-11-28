import { ModelBase } from "./ModelBase";
import { ProductModel } from "./ProductModel";
import OrderStatus from "../enums/OrderStatus";

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
export default OrderModel ;