class ModelBase<T>{
    constructor(p?: T) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
    }
}
export {ModelBase};