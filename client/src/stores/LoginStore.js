import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import axios from 'axios'

export const useLoginStore = defineStore('login', {
  state: () => {
    return {
      user: {
        name: '',
        surname: '',
        roles: []
      },
      isLogged: false
    }
  },
  actions: {
    async loadUser() {
      var userLogin = await axios.get('Identity/GetUser')
      if (userLogin.data) {
        this.user = userLogin.data
        this.isLogged = true
      }
    }
  }
})
