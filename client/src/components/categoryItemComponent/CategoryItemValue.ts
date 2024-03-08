export class CategoryItemValue{
  id:number=0;
  name:string="";
  childrens:Array<CategoryItemValue>=[];
  constructor(p?:CategoryItemValue){
    if(p)
    Object.assign(this,p);
}
}
export class CategoryItemArray extends Array<CategoryItemValue>{}