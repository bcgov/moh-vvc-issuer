using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Issuer.Models.Api
{
    public class ImmunizationResponse
    {
        public string FullUrl { get; set; }

        public string Id { get; set; }

        public PatientResponse Patient { get; set; } = new PatientResponse();

        public VaccineResponse Vaccine { get; set; } = new VaccineResponse();

        public string LotNumber { get; set; }

        public string DateOfVaccination { get; set; }

        public int DoseNumber { get; set; }

        public string CountryOfVaccination { get; set; }

        public string AdministeringCentre { get; set; }

        public string NextVaccinationDueDate { get; set; }

    }
}
