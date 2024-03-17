
import useCartStore from '@/stores/CartStore';
import { useCategoryStore } from '@/stores/CategoryStore';
import { useCitiesAndDistrictsStore } from '@/stores/CitiesAndDistrictsStore';
import { useLoginStore } from '@/stores/LoginStore';
import { createPinia } from 'pinia';
import { type App } from 'vue'

export default async function piniaConfigSetter(app:App<Element>) {
    const pinia=createPinia();

    const category=useCategoryStore(pinia);
    await category.load()

    const login=useLoginStore(pinia);
    await login.loadUser()

    const cart=useCartStore(pinia);
    await cart.loadCart();

    const cityAndDistricts=useCitiesAndDistrictsStore(pinia);
    await cityAndDistricts.loadCitiesAndDistricts();
    
    app.use(pinia)
}
