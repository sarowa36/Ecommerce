class DashboardMenuItemValue{
    icon=""
    text=""
    link=""
    isHr=false
    childItems:Array<DashboardMenuItemValue>=[]
    constructor(p?:DashboardMenuItemValue){
        if(p)
        Object.assign(this,p);
    }
}
export default DashboardMenuItemValue;