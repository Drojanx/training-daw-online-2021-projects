<template>
  <div>
      <div id="catalog" class="d-flex">
        <b-row id="productList">
            <b-col cols="4" v-for="product in products" :key="product.id">
                <Product
                    :product="product"
                    @pick-product="pickProduct"
                />
            </b-col>
        </b-row>
        <div id="cartPart">
            <b-card
            header="Cart products"
            header-text-variant="white"
            header-tag="header"
            header-bg-variant="dark"
            footer="Card Footer"
            footer-tag="footer"
            footer-bg-variant="success"
            footer-border-variant="dark"
        >
            <div>
            <table class="table">
                <thead>
                <tr>
                    <th scope="col">Id</th>
                    <th scope="col">Product</th>
                    <th scope="col">Quantity</th>
                    <th scope="col">Price</th>
                </tr>
                </thead>
                <tbody v-for="cartProduct in cartProducts" :key="cartProduct.productId">
                        <CartItem  
                            :cartProduct="cartProduct"
                        />
                </tbody>
           </table>
                
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
     computed: {
        ...mapState(['products', 'cartProducts']), /*Va al store y se trae el state "books"*/
        ...mapGetters(['inCart'])
    },
    created() {
        this.fetchCartProducts();
    },

/*     data() {
        return {
            cartProducts: [
                {id: 1, name: "item 1", quantity: 2},
                {id: 2, name: "item 2", quantity: 1},
                {id: 3, name: "item 3", quantity: 2}
            ]
        }
    }, */
    methods: {
        ...mapActions(['addToCart', 'modifyCartProduct', 'fetchCartProducts']),
        pickProduct(id){
            let picked = this.inCart(id)
            if (picked === null){
                this.addToCart({
                    productId: id,
                    quantity: 1
                })    
            } else {
                this.modifyCartProduct({ 
                    productId: id,
                    quantity: picked.quantity + 1,
                    id: null
                 })
            }
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
</style>