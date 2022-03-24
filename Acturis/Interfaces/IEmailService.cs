using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acturis.Interfaces
{
    public interface IEmailService
    {

        Task ReportUnsuccessfulUpload(dynamic member, string error);
        Task Report(string error);


    }
}
