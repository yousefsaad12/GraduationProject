

using GraduationProject.Dtos;
using GraduationProject.InterFaces;
using GraduationProject.Mapping;
using GraduationProject.Utilities;

namespace GraduationProject.Services
{
    public class DoctorServices : IDoctorInterface
    {
        private readonly ApplicationDb _context;

        public DoctorServices(ApplicationDb context)
        {
            _context = context;
        }
        public async Task<ServicesResult<bool>> AddDoctor(CreateDoctorRequest createDoctorRequest)
        {
            try
            {
                if (createDoctorRequest is null) return ServicesResult<bool>.Fail("Request is null.");

                if (await IsEmailExist(createDoctorRequest.Email)) return ServicesResult<bool>.Fail("Email already exists.");

                Doctor doctor = createDoctorRequest.CreateDoctorRequestToDoctorMapper();

                await _context.Doctors.AddAsync(doctor);

                int result = await _context.SaveChangesAsync();

                return result > 0 ?
                   ServicesResult<bool>.Ok(true, "Doctor added successfully.") :
                   ServicesResult<bool>.Fail("No changes were saved.");
            }

            catch (Exception ex)
            {
                return ServicesResult<bool>.Fail(ex.Message);
            }

        }

        public async Task<IReadOnlyList<DoctorResponse>> GetAllDoctors()
        {
            return await _context.Doctors.Select(d => d.DoctorToDoctorResponseMapper()).ToListAsync();
        }

        public Task<bool> IsEmailExist(string email)
        {
            return _context.Doctors.AnyAsync(d => d.Email == email);
        }
    }
}