using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Issuer.Models;
using Issuer.Models.Api;

namespace Issuer.HttpClients
{
    public interface IImmunizationClient
    {
        Task<ImmunizationResponse> GetImmunizationRecordAsync(Guid guid);
    }
}
