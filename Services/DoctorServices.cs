using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GraduationProject.InterFaces;
using GraduationProject.Mapping;

namespace GraduationProject.Services
{
    public class DoctorServices : IDoctorInterface
    {
        private readonly ApplicationDb _context;

        public DoctorServices(ApplicationDb context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Database context cannot be null.");
        }

        public async Task<ServicesResult<bool>> AddDoctor(CreateDoctorRequest createDoctorRequest)
        {
            try
            {
                Console.WriteLine("Start AddDoctor");

                // Check if the request is null
                if (createDoctorRequest is null)
                    return ServicesResult<bool>.Fail("Request is null.");

                Console.WriteLine($"createDoctorRequest.Name: {createDoctorRequest.name}");
                Console.WriteLine($"createDoctorRequest.Email: {createDoctorRequest.email}");

                // Check if email is null
                if (string.IsNullOrEmpty(createDoctorRequest.email))
                    return ServicesResult<bool>.Fail("Email is null or empty in request.");

                // Check if the email already exists
                if (await IsEmailExist(createDoctorRequest.email))
                    return ServicesResult<bool>.Fail("Email already exists.");

                // Map the CreateDoctorRequest to a Doctor object
                Doctor doctor = createDoctorRequest.CreateDoctorRequestToDoctorMapper();

                // Check if the doctor mapping failed
                if (doctor is null)
                {
                    Console.WriteLine("Mapping failed. Doctor object is null.");
                    return ServicesResult<bool>.Fail("Doctor mapping failed: returned null.");
                }

                Console.WriteLine($"doctor.Name: {doctor.Name}");

                // Add the doctor to the database and save changes
                await _context.Doctors.AddAsync(doctor);
                int result = await _context.SaveChangesAsync();

                // Check if changes were saved successfully
                return result > 0
                    ? ServicesResult<bool>.Ok(true, "Doctor added successfully.")
                    : ServicesResult<bool>.Fail("No changes were saved.");
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure
                Console.WriteLine($"Error adding doctor: {ex.Message}");
                return ServicesResult<bool>.Fail($"Error adding doctor: {ex.Message}");
            }
        }

        public async Task<ServicesResult<bool>> DeleteDoctor(int id)
        {
            try
            {
                if (!await IsIdExist(id))
                    return ServicesResult<bool>.Fail("Doctor with the provided ID does not exist.");

                Doctor doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);

                if (doctor == null)
                    return ServicesResult<bool>.Fail("Doctor not found.");

                _context.Doctors.Remove(doctor);

                int result = await _context.SaveChangesAsync().ConfigureAwait(false);

                return result > 0
                    ? ServicesResult<bool>.Ok(true, "Doctor has been deleted successfully.")
                    : ServicesResult<bool>.Fail("No changes were saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting doctor: {ex.Message}");
                return ServicesResult<bool>.Fail($"Error deleting doctor: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<DoctorResponse>> GetAllDoctors()
        {
            try
            {
                return await _context.Doctors
                    .Select(d => d.DoctorToDoctorResponseMapper())
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching doctors: {ex.Message}");
                return new List<DoctorResponse>(); // Return an empty list on failure
            }
        }

        public async Task<Doctor> GetDoctor(int id)
        {
            try
            {
                return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching doctor: {ex.Message}");
                return null; // Return null if an error occurs
            }
        }

        public async Task<bool> IsEmailExist(string email)
        {
            try
            {
                return await _context.Doctors.AnyAsync(d => d.Email == email).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking email existence: {ex.Message}");
                return false; // Return false if an error occurs
            }
        }

        public async Task<bool> IsIdExist(int id)
        {
            try
            {
                return await _context.Doctors.AnyAsync(d => d.Id == id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking doctor ID existence: {ex.Message}");
                return false; // Return false if an error occurs
            }
        }

        public async Task<ServicesResult<bool>> UpdateDoctor(CreateDoctorRequest updateDoctorRequest, int id)
        {
            try
            {
                if (updateDoctorRequest is null)
                    return ServicesResult<bool>.Fail("Update has failed, request is null.");

                Doctor doctor = await GetDoctor(id);

                if (doctor == null)
                    return ServicesResult<bool>.Fail("Doctor not found.");

                // Map the update request to the doctor
                DoctorUpdateHelper.MapUpdate(doctor, updateDoctorRequest);

                int result = await _context.SaveChangesAsync().ConfigureAwait(false);

                return result > 0
                    ? ServicesResult<bool>.Ok(true, "Doctor has been updated successfully.")
                    : ServicesResult<bool>.Fail("No changes were saved.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating doctor: {ex.Message}");
                return ServicesResult<bool>.Fail($"Error updating doctor: {ex.Message}");
            }
        }
    }
}
