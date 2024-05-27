// ==========================================================================================
// Examen 2023-07-19
// ==========================================================================================

/*
Producto (id, nombre, precioActual, promocionId)
Promocion(id, nombre, tasaDescuento)
Cliente(id,nombre,email,telefono)
Factura(id,fecha, monto,impuestos, clienteId)
Item(productoId,facturaId,precioVenta,cantidad, descuentoAplicado)
*/

/*
2. Implementar el alta de un producto. La base de datos se llama Ferretería y se ubica en un 
servidor local de SQL Server en la instancia Work.
    a. Codificar los métodos de las entidades de negocio involucradas en el alta de un producto, 
    incluyendo las validaciones que considere necesarias (solamente los que utilice en oportunidad 
    del alta). Asuma que hay una excepción personalizada ProductoException con los 3 constructores habituales.
*/


/*
2. 
   b. Implementar y definir todo lo necesario para el alta de producto en la capa de aplicación(casos de uso). 
   Incluir las dependencias inyectadas.
*/


/*
2. 
   c. Codificar lo necesario para el alta en la capa de acceso a datos (repositorios) usando Entity Framework Core. 
   Incluya lo necesario para inyectar la dependencia que corresponda, indicando dónde lo incluye. Se deberá indicar 
   qué debe incluir y dónde para que el nombre del producto sea único. Indique cuál es la cadena de conexión a utilizar 
   y dónde la ubicaría.
*/


/*
3. Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen. 
    a. Dado un Id de producto, obtener todas las facturas que incluyan ese producto siempre y cuando a ese producto se le 
    haya aplicado algún descuento al venderlo.
*/


/*
3. 
   b. Obtener el nombre y el correo electrónico de todos los clientes que hayan comprado más que un monto dado (sin impuestos). 
   El resultado estará ordenado en forma descendente por nombre de cliente. Cada cliente debe aparecer una única vez
*/


/*
4. WebApi
   a. Escriba las firmas de los métodos de acción de un controlador WebAPI para todas las operaciones CRUD de productos, respetando 
   el protocolo REST. Indique también la url, el verbo HTTP, los parámetros y/o el body de la solicitud que debería enviar un cliente 
   (podría ser postman, o cualquier otro) para dar el alta de un producto. No olvide indicar los códigos de estado que espera obtener 
   en cada caso (alta correcta, y errores esperados).
*/


/*
4. WebApi
   b. Si para el alta de producto el nombre es requerido, pero el valor recibido por la acción POST de una WebAPI fuera null o vacío, 
   ¿qué código HTTP corresponde devolver y qué significado tiene ese código?
*/