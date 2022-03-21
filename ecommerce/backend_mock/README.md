# BACKEND ecommerce API

A continuación se presenta el API necesaria para el desarrollo del proyecto 🚀

## ⚙️ Instalación 
Ejecuta `npm install` en la raiz del proyecto

## 💻 Como arrancar el proyecto
Para ejecutar el API, simplemente lanza:
`npm run json:server`

## API

El API se compone de 2 endpoints:
* products 
* cart
* orders

### Products

#### Listado de productos

`curl localhost:3000/products`

Respuesta:
```

[
  {
    "id": 0,
    "name": "Intelligent Soft Ball",
    "category": "Pants",
    "short": "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients",
    "description": "Quam eaque harum. Deserunt quis sequi. Consequatur ullam fugiat non error occaecati dignissimos culpa exercitationem.\n \rMaiores velit in blanditiis unde laborum quisquam reiciendis ut. Neque consequatur tenetur ea fuga veritatis nostrum temporibus. Dignissimos rerum quam sed.\n \rAd qui et rem. Eum laudantium nisi in dicta. Repudiandae doloremque quas debitis autem repellat id perspiciatis. Tempora ex temporibus eum. Numquam omnis quo molestias.",
    "price": "844.00",
    "mainImage": "http://placeimg.com/320/240/abstract?70581",
    "disccount": 0,
    "images": [
      "http://placeimg.com/320/240/abstract?91431"
    ]
  },
  {
    "id": 1,
    "name": "Practical Soft Hat",
    "category": "Chicken",
    "short": "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support",
    "description": "Possimus molestiae non. Dolores sit quo tenetur asperiores cum voluptatem sit voluptas. Facere culpa optio saepe beatae. Animi dolor eos unde deleniti tempore laudantium sint quia.\n \rDolore doloremque unde. Eius impedit aut provident laboriosam ipsa iste tempore. Sed omnis est qui ut ut dolores. At molestias temporibus maiores dolor dolor accusantium vero assumenda non.\n \rNihil non at. Reprehenderit est doloribus in hic. Non animi earum inventore.",
    "price": "739.00",
    "mainImage": "http://placeimg.com/320/240/abstract?12681",
    "disccount": 0,
    "images": [
      "http://placeimg.com/320/240/abstract?62165",
      "http://placeimg.com/320/240/abstract?82318",
      "http://placeimg.com/320/240/abstract?35674",
      "http://placeimg.com/320/240/abstract?96406",
      "http://placeimg.com/320/240/abstract?66888"
    ]
  },
...
]

```

#### Listado paginado de productos

`curl "localhost:3000/products?_page=2&_limit=5"`
* Página 2 de un listado paginado de 5 elementos

#### Información de un producto
`curl "localhost:3000/products/2"`

### Carrito

#### Añadir un elemento al carrito
`curl -HContent-type:application/json -XPOST -d '{"productId":0,"quantity":1}' "localhost:3000/cart"`

* `ProductId` será la referncia el producto (el ID)
* `quantity` la cantidad de unidades 

#### Visualizar los elementos del carrito

`curl "localhost:3000/cart"`

Respuesta

```
[
  {
    "quantity": 3,
    "productId": 1,
    "id": 2
  },
  ....
]
```

#### Buscar si un elemento se encuentra en el carrito

`curl -vv "localhost:3000/cart?productId=1"`

#### Modificar la cantidad de unidades de un producto

`curl -vv -HContent-type:application/json -XPUT -d '{ "quantity": 3, "productId": 1 }' "localhost:3000/cart/2"`

> ⚠️ **Content-type:application/json**: Es imprescindible indicar la cabecera para el correcto funcionamiento del API

> ⚠️ **Objecto completo**: Al modificar el objeto, será necesario volver a pasarle la referencia al productId para que no se pierda

### Orders

#### Añadir una nueva orden

Para añadir una nueva orden será necesario especificar todas las líneas de producto que queremos añadir

`curl -HContent-type:application/json -XPOST -d '{ "items": [ {"productId":2,"quantity":1}, {"productId": 0, "quantity": 1}] }' "localhost:3000/orders"`
