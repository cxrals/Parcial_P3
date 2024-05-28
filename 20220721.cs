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
4. Utilice el contexto de la parte 3 para resolver mediante Linq, las siguientes consultas:
    a. Dado un nombre de producto y una cantidad, obtener todos los kits que contengan esa 
    cantidad o más de ese producto. Deberá ordenar los kits por precio en forma ascendente y 
    luego por nombre descendente.
*/

public List<Kit> ObtenerKits(string nombreProd, int cantidadIng) {
    return Contexto.Ingredientes
    .Where(i => i.Producto.Nombre == nombreProd && i.Cantidad >= cantidadIng)
    .Select(i => i.Kit)
    .Distinct()
    .OrderBy(k => k.Precio)
    .ThenByDescending(k => k.Nombre)
    .ToList();
}
// fock, u forgot la lista en kits
// xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx :(

public List<Kit> KitsConElProductoEnCantidadMayorOIgualA(string nomProd, int cant) {
    return db.Kits
    .Where(k => k.Ingredientes.Any(i => i.Producto.Nombre == nomProd && i.Cantidad >= cant))
    .OrderBy(k => k.Precio)
    .ThenByDescending(k => k.Nombre)
    .ToList();
}

/* 
4. 
    b. Dada una ubicación, obtener solamente el nombre y el precio de todos los productos ubicados 
    en ese lugar. Asuma que existe una clase de nombre DTOProducto con las propiedades “Nombre” y 
    “Precio”, para utilizar en el retorno de la función.
*/
public List<DTOProducto> ProductosEnUbicados(Ubicacion u) {
    return Contexto.Productos
    .Where(p => p.Ubicacion.Id == u.Id)
    .Select(p => new DTOProducto() { Nombre = p.Nombre, Precio = p.PrecioUnit })
    .ToList();
}

// ==========================================================================================
// Examen 2022-10-14
// ==========================================================================================

/*
Socio(id, numero, nombre, email, documento, telefono)
Sorteo(id, fecha, lugar)
SociosSorteos(id, idSocio, idSorteo)
Vivienda(id, numero, descripcion, direccion, idSorteo, idSocioAdjudicatario)
*/

/* 
5.
    a. Dado un numero de socio, obtener todos los sorteos en los que participó ese socio,
    ordenados por fecha en forma ascendente.
*/
public List<Sorteo> SorteosDeSocio (int nroSocio) {
    return Contexto.Sorteos
    .Where(s => s.Socios.Any(soc=> soc.Numero == nroSocio))
    .OrderBy(s => s.Fecha)
    .ToList();
}

/* 
5.
    b. Dado un número de vivienda, obtener solamente la fecha del sorteo y el nombre del socio al que 
    se adjudicó. Asuma que existe una clase de nombre DTOAdjudicatario con las propiedades “Nombre” y 
    “Fecha”, para utilizar en el retorno de la función.
*/
public DTOAdjudicatario GanadorVivienda(int nroVivienda){
    return Contexto.Viviendas
    .Where(v => v.Numero == nroVivienda)
    .Select(v=> new DTODTOAdjudicatario() { Nombre = v.Socio.Nombre, Fecha = v.Sorteo.Fecha })
    .SingleOrDefault();
}