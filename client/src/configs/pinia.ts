
import useCartStore from '@/stores/CartStore';
import { useCategoryStore } from '@/stores/CategoryStore';
import { useCitiesAndDistrictsStore } from '@/stores/CitiesAndDistrictsStore';
import { useLoginStore } from '@/stores/LoginStore';
import { createPinia } from 'pinia';
import { type App } from 'vue'

export default async function piniaConfigSetter(app:App<Element>) {
    const pinia=createPinia();

    const category=useCategoryStore(pinia);

    const login=useLoginStore(pinia);

    const cart=useCartStore(pinia);

    const cityAndDistricts=useCitiesAndDistrictsStore(pinia);

   await Promise.all([category.load(),login.loadUser(),cart.loadCart(),cityAndDistricts.loadCitiesAndDistricts()])
    app.use(pinia)
}
