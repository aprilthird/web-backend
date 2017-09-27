using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class ActivityTypeDto
    {
        public int ActivityTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(80)]
        public string Description { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int GymCompanyId { get; set; }
    }
}