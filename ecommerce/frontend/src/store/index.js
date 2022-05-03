import Vue from 'vue'
import Vuex from 'vuex'
import url from '../utils/api'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    products: [],
    cartProducts: [],
    detailProduct: []
  },
  getters: {
    inCart: (state) => (productId) => {
      let item = state.cartProducts.find(item => item.productId === productId);
      return item || null
    },
    productPrice: (state) => (productId) => {
      let price = state.products.find(item => item.id === productId).price;
      let disccount = state.products.find(item => item.id === productId).disccount;
      price = price - disccount;
      return price || null
    },
    sumCart: (state, getters) => {
      let sum = state.cartProducts.reduce((Sum, product) => getters.productPrice(product.productId) * product.quantity + Sum  ,0);
      return sum || null
    },
    
  },
  mutations: {
    setProducts(state, payload) {
      state.products = payload
    },
    setDetails(state, payload) {
      state.detailProduct = payload
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
        exists.id = payload.id;
      }
    },
    dropCartProduct(state, payload) {
      state.cartProducts = state.cartProducts.filter(f => {
        return f.productId !== payload;
      })
    },
    clearCart() {
      console.log("Hola")
    }
  },
  actions: {
    fetchProducts( {commit} ) {
      fetch(url('/products'))
        .then(result => result.json())
        .then(data => commit('setProducts', data))
    },
    fetchProductDetails( {commit}, detailsId ) {
      fetch(url('/products/'+detailsId))
        .then(result => result.json())
        .then(data => commit('setDetails', data))
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
      fetch(url('/cart/'+cartId), {
        method: 'PUT',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(productInfo)
      })
        .then(result => result.json())
        .then(data => commit('modifyCart', data))
    },
    dropCartProduct( {commit}, productId) {
      let cartId = this.getters.inCart(productId).id;
      fetch(url('/cart/'+cartId), {
        method: 'DELETE',
      })
        .then(result => result.json())
        .then(commit('dropCartProduct', productId))
    },
    addOrder( _, listItems) {
      fetch(url('/orders'), {
        method: 'POST',
        headers: {
          'Content-type': 'application/json'
        },
        body: JSON.stringify(listItems)
      })
        .then(result => result.json())
    }
  },
  modules: {
  }
})
