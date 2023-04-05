using MagicVilla_API.Datos;
using MagicVilla_API.Dto;
using MagicVilla_API.Modelos;
using Microsoft.AspNetCore.Mvc;


namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;


        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;   
            _db = db;
        }


        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villas ");
            return Ok(_db.Villas.ToList());
        }


        [HttpGet("{id}", Name = "GetVilla")]
         public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id== 0)
            {
                _logger.LogError("Error al traer Villa con Id " + id);
                return BadRequest();
            }
           // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
           var villa= _db.Villas.FirstOrDefault(v=> v.Id== id); 

            if(villa== null)
            {
                return NotFound();
            }

            return Ok(villa);
        }


        [HttpPost]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(_db.Villas.FirstOrDefault(v=>v.Nombre.ToLower()== villaDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                return BadRequest(ModelState);  
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa modelo = new()
            {
               
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            _db.Villas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new {id=villaDto.Id},villaDto);
           
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteVilla(int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();  
            }

           _db.Villas.Remove(villa);
           _db.SaveChanges();

            return NoContent(); 
        }

        [HttpPut("{id}")]

        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if(villaDto == null || id!= villaDto.Id)
            {
                return BadRequest();
            }
          /*  var villa=VillaStore.villaList.FirstOrDefault(v=> v.Id == id);
            villa.Nombre = villaDto.Nombre;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;*/

            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            _db.Villas.Update(modelo);
            _db.SaveChanges();
       
            return NoContent();
        }
    }
}
