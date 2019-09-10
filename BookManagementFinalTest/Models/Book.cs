using System;
using System.ComponentModel.DataAnnotations;

namespace BookManagementFinalTest.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter book name")]
        [MinLength(2, ErrorMessage = "Minimum length should be 2")]
        [MaxLength(50, ErrorMessage = "Maximum length should be 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name includes only alphabets")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter book price")]
        [RegularExpression(@"^\d{0,8}$", ErrorMessage = "Please enter valid price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Please enter book description")]
        [MinLength(5, ErrorMessage = "Minimum length should be 10")]
        [MaxLength(200, ErrorMessage = "Maximum length should be 500")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select book author")]
        [Display(Name = "Author name")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Please enter total pages of book")]
        [Display(Name = "Number of pages")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Pages should be grater than 0")]
        public int NumOfPages { get; set; }

        [Required(ErrorMessage = "Please select book language")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Please select category")]
        public Category Category { get; set; }

        [Display(Name = "Date added"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> RegisterDate { get; set; }

        public virtual Author Author { get; set; }
    }

    public enum Category
    {
        [Display(Name = "Educational")]
        Educational = 1,
        [Display(Name = "Religious")]
        Religious = 2,
        [Display(Name = "Comic")]
        Comic = 3
    }
}
