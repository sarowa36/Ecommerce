class DashboardMenuItemValue{
    icon=""
    text=""
    link:object | string="";
    isHr=false
    childItems:Array<DashboardMenuItemValue>=[]
    constructor(p?:DashboardMenuItemValue){
        if(p)
        Object.assign(this,p);
    }
}
export {DashboardMenuItemValue};