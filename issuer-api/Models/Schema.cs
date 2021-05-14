using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Issuer.Models
{
    public class Schema
    {
        public string name { get; set; }
        public string description { get; set; }
        public string expirationDate { get; set; }
        public string credential_type { get; set; }
        public string countryOfVaccination { get; set; }
        public string recipient_type { get; set; }
        public string recipient_fullName { get; set; }
        public string recipient_birthDate { get; set; }
        public string vaccine_type { get; set; }
        public string vaccine_disease { get; set; }
        public string vaccine_medicinalProductName { get; set; }
        public string vaccine_marketingAuthorizationHolder { get; set; }
        public string vaccine_dateOfVaccination { get; set; }

    }
}
