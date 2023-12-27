import { defineStore } from 'pinia'
import axios, { type AxiosResponse } from 'axios'
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
      items: new Array<CartItem>()
    }
  },
  actions: {
    async loadCart() {
      const loginStore = useLoginStore();
      let res: AxiosResponse<any, any>;
      if (loginStore.isLogged) {
        res = await axios.get('User/ShoppingCart/GetList');
      }
      else {
        res = await axios.get('Anonym/ShoppingCart/GetList');
      }
      this.items = new Array<CartItem>();
      if (res.data) {
        res.data.forEach((element: object) => {
          this.items.push(new CartItem(element));
        });
      }
    },
    async updateCartItem(productId: number, quantity: number): Promise<AxiosResponse<any,any>> {
      const loginStore = useLoginStore();
      let path = "";
      if (loginStore.isLogged)
        path = "User/ShoppingCart/Write";
      else
        path = "Anonym/ShoppingCart/Write";
      return await axios.postForm(path, { productId, quantity });
    }
  }
})

export { CartItem, useCartStore }
export default useCartStore;