using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Issuer.HttpClients;
using Issuer.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Issuer.Services
{
    public class PatientService : BaseService, IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IImmunizationClient _immunizationClient;
        public PatientService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IImmunizationClient immunizationClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _immunizationClient = immunizationClient;
        }

        public async Task<bool> PatientExistsAsync(int patientId)
        {
            return await _context.Patients
                .AsNoTracking()
                .AnyAsync(e => e.Id == patientId);
        }

        public async Task<bool> UserIdExistsAsync(string userId)
        {
            return await _context.Patients
                .AsNoTracking()
                .AnyAsync(e => e.UserId == userId);
        }

        public async Task<Patient> GetPatientForUserIdAsync(string userId)
        {
            return await _context.Patients
                .Include(e => e.Connections)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<Patient> GetPatientAsync(int patientId)
        {
            return await _context.Patients
                .Include(e => e.Connections)
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == patientId);
        }

        public async Task<int> CreatePatientAsync(Patient newPatient)
        {
            newPatient.ThrowIfNull(nameof(newPatient));

            _context.Patients.Add(newPatient);

            var created = await _context.SaveChangesAsync();
            if (created < 1)
            {
                throw new InvalidOperationException("Could not create patient.");
            }

            return newPatient.Id;
        }

        public async Task<IEnumerable<CredentialViewModel>> GetPatientCredentialsAsync(int patientId)
        {
            return await _context.Credentials
                .Include(c => c.Connection)
                .Where(c => c.Connection.PatientId == patientId)
                .ProjectTo<CredentialViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
