
namespace GraduationProject.Mapping
{
    public static class DoctorMapping
    {
        public static Doctor CreateDoctorRequestToDoctorMapper(this CreateDoctorRequest createDoctorRequest)
        {
            return new Doctor()
            {
                Name = createDoctorRequest.Name,
                Email = createDoctorRequest.Email,
                Country = createDoctorRequest.Country,
                PhoneNumber = createDoctorRequest.PhoneNumber,
                Specialization = createDoctorRequest.Specialization,
                Location = createDoctorRequest.Location,
            };
        }
    }
}