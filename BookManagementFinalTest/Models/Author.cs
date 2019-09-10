using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookManagementFinalTest.Models
{
    public class Author
    {
        public Author() => this.Book = new HashSet<Book>();

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [MinLength(2, ErrorMessage = "Minimum length should be 2")]
        [MaxLength(50, ErrorMessage = "Maximum length should be 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First name includes only alphabets")]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [MinLength(2, ErrorMessage = "Minimum length should be 2")]
        [MaxLength(50, ErrorMessage = "Maximum length should be 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last name includes only alphabets")]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression(@"^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$", ErrorMessage = "Email address should be in the format abc@domain.com")]
        [MaxLength(50, ErrorMessage = "Maximum length should be 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter contact number")]
        [MinLength(10, ErrorMessage = "Please enter contact number of 10 digits")]
        [RegularExpression(@"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$", ErrorMessage = "Please enter valid contact")]
        public string Contact { get; set; }

        [Display(Name = "Date added"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> RegisterDate { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}
