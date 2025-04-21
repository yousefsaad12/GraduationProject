
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