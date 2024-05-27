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
2. Implementar el alta de una visita.
    a. Codifique todos los métodos necesarios para el alta en las capas de presentación, 
       aplicación, y acceso a datos, indicando en cada caso en qué clase los ubica. 
       Para la persistencia deberá utilizar Entity Framework.
*/

// En MVC
public class VisitasController : Controller {
    public ICUListado<Cliente> CUListadoClientes { get; set; }
    public ICUListado<Empleado> CUListadoEmpleados { get; set; }
    public ICUAlta<Visita> CUAlta {get; set;}

    public VisitasController(ICUListado<Cliente> cuListCli, ICUListado<Empleado> cuListEmp, ICUAlta<Visita> cuAltaVisita) {
        CUListadoClientes = cuListCli;
        CUListadoEmpleados = cuListEmp;
        CUAlta = cuAltaVisita;
    }

    public ActionResult Create() {
        VisitaViewModel vm = new VisitaViewModel() {
            Empleados = CUListadoEmpleados.ObtenerListado(),
            Clientes = CUListadoClientes.ObtenerListado()
        };
        return View(vm);
    }

    [HttpPost]
    public ActionResult Create(VisitaViewModel vm) {
        try {
            VisitaDTO dto = new VisitaDTO();
            dto.IdCliente = vm.IdCliente;
            // ...

            CUAlta.Alta(dto);
            return RedirectToAction("Index", "Visitas");
        } catch (Exception e) {
            ViewBag.ErrorMsg = e.Message;
        }

        vm.Empleados = CUListadoEmpleados.ObtenerListado();
        vm.Clientes = CUListadoClientes.ObtenerListado();
        return View(vm);
    }
}

// En LogicaAplicacion.CasosUso
public class CUAltaVisita : ICUAlta<VisitaDTO> {
    public IRepositorioVisitas Repo { get; set; }
    public IRepositorioClientes RepoClientes { get; set; }
    public IRepositorioEmpleados RepoEmpleados { get; set; }

    public CUAltaVisita(IRepositorioVisitas repo) {
        Repo = repo;
    }

    public void Alta(VisitaDTO obj) {
        Visita nuevaVisita = MapperVisitass.CrearEntidad(obj);
        Cliente cliente = RepoClientes.FindById(obj.IdCliente);
        Empleado emp = RepoEmpleados.FindById(obj.IdEmpleado);

        Repo.Create(nuevaVisita);
    }
}

// En LogicaDatos.Repositorios
public class RepositorioVisitas : IRepositorioVisitas {
    public EmpresaContext Contexto { get; set; }

    public RepositorioServicios(EmpresaContext ctx) {
        Contexto = ctx;
    }

    public void Create(Visita obj) {
        obj.Validar();
        Contexto.Visitas.Add(obj);
        Contexto.SaveChanges();
    }
}

/*
2. Implementar el alta de una visita.
    b. Indique cuál es la cadena de conexión a utilizar y dónde la ubicaría
*/

// En appsettings.json
// Utilizan una base de datos de nombre ArtDecor ubicada en un servidor SQL Server local, 
// con nombre de instancia Server23 y utilizando seguridad integrada de Windows.
{
    "ConnectionStrings" : {
        "MiConexion" : "Data Source=(local)\\Server23; Initial Catalog=ArtDecor; Integrated Security=SSPI;"
    }
}

/*
2. Implementar el alta de una visita.
    c. Escriba lo necesario en la clase controladora de MVC para inyectarle la/s dependencia/s que corresponda/n.
*/

// En Program.cs

// INYECCIONES DE DEPENDENCIAS DE CONTROLADORES MVC
builder.Services.AddScoped<ICUAlta<VisitaDTO>, CUAltaVisita>();
builder.Services.AddScoped<ICUListado<Cliente>, CUListadoClientes>();
builder.Services.AddScoped<ICUListado<Empleado>, CUListadoEmpleados>();

//INYECCIONES DE DEPENDENCIAS DE CASOS DE USO
builder.Services.AddScoped<IRepositorioVisitas, RepositorioVisitas>();
builder.Services.AddScoped<IRepositorioClientes, RepositorioClientes>();
builder.Services.AddScoped<IRepositorioEmpleados, RepositorioEmpleados>();

string conStr = builder.Configuration.GetConnectionString("MiConexion");
builder.Services.AddDbContext<EmpresaContext>(options => options.UseSqlServer(conStr));

/*
2. Implementar el alta de una visita.
    d. Indique qué debe incluir y dónde para que el documento del empleado sea obligatorio y que el valor que cobra por visita sea un número positivo.
*/

public class Empleado : IValidable {
    public int id { get; set; }
    [Required]
    public string Documento { get; set; }

    [Range(0, decimal.MAX_VALUE)]
    public decimal ValorVisita { get; set; }

    public void Validar() {
        if (string.IsNullOrEmpty(Documento))
            throw new Exception("El Documento es requerido");
        if (ValorVisita < 0)
            throw new Exception("El ValorVisita no puede ser negativo");
    }
}

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

/*
4. WebApi 
    a. Escriba el método de acción de un controlador WebAPI para realizar el alta de un cliente, respetando el protocolo REST. 
       Indique también la url, el verbo HTTP, los parámetros y/o el body de la solicitud que debería enviar un cliente (podría ser postman, o cualquier otro) para dar el alta de un cliente.
*/

[HttpPost]
public IActionResult Post([FromBody] ClienteDTO? dto) {
    if (dto == null) return BadRequest("No se proporcionó la info para el alta"); //400

    try {
        CUAlta.Alta(dto);
        return CreatedAtRoute("FindById", new { id = dto.Id }, dto); //201
    }
    catch (ClienteException ex) {
        return BadRequest(ex.Messsage); //400
    }
    catch (Excetpion ex) {
        return StatusCode(500, "Ocurrió un error inesperado"); //500
    }

}

// URL: http://localhost:xxxx/api/clientes
// VERBO: POST
// BODY: {
//     "Nombre" : "Nombre Apllido",
//     "Email" : "mail@mail.com",
//     "Telefono" : "123123123"
// }

/*
4. WebApi 
    b. ¿Qué significa que la WebAPI retorne un status code 400 cuando se intenta realizar un alta de cliente?
*/

// 400 
// - Bad Request 
// - Se utiliza para indicar al cliente que ha enviado una solicitud con formato incorrecto.

/*
4. WebApi 
    b. ¿Qué status code debería devolver la WebAPI en el caso de que no se pueda dar de alta un cliente por ya existir ese documento?
*/

// 409
// - Conflict
// - Se utiliza para indicar algún conflicto que la solicitud puede estar causando en un recurso.
