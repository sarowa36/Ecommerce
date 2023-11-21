import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useLoginStore = defineStore('login', {
  state: () => {
    return { user: ""}
  },
  getters:{
    isLogged:(state)=>{
        return Boolean(state.user);
    }
  },
  actions: {
    async SignIn(){
        this.user={userName:"Foo"};
    }
  }
})
