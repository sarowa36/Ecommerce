import { defineStore } from 'pinia'
import axios from 'axios'
class CartItem {
  productId: number = 0;
  productImage: string = "";
  productName: string = "";
  productPrice: number = 0;
  quantity: number = 0;
  constructor(p?: CartItem) {
    Object.assign(this, p);
  }
}
const useCartStore = defineStore('cart', {
  state: () => {
    return {
      items:new Array<CartItem>()
    }
  },
  actions: {
    async loadCart() {
      var res = await axios.get('User/ShoppingCart/GetList');
      this.items=new Array<CartItem>();
      if (res.data) {
        res.data.forEach(element => {
          this.items.push(new CartItem(element));
        });
      }
    }
  }
})

export { CartItem,useCartStore }
export default useCartStore;