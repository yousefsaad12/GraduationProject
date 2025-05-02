
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
        [Route("/GetAllDoctor")]
        public async Task<IActionResult> GetAllDoctor()
        {
            IReadOnlyList<DoctorResponse> doctorResponses = await _doctorInterface.GetAllDoctors().ConfigureAwait(false);

            if (doctorResponses is null) return ok
        }
    }
}