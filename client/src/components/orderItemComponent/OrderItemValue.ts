import { OrderItemStatus } from ".";

export class SelectedVariation {
     id: string = "";
     name: string = "";
     value: string = "";
}
export class OrderItemValue {
     id: number = 0;
     productId: number = 0;
     productName: string = "";
     productImage: string = "";
     price: number = 0;
     quantity: number = 0; 
     orderItemStatus: OrderItemStatus = 0;
     variation:Array<SelectedVariation>=[];
     constructor(p: OrderItemValue | null) {
          if (p)
               Object.assign(this, p);
     }
}
export class OrderItemArray extends Array<OrderItemValue>{};
