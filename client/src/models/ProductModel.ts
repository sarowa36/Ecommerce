class ProductModel {
    img: Array<string>
    title: string
    price: string
    star: string
    constructor(p?:ProductModel){
        if(p)
        Object.assign(this,p)
    }
}
export { ProductModel }