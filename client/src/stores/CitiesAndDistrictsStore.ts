import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'

class City {
    id: number;
    name: string;
    districts: Array<District>;
    constructor(p: City | null) {
        if (p)
            Object.assign(this, p);
    }
}
class District {
    id: number;
    name: string;
}

const useCitiesAndDistrictsStore = defineStore('citiesAndDistrictsStore', {
    state: () => {
        return {
            values: Array<City>()
        }
    },
    actions: {
        async loadCitiesAndDistricts() {
            var response = await axios.get("/CitiesAndDistricts.json", { baseURL: "" })
            if (response.isSuccess) {
                response.data.forEach(element => {
                    this.values.push(new City(element))
                });
            }
        },
        getCity(id:number): City {
            return this.values.filter(x => x.id == id)[0];
        },
        getDistrict(cityId:number,districtId:number): District {
            return this.values.filter(x => x.id == cityId)[0].districts.filter(x=>x.id==districtId)[0];
        }
    }
})

export { useCitiesAndDistrictsStore }