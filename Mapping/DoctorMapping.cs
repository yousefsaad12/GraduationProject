
namespace GraduationProject.Mapping
{
    public static class DoctorMapping
    {
        public static Doctor CreateDoctorRequestToDoctorMapper(this CreateDoctorRequest createDoctorRequest)
        {
            return new Doctor()
            {
                Name = createDoctorRequest.name,
                Specialization = createDoctorRequest.specialization,
                Email = createDoctorRequest.email,
                PhoneNumber = createDoctorRequest.phoneNumber,
                Country = createDoctorRequest.country,
                Location = createDoctorRequest.location,
            };
        }

        public static DoctorResponse DoctorToDoctorResponseMapper(this Doctor doctor)
        {
            return new DoctorResponse()
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Email = doctor.Email,
                Country = doctor.Country,
                PhoneNumber = doctor.PhoneNumber,
                Specialization = doctor.Specialization,
                Location = doctor.Location,
            };
        }
    }
}