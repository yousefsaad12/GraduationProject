

namespace GraduationProject.Utilities
{
    public static class DoctorUpdateHelper
    {
        public static void MapUpdate(Doctor doctor, CreateDoctorRequest createDoctorRequest)
        {
            doctor.Name = createDoctorRequest.Name;
            doctor.Specialization = createDoctorRequest.Specialization;
            doctor.Email = createDoctorRequest.Email;
            doctor.PhoneNumber = createDoctorRequest.PhoneNumber;
            doctor.Country = createDoctorRequest.Country;
            doctor.Location = createDoctorRequest.Location;
        }
    }
}