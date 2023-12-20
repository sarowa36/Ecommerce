class ProductComponentValue {
    img: Array<string>
    title: string
    price: string
    star: string
    constructor(p?:ProductComponentValue){
        if(p)
        Object.assign(this,p)
    }
}
export default ProductComponentValue