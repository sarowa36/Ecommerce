class OrderRefundItemValue{
     id:number=0;
     productId:number=0;
     productName:string="";
     productImage:string="";
     price:number=0;
     quantity:number=0;
     constructor(p: OrderRefundItemValue | null) {
          if (p)
               Object.assign(this, p);
     }
}
export default OrderRefundItemValue;
export {OrderRefundItemValue};