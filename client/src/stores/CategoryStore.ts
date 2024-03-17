import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'

class Category {
    id: number = 2
    name: string = ""
    parentId: number | null = null;
    childrens: Array<Category> = []
    isDeleted: boolean = false
    sortIndex: number = 0;
    constructor(p: Category | null) {
        if (p)
            Object.assign(this, p);
    }
}

const useCategoryStore = defineStore('categoryStore', {
    state: () => {
        return {
            values: Array<Category>()
        }
    },
    actions: {
        async load() {
            var response = await axios.get<(Array<Category>)>("Anonym/Category/GetList")
            if (response.isSuccess) {
                this.values = response.data;
            }
        },
        parentCategories(id: number, includeSelf: boolean = true): Array<Category> {
            var returnValue = new Array<Category>();
            if (id && this.values.length > 0) {
                let categoryId: number | null = id;
                do {
                    let category = this.values.filter(x => x.id == categoryId)[0]
                    if (!(categoryId == id && !includeSelf))
                        returnValue.push(category);
                    categoryId = category.parentId;
                }
                while (categoryId != null);
            }
            returnValue.reverse();
            return returnValue;
        },
        childCategories(id: number, includeSelf: boolean = true): Array<Category> {
            var returnValue = [];
            if (id && this.values.length > 0) {
                var categoryId = id;
                if (includeSelf)
                    returnValue.push(this.values.filter(x => x.id == categoryId)[0])
                let childs = this.values.filter(x => x.parentId == categoryId)
                childs.forEach(x => {
                    returnValue.push(x);
                    if (this.values.some(y => y.parentId == x.id)) {
                        returnValue = returnValue.concat(this.childCategories(x.id, false))
                    }
                })
            }
            return returnValue;
        },
        categoriesWithParentNams(id:number):Array<Category>{
            var ary = [];
            if (id) {
                ary = this.childCategories(id);
                ary = this.values.filter(item => !ary.some(child => child.id == item.id));
            }
            else
                ary = this.values;
            ary=JSON.parse(JSON.stringify(ary))
            ary.forEach(item => {
                var parentList = this.parentCategories(item.id);
                if (parentList.length >= 4) {
                   item.name= parentList.slice(0, 2).concat(parentList.slice(parentList.length - 2, parentList.length)).map(x => x.name).join("/")
                }
                else{
                    item.name= parentList.map(x => x.name).join("/")
                }
            })
            return ary;
        },
        get(id: number): Category {
            return this.values.filter(x => x.id == id)[0];
        }
    },
    getters: {

    }
})

export { useCategoryStore }