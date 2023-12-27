import { defineStore } from 'pinia'
import axios from 'axios'
import { useLoginStore } from './LoginStore';

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
      const loginStore=useLoginStore();
      if(loginStore.isLogged){
      var res = await axios.get('User/ShoppingCart/GetList');
      }
      else{
        var res = await axios.get('Anonym/ShoppingCart/GetList');
      }
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