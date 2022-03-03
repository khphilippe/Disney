# Disney

Endpoint

Todos los id son int

Para Registrar
POST: 	/Auth/Register

Para logear
POST:  /Auth/Register


Lista de Personaje
GET:	/characters

Detalle Personaje
GET:	/Characters/id

Buscar personaje por su nombre
GET:    /characters/name?name=nombreQueHayQuePoner

Buscar personaje por su edad
GET:    /characters/age?age=edadAPoner 

Buscar personaje por peso
GET:    /characters/weight?peso=pesoQueHayQuePoner 
        
Buscar personaje por id Pelicula
GET:    /characters/idmovie?idmovie=iDQueHayQuePoner            

Modificar un personaje
PUT:   /Characters/id			

Add personaje
Post:   /characters

Borrar un personaje
Delete:  /characters/idABorrar 


Lista de peliculas
GET:     /Movies

Detalles de peliculas
GET:    /Movies/id

Buscar peliculas por nombre
GET:   /movies/name?name=nombre

Buscar peliculas por id de genero
GET:    /movies/genre?idgenre=1

Detalle peliculas
GET:    /Movies/2

Ordenar la lista de peliculas
GET:    /movies/ord?order=ASC
GET:	/movies/ord?order=DESC

Delete peliculas
Delete:    /movies/2

Modificar Peliculas
PUT: /movies/2


