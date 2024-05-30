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

// ==========================================================================================
// Examen 2022-05-20
// ==========================================================================================

/*
Socio(id, nombre, documento)
Juguete(id, descripción, anio, valor, idCategoria, idColeccion)
Categoria(id, Nombre)
Coleccion(id, fechaRegistro, idSocio, idFuncionario)
Funcionario(id, nombre, nroEmpleado)
*/

/*
5. Utilice el contexto de la parte anterior para resolver mediante Linq, las siguientes consultas:
    a. Dados un año de fabricación y un número de empleado, obtener todos los juguetes fabricados 
    ese año, que forman parte de colecciones ingresadas por el funcionario con ese número. Los juguetes 
    deberán estar ordenados por valor estimado en forma descendiente
*/
public List<Juguete> ObtenerJuguetes(int anio, int nroEmpleado) {
    return Context.Juguetes
    .Where(j => j.Anio == anio && j.Coleccion.Funcionario.NroEmpleado == numEmp)
    .OrderByDescending(j => j.Valor)
    .ToList();
}

/*
5. 
    b. Dados un documento de socio y un nombre de categoria, obtener solamente la descripción de todos los 
    juguetes en las colecciones de ese socio, que pertenezcan a esa categoría.
*/
public List<string> ObtenerSocios(){
    return Contexto.Juguetes
    .Where(j => j.Coleccion.Socio.Documento == docSocio && j.Categoria.nombre == nomCat)
    .Select(j => j.Descripcion)
    .ToList();
}

// ==========================================================================================
// Examen 2023-02-15
// ==========================================================================================

/*
Cliente (Id,Nombre, Telefono, Email)
Factura (Id, Fecha, IdCliente, TasaIva, Total)
Promocion (Id, Nombre, TasaDescuento)
Servicio (Id, Nombre, CostoActual, IdPromocion)
ServicioIncluido (IdServicio, IdFactura, TasaDescuentoAplicada, IdPromocion, Cantidad)
*/

/*
4. Escribir las consultas LINQ que permitan:
    a. Dados un Id de servicio y el id de una promoción, todas las facturas que incluyan ese servicio siempre y cuando a ese servicio se le haya aplicado esa promoción.
*/

public List<Factura> BuscarFacturas(int idServicio, int idPromocion) {
    return Contexto.Facturas
        .Where(f => 
            f.ServiciosIncluidos.Any( si => 
                si.Id == idServicio 
                && si.Promocion != null
                && si.Promocion.Id == idPromocion)
            )
        .ToList();
}

/*
4. Escribir las consultas LINQ que permitan:
    b. Obtener el nombre y el email de todos los clientes que hayan comprado más que un monto dado (sin impuestos). 
       El resultado estará ordenado en forma descendente por nombre de cliente.
*/
public List<Cliente> BuscarClientes(int montoDado) {
    return Contexto.Facturas
        .Where(f => f.Total >= montoDado)
        .Select(f => f.Cliente)
        .Distinct()
        .ToList();
    // TODO .OrderByDescending(cli=>cli.Nombre)
}

// ==========================================================================================
// Examen 2023-05-12
// ==========================================================================================

/*
CentroMedico(id, nombre, dirección, teléfono)
Paciente(id, nombre, cedula, idCentroMedico)
Solicitud(id, fecha, idPaciente, completada)
Examen(codigo, nombre, preparación, tipo ) 
SolicitudExamen(SolicitudId, ExamenCodigo) ----> donut generate class, agregar listas
*/

/*
4. Escribir las siguientes consultas LINQ. Indique en qué clase las ubicaría
   a. Implementar una consulta Linq que, dado el id de un paciente, obtenga las solicitudes 
   no completadas de ese paciente ordenadas por fecha de la más vieja a la más reciente. Al 
   obtener las solicitudes deberá incluir también los exámenes solicitados
*/

