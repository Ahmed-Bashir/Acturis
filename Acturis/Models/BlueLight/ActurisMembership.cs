using System;
using System.Collections.Generic;
using System.Text;

namespace Acturis.Models.BlueLight
{
    public class ActurisMembership
    {

        public string Id { get; set; }

        public List<ActurisCertificate> Certificates { get; set; }
        public string ActurisIntegrationDate { get; set; } = DateTime.Now.ToString("s");
    }
}
