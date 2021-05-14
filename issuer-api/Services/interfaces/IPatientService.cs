using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Issuer.Models;

namespace Issuer.Services
{
    public interface IPatientService
    {
        Task<Patient> GetPatientForUserIdAsync(string userId);
        Task<Patient> GetPatientAsync(int patientId);
        Task<bool> PatientExistsAsync(int patientId);
        Task<bool> UserIdExistsAsync(string userId);
        Task<int> CreatePatientAsync(Patient patient);
        Task<IEnumerable<CredentialViewModel>> GetPatientCredentialsAsync(int patientId);
    }
}
