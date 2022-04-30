<template>
  <div>
    <b-card
        title=""
        img-src="https://picsum.photos/600/300"
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
            <h3>{{ product.price*100/100 }}$</h3>
        </b-card-text>
        <div class="buttons">
            <b-button class="button" href="#" variant="primary" size="lg">View</b-button>
            <b-button  @click="pickProduct" class="button" href="#" variant="primary" size="lg">Add</b-button>
        </div>
    </b-card>
  </div>
</template>



<script>

export default {
    props: ['product'],
    methods: {
        pickProduct() {
            this.$emit('pick-product', this.product.id)
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