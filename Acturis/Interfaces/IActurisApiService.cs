using Acturis.Models;
using Acturis.Models.BlueLight;
using Acturis.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acturis.Interface
{
    public interface IActurisApiService
    {
        Task<List<ActurisMembership>> GetCertificatesAsync();
        Task<List<ActurisMembership>> RenewPolicyAsync();
        Task<List<string>> CancelPolicyAsync();
        Task<LocatePolicy> LocatePolicyUploadAsync(Response jobId); 
        Task<LocatePolicyResponse> locatePolicyAsync(LocatePolicy locatePolicy);
        Task<LocatePartActivityResponse> LocatePartActivityAsync(LocatePolicyResponse locatePolicyResponse);
        Task<ActurisMembership> GetDocumentFromActivityAsync(LocatePartActivityResponse locatePartActivityResponse);
        Task SendApprovedMembersAsync();

    }
}
