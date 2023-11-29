
class FilterValue {
    value: string;
    text: string | null;
    selected: boolean = false;
    constructor(p?: FilterValue) {
        if (p)
            Object.assign(this, p);
        if (this.value && !this.text) {
            this.text = this.value;
        }
    }


}
export default FilterValue;