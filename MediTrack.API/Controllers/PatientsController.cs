using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediTrack.API.DTOs.Patient;
using MediTrack.API.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MediTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)

        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResponseDto>>> GetAll()
        {
            var patients = await _patientService.GetAllAsync();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResponseDto>> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if(patient == null)
            {
                return NotFound(new { message = $"Patient with id {id} not found." });
            }
            return Ok(patient);
        }
        [HttpPost]
        public async Task<ActionResult<PatientResponseDto>> Create([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _patientService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost]
        public async Task<ActionResult<PatientResponseDto>> Update([FromBody] UpdatePatientDto dto)
        {
            var updated = await _patientService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _patientService.DeleteAsync(id);
            if(!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }



            }
}
