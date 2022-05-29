<template >
  <div>
    <div class="productPart">
    <b-carousel
      id="carousel-1"
      v-model="slide"
      :interval="4000"
      controls
      indicators
      img-width="1400"
      img-height="563"
      @sliding-start="onSlideStart"
      @sliding-end="onSlideEnd"
    >
      <b-carousel-slide>
          <template #img>
            <img
              class="d-block img-fluid"
              width="4000"
              height="563"
              v-bind:src="detailProduct.mainImage"
              alt="image slot"
            >
          </template>
      </b-carousel-slide>

      <div v-for="(image, index) in mapImagesUrl" :key="`image-${index}`">

        <b-carousel-slide>
          <template #img>
            <img
              class="d-block img-fluid"
              width="4000"
              height="563"
              v-bind:src="image"
              alt="image slot"
            >
          </template>
        </b-carousel-slide>

      </div>
        
    </b-carousel>
        <h2 class="name">{{detailProduct.name}}</h2>
        <p class="description">{{detailProduct.description}}</p>
        <h2 class="price w-50">{{detailProduct.price*100/100}}$</h2>
        <div class="w-50 m-auto d-flex justify-content-evenly">
            <b-button @click="toCatalog" class="button" href="#" variant="primary" size="lg">Back</b-button>
            <b-button  @click="moveProduct" class="button" href="#" variant="primary" size="lg">Add</b-button>
        </div>
    </div>

  </div>
</template>

<script>
  export default {
    props: ['detailProduct'],  
    data() {
      return {
        slide: 0,
        sliding: null
      }
    },
    computed: {
      mapImagesUrl(){
        var imageUrls;
        if (typeof this.detailProduct.images === "string") {
          imageUrls = this.detailProduct.images.split(',');
        } else {
          imageUrls = this.detailProduct.images;
        }
        return imageUrls;
      }
    },
    methods: {
      onSlideStart() {
        this.sliding = true
      },
      onSlideEnd() {
        this.sliding = false
      },
      toCatalog() {
        this.$router.push(`/products`)
      },
      moveProduct() {
            this.$emit('move-product', this.detailProduct.id, true)
      }
    }
  }
</script>

<style>
    .description{
        text-align: justify;
    }
    .button {
        width: 10em;
        margin-top: 1.5em;
    }
    .price{
        margin: auto;
        margin-top: 1em;
    }
    .name{
        margin-top: 1em;
    }
</style>