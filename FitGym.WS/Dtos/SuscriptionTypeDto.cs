using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class SuscriptionTypeDto
    {
        public int SuscriptionTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}