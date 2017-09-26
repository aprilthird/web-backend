using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class GymCompanyDto
    {
        public int GymCompanyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string Status { get; set; }
    }
}