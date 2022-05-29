<template>
  <div id="checkoutArea" class="w-50 m-auto">
    <table class="table checkoutTable my-0">
        <thead>
        <tr>
            <th scope="col"></th>
            <th scope="col">ID.Product</th>
            <th scope="col">Quantity</th>
            <th scope="col">Price</th>
        </tr>
        </thead>
        <tbody v-for="cartProduct in cartProducts" :key="cartProduct.id">
                <CartItem  
                    :cartProduct="cartProduct"
                    :key="cart_reload"
                    @move-product="moveProduct"
                    @drop-product="dropProduct"
                />                    
        </tbody>
        <tr>
            <th scope="row"></th>
            <td></td>
            <td><h3 class="my-0 py-1">Total: </h3></td>
            <td><h3 class="my-0 py-1">{{sumCart}}$</h3></td>
        </tr>            
    </table>
    <div><b-button @click="createOrder(cartProducts)" squared variant="success" class="w-100">Checkout</b-button></div>
    <div id="success"><p id="message"></p></div>
  </div>
</template>

<script>
import CartItem from '../components/CartItem.vue'
import { mapState } from 'vuex'
import { mapActions } from 'vuex'
import { mapGetters } from 'vuex'

export default {
  components: {
    CartItem
  },
  computed: {
    ...mapState(['cartProducts']),
    ...mapGetters(['inCart', 'sumCart'])
  },
  created() {
    this.fetchCartProducts();
    this.id = this.$route.params.id
  },  
  data() {
    return {
        cart_reload: 0
    }
  },
  methods: {
    ...mapActions(['fetchCartProducts', 'modifyCartProduct', 'addToCart', 'dropCartProduct', 'addOrder']),
    moveProduct(id, action){
      this.reload();
      let picked = this.inCart(id)
      if (picked === null && action){
        this.addToCart({
          productId: id,
          quantity: 1
        })    
      }
      if (picked !== null) {
        if (action) {
          this.modifyCartProduct({ 
            productId: id,
            quantity: picked.quantity + 1,
            id: null
          })
        } else {
          if (picked.quantity - 1 === 0) {
            this.dropProduct(id)
          } else  {
              this.modifyCartProduct({ 
                productId: id,
                quantity: picked.quantity - 1,
                id: null
              })
          }
          
        }
      }            
    },
    dropProduct(id){
      this.reload();
      this.dropCartProduct(id);
    },
    createOrder(cartProducts){
      this.reload();
      let items = [];
      for (let i=0; i<cartProducts.length; i++) {
        let item = {
          productId: cartProducts[i].productId,
          quantity: cartProducts[i].quantity
        }
        items.push(item);
         this.dropCartProduct(cartProducts[i].productId);
      }

      var success = document.querySelector('#success');
      var message = document.querySelector('#message');

      if (!items.length){
        
        success.classList.add("alert");
        success.classList.add("alert-danger");
        success.classList.add("mt-3");
        success.setAttribute("role", "alert");
        let text = "You need to add items to create an order";
        message.innerHTML = text;
        message.classList.add("mb-0");
        success.appendChild(message);
      } else {
        this.addOrder({items});

        success.classList.add("alert");
        success.classList.add("alert-success");
        success.classList.add("mt-3");
        success.setAttribute("role", "alert");
        let text = "Order registered correctly!";
        message.innerHTML = text;
        message.classList.add("mb-0");
        success.appendChild(message);
      }
      
    },
    reload() {
      this.cart_reload++;
    }
  }
}
</script>

<style>
  .checkoutTable{
    border-left: .5em solid black;
    border-right: .5em solid black;
    border-top: .5em solid black;
  }
</style>