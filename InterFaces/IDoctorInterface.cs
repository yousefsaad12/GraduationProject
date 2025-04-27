

namespace GraduationProject.InterFaces
{
    public interface IDoctorInterface
    {
        public Task<ServicesResult<bool>> AddDoctor(CreateDoctorRequest createDoctorRequest);

        public Task<ServicesResult<bool>> UpdateDoctor(CreateDoctorRequest updateDoctorRequest, int id);
        public Task<ServicesResult<bool>> DeleteDoctor(int id);
        public Task<IReadOnlyList<DoctorResponse>> GetAllDoctors();
        public Task<Doctor> GetDoctor(int id);
        public Task<bool> IsEmailExist(string email);

    }
}