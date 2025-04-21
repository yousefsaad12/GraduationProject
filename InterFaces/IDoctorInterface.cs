

namespace GraduationProject.InterFaces
{
    public interface IDoctorInterface
    {
        public Task<ServicesResult<bool>> AddDoctor(CreateDoctorRequest createDoctorRequest);
        public Task<IReadOnlyList<DoctorResponse>> GetAllDoctors();
        public Task<bool> IsEmailExist(string email);

    }
}