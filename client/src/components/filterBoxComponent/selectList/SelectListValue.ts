export class SelectListValue{
    name:String="";
    value:any;
    constructor(p?: SelectListValue) {
        if (p)
            Object.assign(this, p);
    }
}