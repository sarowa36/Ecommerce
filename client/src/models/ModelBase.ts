class ModelBase<T>{
    constructor(p?: T) {
        if (p) {
            Object.entries(p).forEach(item => {
                this[item[0]] = item[1];
            })
        }
    }
}
export {ModelBase};