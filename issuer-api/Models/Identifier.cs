using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace Issuer.Models
{
    [Table("Identifier")]
    public class Identifier : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Uri { get; set; }
    }
}
