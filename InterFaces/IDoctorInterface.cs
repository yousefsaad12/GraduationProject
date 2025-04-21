
using GraduationProject.Dtos;

namespace GraduationProject.InterFaces
{
    public interface IDoctorInterface
    {
        public Task<ServicesResult<bool>> AddDoctor(CreateDoctorRequest createDoctorRequest);

        public Task<bool> IsEmailExist(string email);

    }
}