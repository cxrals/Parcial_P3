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
3. Implementar la baja de una promoción utilizando MVC, C# Y Entity Framework.
    a. Codifique todos los métodos necesarios en las siguientes capas:
        - Presentación - no es necesario implementar la vista
        - Aplicación 
        - Acceso a datos usando Entity Framework Core 
*/
public class PromocionController : Controller {

    public ICUBaja<Promocion> CUBaja {get;set}
    public ICUBuscarPorId<Promocion> CUBuscarPorIdPromocion {get;set}

    public PromocionController(ICUBaja<Promocion> cuBaja, ICUBuscarPorId<Promocion> cuBuscarPorIdPromocion) {
        CUBaja = cuBaja;
        CUBuscarPorIdPromocion = cuBuscarPorIdPromocion;
    }


    public ActionResult Delete(int id) {
        Promocion p = CUBuscarPorIdPromocion.BuscarPorId(id);
        return View(p);
    }

    [HttpPost]
    public ActionResult Delete() {
        
        return View();
    }
}

public class CUBajaPromocion : ICUBaja<Promocion> {

}

public class RepositorioPromociones : IRepositorioPromociones {

}

/*
4.Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen.
    a. Dada una fecha, y una cantidad pedida, obtener todos los modelos con promociones vigentes 
    a esa fecha y para los cuales la cantidad pedida permita aplicar el descuento (15 puntos)
    b. Dada un código de modelo, obtener todos los distribuidores a los que se les haya vendido 
    dicho modelo en el presente año, ordenados por razón social en forma descendente (15 puntos).
*/

/*
4.Implementar los métodos necesarios con las consultas LINQ abajo detalladas, indicando en qué repositorio se incluyen.
    b. Dada un código de modelo, obtener todos los distribuidores a los que se les haya vendido 
    dicho modelo en el presente año, ordenados por razón social en forma descendente (15 puntos).
*/

/*
5.
    b. ¿Qué significa que la WebAPI retorne un status code 401 cuando se intenta realizar un alta de modelo?
    Elija uno de los dos status code para su respuesta (5 puntos)

    
    c. ¿Qué status code debería devolver la WebAPI en el caso de que no se pueda establecer la conexión con 
    el servidor de base de datos? (5 puntos)
*/