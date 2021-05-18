using System;

namespace Issuer.Models
{
    public class CredentialViewModel
    {
        public Guid Guid { get; set; }
        public DateTimeOffset? AcceptedCredentialDate { get; set; }
    }
}
