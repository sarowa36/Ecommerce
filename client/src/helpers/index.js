function createRandomNumber(){
    return parseInt(Math.random().toString().replace("0.","").substr(0,8));
}
function dateToDateTimeString(date){
    var str=date.toLocaleString();
    str=str.substring(0,str.length -3)
    return str;
}
export {createRandomNumber,dateToDateTimeString};