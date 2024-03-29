export function createRandomNumber(){
    return parseInt(Math.random().toString().replace("0.","").substr(0,8));
}
export function dateToDateTimeString(date:Date){
    let str=date.toLocaleString();
    str=str.substring(0,str.length -3)
    return str;
}

export function enumToKeyArray(e:object):Array<any>{
let enumNames=Object.keys(e);
enumNames=enumNames.slice(enumNames.length/2,enumNames.length);
return enumNames.map(x=>e[x]);
}
