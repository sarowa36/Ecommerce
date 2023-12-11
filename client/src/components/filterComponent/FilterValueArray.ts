import FilterValue from "./FilterValue";

class FilterValueArray extends Array<FilterValue>{

    public GetSelectedValues() {
        return this.filter(x=>x.selected)
    }
}
export default FilterValueArray;