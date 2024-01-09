import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { fab } from '@fortawesome/free-brands-svg-icons'
import { type App } from 'vue'

export default async function FontAwasomeConfigSetter(app:App<Element>) {
    library.add(far)
    library.add(fas)
    library.add(fab)
}
