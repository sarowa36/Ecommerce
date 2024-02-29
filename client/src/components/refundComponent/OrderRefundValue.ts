import {OrderItemValue} from "@/components/orderItemComponent/";
import OrderRefundStatus from "./OrderRefundStatus";

class OrderRefundValue {
     id:number  =0
     refundItems:Array<OrderItemValue>=[]
     totalRefundAmount:number=0
     createDate:Date=new Date()
     orderRefundStatus:OrderRefundStatus=OrderRefundStatus.WaitingApprove;
     userMessage:string="";
     sellerResponse:string="";
     cargoCode:string="";
     constructor(p?: OrderRefundValue | null) {
          if (p){
               Object.assign(this, p);
               if(typeof p.createDate==="string"){
                    this.createDate=new Date(p.createDate);
               }
          }
     }
}
export default OrderRefundValue;
export { OrderRefundValue };