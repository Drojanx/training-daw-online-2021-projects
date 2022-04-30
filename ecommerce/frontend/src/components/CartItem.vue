<template>
        <tr>
            <th scope="row">{{selectedProduct.id}}</th>
            <td>{{selectedProduct.name}}</td>
            <td>{{myCartProd.quantity}}</td>
            <td>{{ (selectedProduct.price*myCartProd.quantity)*100/100 }}$</td>
        </tr>
</template>

<script>
import { mapState } from 'vuex'
 import store from '@/store'

export default {
    store: store,
    props: ['cartProduct'],
    data() {
        return {
            myCartProd: this.cartProduct,
/*             myAllProducts: this.products,
            productName: this.myAllProducts.find(f => f.id === this.myCartProd.productId).name,
            productPrice: this.myAllProducts.find(f => f.id === this.myCartProd.productId).price
 */        }
    },
    computed: {
        ...mapState(['products']),
        selectedProduct() {
            return this.products.filter(product => product.id === this.myCartProd.productId)[0]
        }
    }
}
</script>

<style>

</style>