public List<Solicitud> BuscarPorPaciente(int id){
    return Contexto.Solicitudes
    .Include(s => s.Examenes)
    .Where(s => s.PacienteId == id && s.Completada == false)
    .OrderByDescending(s => s.Fecha)
    .ToList();
}

/*
4. 
   b. Dado el id de un centro médico obtener el nombre y preparación de los exámenes radiológicos 
   que haya solicitado ese centro médico. Cada examen debe aparecer una única vez
*/

public List<Examen> BuscarPorCentroMedico(int id){
    return Contexto.Examenes
    .Where(e => e.Tipo == "Rad" && e.Solicitudes.Any(s => s.Paciente.CentroMedicoId == id))
    .Select(e => e.Nombre, e.Preparacion)
    .ToList();
}

// ==========================================================================================
// Examen 2023-09-22
// ==========================================================================================

/*
Clientes(id, nombre, email, telefono)
Direcciones(id, direccion, idCliente)
Empleados(id, doc, nombre, valorVisita)
Visitas(id, fecha, idDecorador, idDireccionCliente, valorVisita)
*/

/*
3. Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen.
    a. Dado un Id de empleado, obtener el total que ha cobrado históricamente por todas las visitas a clientes que ha realizado.
*/
//EN REPOSITORIO DE EMPLEADOS
public decimal TotalEmpleado(int id) {
    var resultado = Contexto.Empleados
        .Where(emp => emp.Id == id)
        .Select(emp => emp.Visitas.Sum(vis => vis.ValorVisita))
        .SingleOrDefault();
    return resultado;
}

//OTRA SOLUCIÓN
public decimal TotalEmpleado(int id) {
    var resultado = Contexto.Visitas
        .Where(vis => vis.Empleado.Id == id)
        .Sum(vis => vis.ValorVisita);
    return resultado;
}

/*
3. Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen.
    b. Dada una cantidad de visitas, obtener todos los clientes a los que se les hayan realizado más de esa cantidad de visitas ordenados por nombre en forma descendente.
*/
//EN REPOSITOSIO DE CLIENTES
public List<Cliente> ClientesConMasVisitasQue(int cantidad) {
    var resultado = Contexto.Clientes
        .Where(cli => cli.Visitas.Count() > cantidad)
        .OrderByDescending(cli => cli.Nombre)
        .ToList()
    return resultado;
}

// ==========================================================================================
// Examen 2024-02-22
// ==========================================================================================

/*
Calzados(id, nombre, codigo, descripcion, precioVenta, stock)
Promocion(id, fechaDesde, fechaHasta, calzadoId, descuento, cantidadMinima)
Pedido(id, fecha, distribuidorId)
Distribuidor(id, rut, razonSocial, direccion, email)
CalzadoCantidad(calzadoId, pedidoId, cantidad)
*/

/*
4.Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen.
    a. Dada una fecha, y una cantidad pedida, obtener todos los modelos con promociones vigentes 
    a esa fecha y para los cuales la cantidad pedida permita aplicar el descuento (15 puntos)
*/
// En repo promociones
public List<Calzado> CalzadosConPromocionesVigentes(DateOnly fecha, int cantidad) {
    return Contexto.Promociones
    .Where(p=> p.fechaHasta <= fecha && p.cantidadMinima >= cantidad)
    .Select(p => p.Calzado)
    .ToList();
}

/*
4.Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen.
    b. Dada un código de modelo, obtener todos los distribuidores a los que se les haya vendido 
    dicho modelo en el presente año, ordenados por razón social en forma descendente (15 puntos).
*/
// En repo pedidos
public List<Distribuidor> ObtenerDistribuidores(int codigo) {
    Contexto.Pedidos
    .Where(p => p.fecha.Year == DT.Now.Year && p.CalzadoCantidad.Any(cc.Calzado.Codigo == codigo))
    .Select(p => p.Distribuidor)
    .OrderByDescending(dis => dis.RazonSocial)
    .Distinct()
    .ToList();
}