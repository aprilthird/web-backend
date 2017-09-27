using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class DietDto
    {
        public int DietId { get; set; }

        [Required]
        [Range(0, 99999)]
        public decimal TotalCalories { get; set; }
    }
}