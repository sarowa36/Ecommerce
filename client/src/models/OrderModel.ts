import { ProductComponentValue } from "@/components/productComponent/";
import OrderStatus from "../enums/OrderStatus";

class OrderModel {
    id: string
    orderStatus: OrderStatus
    date: Date
    products: Array<ProductComponentValue>
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