<template>
  <div>
    <b-card
        title=""
        :img-src="product.mainImage"
        img-alt="Image"
        img-top
        tag="article"
        style="max-width: 30rem; background-color: aliceblue"
        class="mb-2"> 
        
        <h3>{{ product.name }}</h3>
        
        <b-card-text id="short">
            {{ product.short }}
        </b-card-text>
        <b-card-text id="price">
            <div v-if="product.disccount === 0">
                <h3>{{ product.price*100/100 }}$</h3>
            </div>
            <div v-if="product.disccount !== 0">
                <h3 style="color: red;">{{ (product.price*100/100)-product.disccount }}$</h3>
            </div>

        </b-card-text>
        <div class="buttons">
            <b-button @click="showProductDetails" class="button" href="#" variant="primary" size="lg">View</b-button>
            <b-button  @click="moveProduct" class="button" href="#" variant="primary" size="lg">Add</b-button>
        </div>
    </b-card>
  </div>
</template>



<script>

export default {
    props: ['product'],
    methods: {
        moveProduct() {
            this.$emit('move-product', this.product.id, true)
        },
        showProductDetails() {
            this.$router.push(`products/${this.product.id}`)
        }
    }

}
</script>

<style>
    .buttons {
        display: flex;
        justify-content: space-around;
        margin: auto;
    }
    .button {
        width: 5em;
    }
    #price{
        text-align: end;
        font-weight: bolder;
        margin-bottom: 0;
    }
    #short{
        text-align:justify ;
    }

</style>