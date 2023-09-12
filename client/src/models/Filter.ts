interface IFilter{
    filterName:string;
    icon:string;
    value:Object;
    selectableValues:Object;
    isActive():boolean;
    valueAsString(arg):string
}
class Filter<T,TChoosedValue extends Object> implements IFilter{
    constructor(name:string, _icon:string,
        valueToStringConverter:(val:Filter<T,TChoosedValue>)=>string,
         activeStatement:(val:TChoosedValue)=>boolean) {
        this.icon=_icon;
        this.filterName=name;
        this._valueToStringConverter=valueToStringConverter;
        this._activeStatement=activeStatement;
    }
    private _activeStatement:(val:TChoosedValue)=>boolean
    private _valueToStringConverter:(val:Filter<T,TChoosedValue>)=>string
    filterName:string="";
    icon: string;
    value:TChoosedValue;
    selectableValues:Array<T>=[];
    isActive(){
        return this._activeStatement(this.value)
    }
    valueAsString(){ 
        return this._valueToStringConverter(this)
    }
    
    
}
function arraystringConverter(val:Filter<string,Array<string>>):string{
    if(val.value !=null && val.value.length >0)
         return val.value.join(", ")
        else 
        return val.filterName
}
export {Filter,IFilter,arraystringConverter};