using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Issuer.Models;
using Issuer.Models.Api;
using Issuer.Services;

namespace Issuer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IVerifiableCredentialService _verifiableCredentialService;

        public PatientsController(
            IPatientService patientService,
            IVerifiableCredentialService verifiableCredentialService)
        {
            _patientService = patientService;
            _verifiableCredentialService = verifiableCredentialService;
        }

        // GET: api/Patients/1234567890/auth
        /// <summary>
        /// Gets Patient by UserId
        /// </summary>
        /// <param name="userId"></param>
        // TODO temporarily added /auth to distinguish between the getBy endpoints until a
        // proper GUID is provided and.Net can distinguish between the URI param data types
        [HttpGet("{userId}/auth", Name = nameof(GetPatientByUserId))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // [ProducesResponseType(typeof(ApiResultResponse<Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPatientByUserId(string userId)
        {
            var patient = await _patientService.GetPatientForUserIdAsync(userId);
            // return Ok(ApiResponse.Result( patient ));
            return Ok(patient);
        }

        // GET: api/Patients/5
        /// <summary>
        /// Gets a specific Patient.
        /// </summary>
        /// <param name="patientId"></param>
        [HttpGet("{patientId}", Name = nameof(GetPatientById))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        // [ProducesResponseType(typeof(ApiResultResponse<Patient>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPatientById(int patientId)
        {
            var patient = await _patientService.GetPatientAsync(patientId);
            if (patient == null)
            {
                return NotFound(ApiResponse.Message($"Patient not found with id {patientId}"));
            }

            // return Ok(ApiResponse.Result(patient));
            return Ok(patient);
        }

        // POST: api/Patients
        /// <summary>
        /// Creates a new Patient.
        /// </summary>
        [HttpPost(Name = nameof(CreatePatient))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        // [ProducesResponseType(typeof(ApiResultResponse<Patient>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreatePatient(Patient patient)
        {
            if (patient == null)
            {
                ModelState.AddModelError("Patient", "Could not create an patient, the passed in Patient cannot be null.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            if (await _patientService.UserIdExistsAsync(patient.UserId))
            {
                ModelState.AddModelError("Patient.UserId", "An patient already exists for this User Id, only one patient is allowed per User Id.");
                return BadRequest(ApiResponse.BadRequest(ModelState));
            }

            var createdPatientId = await _patientService.CreatePatientAsync(patient);
            patient = await _patientService.GetPatientAsync(createdPatientId);

            return CreatedAtAction(
                nameof(GetPatientById),
                new { patientId = createdPatientId },
                // ApiResponse.Result(patient)
                patient
            );
        }

        // POST: api/Patients/5/credential
        /// <summary>
        /// Issues Credentials for a patient.
        /// Creates a new connection if no active connection exists.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="identifiers"></param>
        [HttpPost("{patientId}/credential", Name = nameof(Credential))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Credential(int patientId, List<Identifier> identifiers)
        {
            var patient = await _patientService.GetPatientAsync(patientId);
            if(patient == null)
            {
                return BadRequest();
            }

            var qrCode = await _verifiableCredentialService.IssueCredentialsAsync(patient, identifiers);

            // return qrcode if invitation not yet accepted, otherwise nothing
            return Ok(qrCode);
        }

        // GET: api/Patients/5/credentials
        /// <summary>
        /// Gets a patients credentials
        /// </summary>
        /// <param name="patientId"></param>
        [HttpGet("{patientId}/credentials", Name = nameof(GetPatientCredentials))]
        [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        // [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<CredentialViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CredentialViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetPatientCredentials(int patientId)
        {
            var patient = await _patientService.GetPatientAsync(patientId);
            if (patient == null)
            {
                return NotFound(ApiResponse.Message($"Patient not found with id {patientId}"));
            }

            var credentials = await _patientService.GetPatientCredentialsAsync(patientId);

            // return Ok(ApiResponse.Result(credentials));
            return Ok(credentials);
        }

    }
}
