import Vue from 'vue'
import Vuex from 'vuex'
import url from '../utils/api'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    products: [],
    cartProducts: []
  },
  getters: {
    inCart: (state) => (productId) => {
      let item = state.cartProducts.find(item => item.productId === productId);
      return item || null
    }
  },
  mutations: {
    setProducts(state, payload) {
      state.products = payload
    },
    setCart(state, payload) {
      state.cartProducts = payload
    },
    addToCart(state, payload) {
        state.cartProducts.push(payload);
    },
    modifyCart(state, payload) {
      let productId = payload.productId
      let exists = state.cartProducts.find(f => f.productId === productId)
      if (exists){
        exists.quantity = payload.quantity;
      }
    }
  },
  actions: {
    fetchProducts( {commit} ) {
      fetch(url('/products'))
        .then(result => result.json())
        .then(data => commit('setProducts', data))
    },
    fetchCartProducts( {commit} ) {
      fetch(url('/cart'))
        .then(result => result.json())
        .then(data => commit('setCart', data))
    },
    addToCart( {commit}, productInfo) {
      fetch(url('/cart'), {
        method: 'POST',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(productInfo)
      })
        .then(result => result.json())
        .then(data => commit('addToCart', data))
    },
    modifyCartProduct( {commit}, productInfo) {

      let cartId = this.getters.inCart(productInfo.productId).id;
      productInfo.id = cartId;
      fetch(url('/cart/'+cartId), {
        method: 'PUT',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(productInfo)
      })
        .then(result => result.json())
        .then(data => commit('modifyCart', data))
    }
  },
  modules: {
  }
})
