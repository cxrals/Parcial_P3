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

// builder.Services.AddScoped<IRepositorySample, RepositorySample>();

// string conStr = builder.Configuration.GetConnectionString("MiConexion");

// builder.Servides.AddDbContext<ContextClass>(options => options.UseSqlServer(conStr));