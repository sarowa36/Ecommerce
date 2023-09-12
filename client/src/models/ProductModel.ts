class ProductModel{
    img:string
    title:string
    price:string
    star: string
    
    constructor(p:object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
    }
}
export {ProductModel}