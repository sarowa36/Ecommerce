import { ProductModel } from "../components/productComponent/ProductComponentValue";
import OrderStatus from "../enums/OrderStatus";

class OrderModel {
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
    constructor(p?:OrderModel){
        if(p)
        Object.assign(this,p)
    }
}
export default OrderModel ;