import { defineStore } from 'pinia'
import axios, { type AxiosResponse } from 'axios'
import { useLoginStore } from './LoginStore';

class SelectedVariation{
  id:string="";
  name:string="";
  value:string="";
  constructor(p?: CartItem) {
    Object.assign(this, p);
  }
}
class CartItem {
  id:number=0;
  productId: number = 0;
  productImage: string = "";
  productName: string = "";
  productPrice: number = 0;
  quantity: number = 0;
  variation:Array<SelectedVariation>=[];
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
      if (res.isSuccess && res.data.length>0) {
        res.data.forEach((element: object) => {
          this.items.push(new CartItem(element));
        });
      }
    },
    async addOrUpdate(productId: number, quantity: number,variation:object): Promise<AxiosResponse<any,any>> {
      const loginStore = useLoginStore();
      let path = "";
      if (loginStore.isLogged)
        path = "User/ShoppingCart/AddOrUpdate";
      else
        path = "Anonym/ShoppingCart/AddOrUpdate";
      return await axios.postForm(path, { productId, quantity,variation });
    },
    async updateQuantity(cartId: number, quantity: number): Promise<AxiosResponse<any,any>> {
      const loginStore = useLoginStore();
      let path = "";
      if (loginStore.isLogged)
        path = "User/ShoppingCart/UpdateQuantity";
      else
        path = "Anonym/ShoppingCart/UpdateQuantity";
      return await axios.postForm(path, { cartId, quantity });
    },
    async delete(cartId: number): Promise<AxiosResponse<any,any>> {
      const loginStore = useLoginStore();
      let path = "";
      if (loginStore.isLogged)
        path = "User/ShoppingCart/Delete";
      else
        path = "Anonym/ShoppingCart/Delete";
      return await axios.postForm(path, { cartId });
    },
  }
})

export { CartItem, useCartStore }
export default useCartStore;