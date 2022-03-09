using Acturis.Models.BlueLight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acturis.Interfaces
{
    public interface IBluelightApiService
    {
        Task<List<Bluelight>> GetMembersAsync();
        Task PostMembership(List<ActurisMembership> acturisMemberships);
        Task PostMembershipCertificates(List<ActurisCertificate> acturisCertificates, string Id);

    }
}
