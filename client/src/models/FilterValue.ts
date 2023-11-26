import { ModelBase } from "./ModelBase"
//value:"XS",text:"XS",selected:false
class FilterValue extends ModelBase<FilterValue> {
    constructor(p?:FilterValue){
        super(p);
        if(this.value && !this.text){
            this.text=this.value;
        }
    }
value:string
text:string |null
selected=false
}
export default FilterValue;