
using GraduationProject.InterFaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace GraduationProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorInterface _doctorInterface;

        public DoctorController(IDoctorInterface doctorInterface)
        {
            _doctorInterface = doctorInterface;
        }

        [HttpGet]
        [Route("GetAllDoctor")]
        public async Task<IActionResult> GetAllDoctor()
        {
            IReadOnlyList<DoctorResponse> doctorResponses = await _doctorInterface.GetAllDoctors().ConfigureAwait(false);

            return Ok(doctorResponses);
        }

        [HttpPost]
        [Route("AddDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody] CreateDoctorRequest createDoctorRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ServicesResult<bool> result = await _doctorInterface.AddDoctor(createDoctorRequest).ConfigureAwait(false);

            if (result.Success) return Created(string.Empty, "Doctor successfully added.");

            return BadRequest(result);

        }
    }
}