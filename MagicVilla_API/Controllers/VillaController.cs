using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Dto;
using MagicVilla_API.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;   
            _db = db;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villas ");

            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }


        [HttpGet("{id}", Name = "GetVilla")]
         public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if(id== 0)
            {
                _logger.LogError("Error al traer Villa con Id " + id);
                return BadRequest();
            }
           // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
           var villa = await _db.Villas.FirstOrDefaultAsync(v=> v.Id== id); 

            if(villa== null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }


        [HttpPost]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _db.Villas.FirstOrDefaultAsync(v=>v.Nombre.ToLower()== createDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese nombre ya existe");
                return BadRequest(ModelState);  
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            Villa modelo = _mapper.Map<Villa>(createDto);
           

            await _db.Villas.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {id=modelo.Id},modelo);
           
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id== 0)
            {
                return BadRequest();
            }
            var villa =await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();  
            }

           _db.Villas.Remove(villa);
           await _db.SaveChangesAsync();

            return NoContent(); 
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if(updateDto == null || id!= updateDto.Id)
            {
                return BadRequest();
            }
           
            Villa modelo = _mapper.Map<Villa>(updateDto);
          

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
       
            return NoContent();
        }
    }
}
