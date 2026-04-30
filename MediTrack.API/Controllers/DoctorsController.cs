using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediTrack.API.DTOs.Doctor;
using MediTrack.API.Interfaces;



namespace MediTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DoctorResponseDto>>> GetAll()
            => Ok(await _doctorService.GetAllSync());

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DoctorResponseDto>> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return doctor == null? NotFound() : Ok(doctor);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<DoctorResponseDto>> Create([FromBody] CreateDoctorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _doctorService.CreateIdAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>Delete(int id)
        {
            var deleted = await _doctorService.DeleteIdAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }

}
