
using GraduationProject.InterFaces;
using GraduationProject.Mapping;

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

                await _context.Doctors.AddAsync(doctor).ConfigureAwait(false);

                int result = await _context.SaveChangesAsync().ConfigureAwait(false);

                return result > 0 ?
                   ServicesResult<bool>.Ok(true, "Doctor added successfully.") :
                   ServicesResult<bool>.Fail("No changes were saved.");
            }

            catch (Exception ex)
            {
                return ServicesResult<bool>.Fail(ex.Message);
            }

        }

        public async Task<ServicesResult<bool>> DeleteDoctor(int id)
        {
            if (!await IsIdExist(id)) return ServicesResult<bool>.Fail("Id not exists.");

            Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);

            _context.Doctors.Remove(doctor);

            int result = await _context.SaveChangesAsync().ConfigureAwait(false);

            return result > 0 ?
                   ServicesResult<bool>.Ok(true, "Doctor has been deleted successfully.") :
                   ServicesResult<bool>.Fail("No changes were saved.");
        }

        public async Task<IReadOnlyList<DoctorResponse>> GetAllDoctors()
        {
            return await _context.Doctors.Select(d => d.DoctorToDoctorResponseMapper()).ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Doctors.AnyAsync(d => d.Email == email).ConfigureAwait(false);
        }
        public async Task<bool> IsIdExist(int id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id).ConfigureAwait(false);
        }
    }
}