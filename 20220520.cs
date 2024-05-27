// ==========================================================================================
// Examen 2022-05-20
// ==========================================================================================

/*
Socio(id, nombre, documento)
Juguete(id, descripción, anio, idCategoria, idColeccion)
Categoria(id, Nombre)
Coleccion(id, fechaRegistro, idSocio, idFuncionario)
Funcionario(id, nombre, nroEmpleado)
*/


/*
4. Implemente la clase de contexto de EF completa, con lo necesario para conectarse a la base 
de datos, asumiendo que esa información se encuentra en el web.config de la aplicación web.
*/


/*
5. Utilice el contexto de la parte anterior para resolver mediante Linq, las siguientes consultas:
    a. Dados un año de fabricación y un número de empleado, obtener todos los juguetes fabricados 
    ese año, que forman parte de colecciones ingresadas por el funcionario con ese número. Los juguetes 
    deberán estar ordenados por valor estimado en forma descendiente
*/


/*
5. 
    b. Dados un documento de socio y un nombre de categoria, obtener solamente la descripción de todos los 
    juguetes en las colecciones de ese socio, que pertenezcan a esa categoría.
*/