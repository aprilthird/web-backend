using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitGym.WS.Dtos
{
    public class PersonalTrainerDto
    {
        public int PersonalTrainerId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(120)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(120)]
        public string LastName { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int GymCompanyId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(200)]
        public string Address { get; set; }
        
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string Gender { get; set; }

        public string PhotoUrl { get; set; }
    }
}