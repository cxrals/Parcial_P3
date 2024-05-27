// ==========================================================================================
// Examen 2022-07-21
// ==========================================================================================

/*
Kit(id, nombre, precio)
Ingrediente(id, idKit, idProducto, cantidad)
Producto(id, nombre, precioUnit, IdUbicacion)
Ubicacion(id, sector, estanteria, estante)
*/


/* 
3. Implemente la clase de contexto de EF completa, con lo necesario para conectarse a la base de 
datos, asumiendo que esa información se encuentra en el archivo appsettings.json
*/


/* 
4. Utilice el contexto de la parte 3 para resolver mediante Linq, las siguientes consultas:
    a. Dado un nombre de producto y una cantidad, obtener todos los kits que contengan esa cantidad o más de ese producto.
    Deberá ordenar los kits por precio en forma ascendente y luego por nombre descendente.
*/


/* 
4. 
    b. Dada una ubicación, obtener solamente el nombre y el precio de todos los productos ubicados en ese lugar.
    Asuma que existe una clase de nombre DTOProducto con las propiedades “Nombre” y “Precio”, para utilizar en el retorno de la función.
*/

// ==========================================================================================
// Examen 2022-10-14
// ==========================================================================================

/*
Socio(id, numero, nombre, email, documento, telefono)
Sorteo(id, fecha, lugar)
SociosSorteos(id, idSocio, idSorteo)
Vivienda(id, numero, descripcion, direccion, idSorteo, idSocioAdjudicatario)
*/

// a. Dado un numero de socio, obtener todos los sorteos en los que participó ese socio,
// ordenados por fecha en forma ascendente.

// b. Dado un número de vivienda, obtener solamente la fecha del sorteo y el nombre del socio al que se adjudicó.
// Asuma que existe una clase de nombre DTOAdjudicatario con las propiedades “Nombre” y “Fecha”, para utilizar en el retorno de la función.
