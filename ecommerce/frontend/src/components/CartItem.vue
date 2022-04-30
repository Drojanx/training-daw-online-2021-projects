<template>
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
           <tbody>
               <tr>
                   <th scope="row"> {{ cartProduct.productId }}</th>
                   <td> {{ productName.name }} </td>
                   <td> {{ cartProduct.quantity }} </td>
               </tr>
           </tbody>
       </table>
    </div>
</template>

<script>


export default {
    props: ['cartProduct', 'products'],
    data() {
        return {
            myCart: this.cartProduct,
            myAllProducts: this.products,
            productName: this.myAllProducts.find(f => f.id === this.myCart.productId).name,
            productPrice: this.myAllProducts.find(f => f.id === this.myCart.productId).price
        }
    },
}
</script>

<style>

</style>