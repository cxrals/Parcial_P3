// ==========================================================================================
// Examen 2023-05-12
// ==========================================================================================

/*
CentroMedico(id, nombre, dirección, teléfono)
Paciente(id, nombre, cedula, idCentroMedico)
Solicitud(id, fecha, idPaciente, completada)
Examen(codigo, nombre, preparación, tipo ) 
SolicitudExamen(SolicitudId, ExamenCodigo)
*/

/*
2. Implementar en .NET Core las clases Solicitud, Paciente y Examen para que Entity Framework genere 
   esas tablas. Incluya las Data Annotations necesarias para que las claves primarias y foráneas de 
   las tablas se generen coherentemente con el esquema anteriormente especificado
*/
[Table("Pacientes")]
public class Paciente : IValidable {
    public int Id {get;set}
    public string Nombre {get;set}
    public string Cedula{get;set}

    [ForeignKey(nameof(CentroMedico))]
    public int CentroMedicoId{get;set}
    public CentroMedico CentroMedico{get;set};
}

[Table("Solicitudes")]
public class Solicitud : IValidable {
    public int Id{get;set}
    public DateTime Fecha{get;set}
    public bool Completada{get;set}
   
    [ForeignKey(nameof(Paciente))]
    public int PacienteId{get;set}
    public Paciente Paciente{get;set}

    public ICollection<SolicitudExamen> Exámenes {get;set;}=new List<Examen>();
}

[Table("Examenes")]
public class Examen : IValidable {
    [Key]
    public int Codigo{get;set}
    public string Nombre{get;set}
    public string Preparacion{get;set}
    public string Tipo{get;set}
    public ICollection<SolicitudExamen> Exámenes {get;set;}=new List<Examen>();
}

/*
3. Implementar el alta de la solicitud. Asuma que al momento de dar de alta la solicitud ya 
   incluye todos los objetos relacionados necesarios. Se asume que todas las clases implementan 
   la interfaz IValidable y lanzan una excepción InvalidOperationException con el mensaje apropiado 
   en caso de que su estado no sea válido
   a. Codificar lo necesario para el alta en la capa de acceso a datos (repositorios) usando Entity Framework Core.
*/

// En LogicaDatos.Repositorios
public class RepositorioSolicitudes : IRepositorioSolicitudes {
    public ClinicaContext Contexto { get; set; }

    public RepositorioSolicitudes(ClinicaContext ctx) {
        Contexto = ctx;
    }

    public void Create(Solicitud obj) {
        obj.Validar();
        try {
            Contexto.Solicitudes.Add(obj);
            Contexto.Entry(obj.Paciente).State = EntityState.Unchanged;
            foreach (Examen e in obj.Examenes) {
                Contexto.Entry(e).State = EntityState.Unchanged;
            }
            Contexto.SaveChanges();
        } catch (Exception e) {
            throw new InvalidOperationException("Mensaje");
        }        
    }
}

/*
3. 
   b. Implementar un método de acción de una WebApi para dar el alta de un servicio.
*/
[Route("api/[controller]")]
[ApiController]
public class SolicitudController : ControllerBase {
    // TODO
}

/*
3. 
   b. Explique muy brevemente qué ventaja implica utilizar inyección de dependencias.
*/

/*
4. Escribir las siguientes consultas LINQ. Indique en qué clase las ubicaría
   a. Implementar una consulta Linq que, dado el id de un paciente, obtenga las solicitudes 
   no completadas de ese paciente ordenadas por fecha de la más vieja a la más reciente. Al 
   obtener las solicitudes deberá incluir también los exámenes solicitados
*/


/*
4. 
   b. Dado el id de un centro médico obtener el nombre y preparación de los exámenes radiológicos 
   que haya solicitado ese centro médico. Cada examen debe aparecer una única vez
*/