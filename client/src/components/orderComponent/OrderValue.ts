import {OrderItemValue} from "@/components/orderItemComponent/";
import OrderStatus from "./OrderStatus";
class OrderAddress{
     id: number | null = null;
     name: string = "";
     cityId: number | null = null;
     districtId: number | null = null;
     zip: string = "";
     detail: string = "";
     constructor(p?: OrderAddress | null) {
         if (p)
             Object.assign(this, p)
     }
}
class OrderValue {
     id: number = 0
     orderItems: Array<OrderItemValue> = []
     totalPrice: number = 0;
     discount: number = 0;
     paidPrice: number = 0;
     cargoCode: string = "";
     orderStatus: OrderStatus = OrderStatus.PaymentFail;
     createDate: Date = new Date();
     address:OrderAddress=new OrderAddress();
     targetPerson: string = "";
     targetPhone: string = "";
     constructor(p?: OrderValue | null) {
          if (p){
               Object.assign(this, p);
               if(typeof p.createDate==="string"){
                    this.createDate=new Date(p.createDate);
               }
          }
     }
}
export default OrderValue;
export { OrderValue };