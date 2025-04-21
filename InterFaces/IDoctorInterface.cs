

using GraduationProject.Dtos;

namespace GraduationProject.InterFaces
{
    public interface IDoctorInterface
    {
        public Task<bool> AddDoctor(CreateDoctorRequest createDoctorRequest);

    }
}