using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Issuer.Models.Api
{
    public class VaccineResponse
    {
        public Identifier Identifier { get; set; }

        public string Manufacturer { get; set; }

        public string ProductName { get; set; }

        public string Disease { get; set; }
    }
}
