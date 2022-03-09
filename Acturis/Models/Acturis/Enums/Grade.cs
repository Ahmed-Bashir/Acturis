using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acturis.Models.Acturis
{
    
    
    enum Grade
    {
        [Display(Name = "Childminder")]
        Childminder = 291061,
        [Display(Name = "Registered Nanny")]
        RegisteredNanny = 291062,
        [Display(Name = "Non-Registered Nanny")]
        NonRegisteredNanny = 293752,

        
    }
}
