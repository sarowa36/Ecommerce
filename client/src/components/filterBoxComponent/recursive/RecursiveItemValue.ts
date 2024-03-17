import { type RouteLocationRaw } from "vue-router";
export class RecursiveItemValue {
    id: number = 0;
    name: string = "";
    link: RouteLocationRaw | string = "";
    childrens: Array<RecursiveItemValue> = [];
    showChildrens:boolean=false;
    constructor(p?: RecursiveItemValue) {
        if (p)
            Object.assign(this, p);
    }
}