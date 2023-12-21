class ProductComponentValue {
    id:number=0;
    image: Array<string>=[];
    name: string="";
    price: string="";
    star: string="5.0";
    constructor(p?:ProductComponentValue){
        if(p)
        Object.assign(this,p)
    }
}
export default ProductComponentValue