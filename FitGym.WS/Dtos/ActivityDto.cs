using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StarTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string Status { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int ClientId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int EstablishmentId { get; set; }
    }
}