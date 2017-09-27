using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class SubscriptionTypeDto
    {
        public int SubscriptionTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int QPTrainers { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int QClients { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int QEstablishments { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string Status { get; set; }

        [Required]
        [Range(0, 99999999)]
        public Decimal Price { get; set; }
    }
}