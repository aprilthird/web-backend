using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class ClientDto
    {
        public int ClientId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Password { get; set; }
        
        public string Status { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int PersonalTrainerId { get; set; }

        [Required]
        [Range(0, 9)]
        public decimal Height { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string Gender { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string PhotoUrl { get; set; }
    }
}