<template>
  <div>
      <div id="catalog" class="d-flex">
        <b-row id="productList">
            <b-col cols="4" v-for="product in products" :key="product.id">
                <Product
                    :product="product"
                    @move-product="moveProduct"
                />
            </b-col>
        </b-row>
        <div id="cartPart">
            <b-card
            header="Cart products"
            header-text-variant="white"
            header-tag="header"
            header-bg-variant="dark"
        >
            <div>
                <table class="table">
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
                <b-button @click="goToCheckout" squared variant="success" class="checkoutBtn">Checkout</b-button>
            </div>
            </b-card>

        </div>
      </div>
      
  </div>
</template>

<script>
import Product from '../components/Product.vue'
import CartItem from '../components/CartItem.vue'
import { mapState } from 'vuex'
import { mapActions } from 'vuex'
import { mapGetters } from 'vuex'

export default {
    name:'Catalog',
    components: {
        Product,
        CartItem
    },
    data() {
        return {
            cart_reload: 0
        }
    },
     computed: {
        ...mapState(['products', 'cartProducts']), /*Va al store y se trae el state "books"*/
        ...mapGetters(['inCart', 'sumCart']),
    },
    created(){
        this.fetchProducts()
        this.fetchCartProducts()
    },
    methods: {
        ...mapActions(['addToCart', 'modifyCartProduct', 'fetchCartProducts', 'dropCartProduct', 'fetchProducts']),
        moveProduct(id, action){
            this.cart_reload++;
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
        goToCheckout(){
            this.$router.push(`/checkout`)
        },
        reload() {
            this.cart_reload++;
        }
    }   
}
</script>

<style>
#catalog{
    justify-content: space-between;
    width: 90%;
    margin: auto;
}
#productList {
    width: 75%;
}
#cartPart {
    width: 25%;
}
.checkoutBtn{
    width: 100%;
}
</style>