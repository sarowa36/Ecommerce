import { Guid } from 'guid-typescript'
import { default as Toast, useToast } from 'vue-toastification'
import axios from 'axios'
import { ref,type App } from 'vue'

export default async function AxiosConfigSetter(app:App<Element>) {
  const loadingIds=ref([]);
  
  axios.defaults.baseURL = location.origin + '/api/';
  axios.defaults.validateStatus = (status) => status >= 200 && status <= 600;

  axios.interceptors.request.use((request)=>{
    request.id=Guid.create().toString();
    loadingIds.value.push(request.id);
    return request;
  })
  axios.interceptors.response.use(function (response) {
    const toast = useToast()
    const serverErrorGroup = response.status >= 500
    const requestErrorGroup = response.status >= 400 && response.status < 500
    if (response.data.redirect) {
      window.location.href = response.data.redirect
    }
    if (serverErrorGroup)
      toast.error(
        'A server-based error occurred. The error report has been logged and will be resolved as soon as possible.\n'+(response.data.modelOnly || "")
      )
    else if (requestErrorGroup)
      toast.warning('You have made a false request. Check your request again.\n'+(response.data.modelOnly || ""))
    response.isSuccess = !serverErrorGroup && !requestErrorGroup;
    loadingIds.value=loadingIds.value.filter(x=>x!=response.config.id);
    return response
  }, undefined)

  app.mixin({
    data(){
      return{
        loadingIds
      }
    },
    computed:{
      isLoading:{
        get(){
          return loadingIds.value.length!=0;
        }
      }
    }
  })
}
