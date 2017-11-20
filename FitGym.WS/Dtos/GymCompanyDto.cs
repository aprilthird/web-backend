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
        [StringLength(100)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        
        public string Status { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public string UrlLogo { get; set; }
    }
}