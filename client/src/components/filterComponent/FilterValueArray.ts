import {FilterValue} from ".";

class FilterValueArray extends Array<FilterValue>{

    public GetSelectedValues() {
        return this.filter(x=>x.selected)
    }
}
export  {FilterValueArray};