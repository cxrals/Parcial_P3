// MVC
public class ObjController : Controller {
    public ICUBaja<Obj> CUBaja {get;set;}
    public ICUBuscarPorId<Obj> CUBuscarPorIdObj { get; set; }

    // ctor

    public ActionResult Delete(int id) {
        // Obj o = llamar cu buscar object por id
        return View(0);
    }

    [HttpPost]
    public ActionResult Delete(int id, Obj o) {
        try {                
            // llamar cu baja
            return RedirectToAction("Index", "Objs");
        } catch(Exception e) {
            ViewBag.Mensaje = e.Message;
            return View();
        }
    }
}
//================================================================
// Capa Aplicacion
public class CUBajaObj : ICUBaja<Obj> {
    public IRepositorioObj Repo {get;set;}

    // ctor

    public void Baja(int id) {
        Repo.Delete(id);
    }
}
//================================================================
// Capa Datos
public class RepositorioObj : IRepositorioObj {
    public DbContext Contexto {get;set;}

    // ctor

    public void Delete(int id) {
        // buscar el obj
        Contexto.Obj.Remove(obj);
        Contexto.SaveChanges();
    }

    public Obj FindById(int id) {
        return Contexto.Obj.Find(id);
    }
}

/*
Metodos crud dbc
    - Contexto.Objs.Add(obj);
    - Contexto.Objs.Remove(obj);
    - Contexto.Objs.Update(obj);
    - Contexto.Objs.Find(id);
*/

// ==========================================================================================
// WebApi
// ==========================================================================================

[Route("api/[controller]")]
[ApiController]
public class ObjsController : ControllerBase {
    // casos de uso

    // ctor

    //================================================================
    //api/objs/
    //Verbo: GET
    [HttpGet]
    public IActionResult Get() {
        try {
            // llamar cu buscar todos
            // si encuentra 
                return Ok(objDTO) // 200
            // si no
                return NotFount("Msg") // 400
        } catch (Exception e) {
            return StatusCode(500, "msg") // 500
        }
    }
    //================================================================
    //api/objs/5
    //Verbo: GET
    [HttpGet("{id}", Name = "BuscarPorId")]
    public IActionResult Get(int id) {
        if (id <= 0) return BadRequest("El id deber ser un entero positivo");
        // llamar cu buscar por id
        if (resultado == null) return NotFound("No existe el record");
        return Ok(resultado);
    }
    //================================================================
    //api/temas/
    //Verbo: POST
    [HttpPost]
    public IActionResult Post([FromBody] ObjDTO objDTO) {
        if (objDTO == null) return BadRequest("No se proporcionaron datos para el alta");
        try {
            // llamar cu alta
            return Created("BuscarPorId", objDTO);
        } catch (ExcepcionPropia ex) {
            return BadRequest(ex.Message);
        } catch (Exception ex) {             
            return StatusCode(500, ex.Message);
        }
    }
    //================================================================
    //api/objs/5
    //Verbo: DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest("El id debe ser un entero positivo");
        try {
            // llamar cu buscar por id
            if (resultado == null) return NotFound("No existe el record");
            // llamar cu baja
            return NoContent();
        } catch (Exception ex) {
            return StatusCode(500, ex.Message);
        }            
    }
    //================================================================
    //api/objs/5
    //Verbo: PUT
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ObjDTO? aModificar) {
        if (id <= 0) return BadRequest("El id debe ser un entero positivo");
        if (aModificar == null) return BadRequest("No se proporcionaron datos para la modificación");
        if (id != aModificar.Id) return BadRequest("Se proporcionaron dos id diferentes");

        try {
            // llamar cu modificar
            return Ok(aModificar);
        } catch (ExcepcionPropiaException ex) {
            return BadRequest(ex.Message);
        } catch (Exception ex) {
            return StatusCode(500, ex.Message);
        }            
    }
}