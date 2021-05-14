using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Issuer.Models.Api
{
    public class PatientResponse
    {
        public string FullName { get; set; }

        public string BirthDate { get; set; }

        public string Reference { get; set; }

    }
}
