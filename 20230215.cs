// ==========================================================================================
// Examen 2023-02-15
// ==========================================================================================

/*
Cliente (Id,Nombre, Telefono, Email)
Factura (Id, Fecha, IdCliente, TasaIva, Total)
Promocion (Id, Nombre, TasaDescuento)
Servicio (Id, Nombre, CostoActual, IdPromocion)
ServicioIncluido (IdServicio, IdFactura, TasaDescuentoAplicada, IdPromocion, Cantidad)

3. Implementar el alta de un servicio. La base de datos se llama Barbería y se ubica en un servidor local de Sql Server en la instancia Work
    a. Codificar los métodos de las entidades de negocio involucradas en el alta de un servicio incluyendo las validaciones que considere necesarias 
    (solamente los que utilice en oportunidad del alta)
*/

// De los servicios se conoce un Id (identity), un nombre único, el costo actual y la promoción que aplica actualmente. Puede haber servicios a los que 
// actualmente no se les aplique una promoción, y a un mismo servicio no se le pueden aplicar varias promociones simultáneamente.
[Index(nameof(Nombre), IsUnique = true)]
[Table("Servicios")]
public class Servicio : IValidable {
    public int Id { get ; set ; }
    public string Nombre { get; set; }
    public decimal CostoActual { get; set; }
    public Promocion Promocion { get; set; }

    public void Validar() {
        if (string.IsNullOrEmpty(Nombre))
            throw new Exception("El nombre es requerido");
        if (CostoActual < 0)
            throw new Exception("El costo no puede ser negativo");
    }
}

/*
3. 
    b. Codificar lo necesario para el alta del servicio utilizando Entity Framework.
*/

// En appsettings.json
// base ubicada en la instancia Sql2019 de un servidor local, 
{
    "ConnectionStrings" : {
        "MiConexion" : "Data Source=(local)\\Sql2019; Initial Catalog=Barberia; Integrated Security=SSPI;"
    }
}

// En Program.cs
builder.Services.AddScoped<ICUAlta<Servicio>, CUAltaServicio>();
builder.Services.AddScoped<IRepositorioServicios, RepositorioServicios>();

string conStr = builder.Configuration.GetConnectionString("MiConexion");
builder.Services.AddDbContext<BarberiaContext>(options => options.UseSqlServer(conStr));

// En LogicaDatos.Repositorios
public class RepositorioServicios : IRepositorioServicios {
    public BarberiaContext Contexto { get; set; }

    public RepositorioServicios(BarberiaContext ctx) {
        Contexto = ctx;
    }

    public void Create(Servicio obj) {
        obj.Validar();
        // TODO: precuntar validar duplicates si nombre es unico (?
        if (!Contexto.Servicios.Any(s => s.Nombre == obj.Nombre)) {
            Contexto.Servicios.Add(obj);
            Contexto.SaveChanges();
        } else {
            throw new DuplicadoException("Ya existe un servicio con ese nombre.");
        }
        
    }
}

/*
3. 
    c. Implementar un método de acción de una WebApi para dar el alta de un servicio.
*/
[Route("api/servicios/")]
//[Route("api/[controller]")]
[ApiController]
public class ServiciosController : ControllerBase {
    public ICUAlta<ServicioDTO> CUAlta {get; set;}

    public ServiciosController(ICUAlta<ServicioDTO> cUAlta) {
        CUAlta = cUAlta;
    }

    //api/servicios/
    [HttpPost]
    public IActionResult Post([FromBody] ServicioDTO servicioDTO) {
        if (servicioDTO == null)
            return BadRequest("El servicio no puede ser null"); //400

        try {
            CUAlta.Alta(servicioDTO);
            return CreatedAtRoute("FindById", new { id = servicioDTO.Id }, servicioDTO); //201
            // 201 - Created - Para solicitudes que crean objetos de servidor (por ejemplo, POST). 
            // El servidor debe haber creado el objeto antes de enviar este código de estado.
        }
        catch (ExcepcionPropiaException ex) {
            return BadRequest(ex.Messsage); //400
            // 400 - Bad Request - Se utiliza para indicar al cliente que ha enviado una solicitud con formato incorrecto.
        }
        catch (Excetpion ex) {
            return StatusCode(500, "Ocurrió un error inesperado"); //500
            // 500 - Internal Server Error - Se utiliza cuando el servidor encuentra un error que le impide atender la solicitud.
        }
    }
}

/*
3. 
    d. Escribir lo que debería incluir en Postman para acceder al método de la Web Api. Incluya la URL, el verbo HTTP y la estructura de datos (json) que debería pasar en el body.
*/

// URL: http://localhost:xxxx/api/Servicios
// Verbo HTTP: Post
// Body: {
//     "Nombre":"nombre del servicio",
//     "CostoActual":1000
// }

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