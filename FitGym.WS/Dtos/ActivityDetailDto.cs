using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class ActivityDetailDto
    {
        public int ActivityDetailId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int QRepetition { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int ActivityId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int ActivityTypeId { get; set; }

        [Required]
        [StringLength(120)]
        public string Description { get; set; }
    }
}