import { ModelBase } from "./ModelBase"

class ProductModel extends ModelBase {
    img: Array<string>
    title: string
    price: string
    star: string
}
export { ProductModel }