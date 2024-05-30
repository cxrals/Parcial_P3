// ==========================================================================================
// String de conexion a db
// ==========================================================================================

/*
Si la letra dice:
    - Base de datos con nombre "xxx"
    - Ubicada en la instancia "Sql2019" de un servidor local
    - Usando seguridad integrada de Windows
*/

/*
Then:
    - "Data Source = (local)\\Sql2019"
    - "Initial Catalog = xxx"
    - "Integrated Security = SSPI"

    "ConnectionString" : {
        "MiConexion" : "Data Source = (local)\\Sql2019; Initial Catalog = xxx; Integrated Security = SSPI;"
    }

*/

// ==========================================================================================
// Inyeccion de dependencias
// ==========================================================================================

// En Program

builder.Services.AddScoped<IRepositorySample, RepositorySample>();

string conStr = builder.Configuration.GetConnectionString("MiConexion");

builder.Servides.AddDbContext<ContextClass>(options => options.UseSqlServer(conStr));

// ==========================================================================================
// Clase de contexto
// ==========================================================================================

public class ObligatorioContext : DbContext {
    public DbSet<Obj> Objs { get; set; }

    public ObligatorioContext(DbContextOptions options) : base(options) {
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Aca va el string de conexion")
    }
}

// ==========================================================================================
// Data Annotations
// ==========================================================================================
[Index(nameof(Nombre), IsUnique = true)] // si el atributo es unico
[Table("Servicios")]
public class Servicio : IValidable {
    [Key] // Si la PK es Id entonces la agrega por defecto, si no usar esto
    public int Codigo{get;set}

    [ForeignKey(nameof(Obj))] // si la FK tiene formato NombreObjetoId no es necesario, EF lo agrega por defecto
    public int PacienteId{get;set}

    [Required]
    [Range(0,int.MaxValue)]
    [MaxLenght(int)]
    [MinLength(5)]
    [StringLength(500, MinimumLength = int)]
    [EmailAddress]
}

// ==========================================================================================
// Migrations
// ==========================================================================================

/*
- Add-Migration <nombre descriptivo>

- Update-Database [nombre de la migración de destino]
    - Si no se proporciona nombre de migración de destino, 
        aplica todas las migraciones pendientes en orden hacia adelante 

    - Si se proporciona nombre, 
        revierte todas las migraciones posteriores a la de destino, en orden hacia atrás 

- Remove-Migration
    - Quita de la carpeta Migrations la última migración que NO esté aplicada en la BD

*/

// ==========================================================================================
// .NET WebApi helpers
// ==========================================================================================

/*

200 - Ok                    - Ok()
201 - Created               - Created(ruta, objeto)
204 - No Content            - NoContent()
400 - Bad Request           - BadRequest()
404 - Not Found             - NotFount()
500 - Internal Server Error - InternalServerError()

*/