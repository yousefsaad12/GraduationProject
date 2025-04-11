namespace GraduationProject.Models
{
    public class Doctor
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Specialization { set; get; }
        public string Email { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Country { set; get; }
        public string? Location { set; get; }
    }
}