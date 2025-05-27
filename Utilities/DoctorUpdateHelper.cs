

namespace GraduationProject.Utilities
{
    public static class DoctorUpdateHelper
    {
        public static void MapUpdate(Doctor doctor, CreateDoctorRequest createDoctorRequest)
        {
            doctor.Name = createDoctorRequest.name;
            doctor.Specialization = createDoctorRequest.specialization;
            doctor.Email = createDoctorRequest.email;
            doctor.PhoneNumber = createDoctorRequest.phoneNumber;
            doctor.Country = createDoctorRequest.country;
            doctor.Location = createDoctorRequest.location;
        }
    }
}