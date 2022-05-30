# ClienteAPI
***
## _Backend_

API para la gestión de un e-commerce. Ofrece operaciones CRUD para los modelos Products, Orders y Cart.

El proyecto contiene 2 ficheros Dockerfile: uno que conteneriza la API exponiendo el puerto 3022 y otro (Migrations.Dockerfile) encargado de hacer las migraciones 
de Entity Framework necesarias para hablar con la Base de Datos. Para ello usará el script "Setup.sh".

Con esto y ejecutando `docker-compose up -d`, podemos tener dockerizados y funcionando en conjunto tanto a la API como a la base de datos. Para usarla, simplemente 
seguimos mandando las peticiones CRUD a https://localhost:3022

De hecho, la API está configurada para funcionar desde Docker. Si quisieramos lanzarla manualmente con el comando `dotnet run` junto con la base de datos dockerizada,
habría que modificar la Connection String e indicar que el servidor es "localhost,3012" en lugar de "ecommerce-db", que es lo que aparece ahora mismo.

Para ejecutar la Base de Datos en Docker:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPassword-1234" -p 3012:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
Seguidamente, para crear los esquemas en la Base de Datos almacenados en la carpeta de Migrations:
```
dotnet ef database update
```


Para iniciar la API:
```
dotnet run
```
La URL a la que habrá mandar las peticiones CRUD es: http://localhost:3022

Contiene también un fichero **ClienteAA.postman_collection.json** con una coleccción Postman con la que probar las peticiones.

Además, al lanzarse la API, se genera un Openapi 3 en la url http://localhost:3022/swagger
