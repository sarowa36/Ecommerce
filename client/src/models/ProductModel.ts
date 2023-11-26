import { ModelBase } from "./ModelBase"

class ProductModel extends ModelBase<ProductModel> {
    img: Array<string>
    title: string
    price: string
    star: string
}
export { ProductModel }