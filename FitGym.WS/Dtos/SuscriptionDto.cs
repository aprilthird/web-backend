using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class SuscriptionDto
    {
        public int SuscriptionId { get; set; }

        [Required]
        public int QMonths { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int SuscriptionTypeId { get; set; }

        [Required]
        public int GymCompanyId { get; set; }
    }
}