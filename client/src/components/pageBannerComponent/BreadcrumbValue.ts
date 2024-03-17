import { type RouteLocationRaw } from "vue-router";
export class BreadcrumbValue{
    text:String="";
    link: RouteLocationRaw | string = "";
    constructor(p?: BreadcrumbValue) {
        if (p)
            Object.assign(this, p);
    }
}