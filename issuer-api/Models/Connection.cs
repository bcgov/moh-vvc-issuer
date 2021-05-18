using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Issuer.Models
{
    [Table("Connection")]
    public class Connection : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }

        public string ConnectionId { get; set; }

        public string Base64QRCode { get; set; }

        public DateTimeOffset? AcceptedConnectionDate { get; set; }

        [JsonIgnore]
        public ICollection<Credential> Credentials { get; set; }

    }
}
