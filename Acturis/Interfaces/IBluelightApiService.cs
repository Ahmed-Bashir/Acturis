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
        Task<List<Bluelight>> GetNameChangeAsync();
        Task<List<Bluelight>> GetCancellationsAsync();
        Task PostMembership(ActurisMembership acturisMemberships);
        Task PostMembershipCertificates(ActurisMembership acturisMembership);
        Task ClearNameChange(ActurisMembership acturisMembership);

    }
}
