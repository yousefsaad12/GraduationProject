

using GraduationProject.Dtos;
using GraduationProject.InterFaces;

namespace GraduationProject.Services
{
    public class DoctorServices : IDoctorInterface
    {
        private readonly ApplicationDb _context;

        public DoctorServices(ApplicationDb context)
        {
            _context = context;
        }
        public Task<bool> AddDoctor(CreateDoctorRequest createDoctorRequest)
        {
            throw new NotImplementedException();
        }
    }
}