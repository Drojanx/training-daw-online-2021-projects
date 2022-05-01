<template>
  <div id="slider" class="d-flex justify-content-evenly">
    <div class="infoArea">
      <ProductDetails
      :detailProduct="detailProduct"
      @move-product="moveProduct"/>
    </div>
    <div>
      <b-card
            header="Cart products"
            header-text-variant="white"
            header-tag="header"
            header-bg-variant="dark"
        >
      <div>
        <table class="table my-0">
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
        <b-button squared variant="success" class="checkoutBtn">Checkout</b-button>
      </div>
      </b-card>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import { mapActions } from 'vuex'
import { mapGetters } from 'vuex'
import ProductDetails from '../components/ProductDetails.vue'
import CartItem from '../components/CartItem.vue'
export default {
    components: {
        ProductDetails,
        CartItem
    },
    computed: {
        ...mapState(['detailProduct', 'cartProducts']),
        ...mapGetters(['inCart', 'sumCart'])
    },
    created() {
        this.fetchCartProducts();
        this.fetchProductDetails(this.$route.params.id);
        this.id = this.$route.params.id
    },
    beforeRouteUpdate(to){
      this.fetchProductDetails(to.params.id)
      this.id = to.params.id
    },
    data() {
        return {
            id: 1,
            cart_reload: 0
        }
    },
    methods: {
    ...mapActions(['fetchProductDetails', 'fetchCartProducts', 'modifyCartProduct', 'addToCart', 'dropCartProduct']),
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
    reload() {
        this.cart_reload++;
    }
  }

}
</script>

<style>
  #slider {
    background-color: aliceblue;
    padding: 5em
  }
  .infoArea{
    width: 35%;
  }
  .checkoutBtn{
    width: 100%;
}
</style>