using DiplomaDataModel.OptionPicker.CustomValidation;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.OptionPicker
{
    [DiplomaDataModel.OptionPicker.CustomValidation.DropdownValidation()]
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }

        [Display(Name = "Year Term ID:")]
        [ForeignKey("YearTerm")]
        public int YearTermId { get; set; }

        [ForeignKey("YearTermId")]
        public YearTerm YearTerm { get; set; }

        [Display(Name = "Student ID:")]
        [MaxLength(9, ErrorMessage = "StudentId cannot be longer than 9 characters.")]
        public string StudentId { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(40, ErrorMessage = "Student first name cannot be longer than 40 characters.")]
        public string StudentFirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(40, ErrorMessage = "Student last name cannot be longer than 40 characters.")]
        public string StudentLastName { get; set; }

        [Display(Name = "First Choice:")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }

        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }

        [Display(Name = "Second Choice:")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }

        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }

        [Display(Name = "Third Choice:")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }

        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }

        [Display(Name = "Fourth Choice:")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }

        [ForeignKey("FourthChoiceOptionId")]
        public Option FourthOption { get; set; }

        private DateTime _SelectionDate = DateTime.MinValue;

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyy}")]
        public DateTime SelectionDate
        {
            get
            {
                return (_SelectionDate == DateTime.MinValue) ? DateTime.Now : _SelectionDate;
            }
            set { _SelectionDate = value; }
        }

    }
}
