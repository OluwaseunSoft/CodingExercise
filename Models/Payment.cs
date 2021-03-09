using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CodingExercise.Validators;

namespace CodingExercise.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        [DateValidator(ErrorMessage ="Expiry Message Can't Be In The Past", ErrorMessageResourceName ="")]
        public DateTime ExpirationDate { get; set; }

        [StringLength(3)]
        public string SecurityCode { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
