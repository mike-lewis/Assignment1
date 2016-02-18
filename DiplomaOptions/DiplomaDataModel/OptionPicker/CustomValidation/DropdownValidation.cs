using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DiplomaDataModel.OptionPicker.CustomValidation
{
    public class DropdownValidation : ValidationAttribute
    {
        public HashSet<int> hash;
        public DropdownValidation()
        {
            hash = new HashSet<int>();
        }

        protected override ValidationResult IsValid(object obj, ValidationContext context)
        {
            var selections = new[] {obj.GetType().GetProperty("FirstChoiceOptionId", BindingFlags.Instance | BindingFlags.Public).GetValue(obj),
                                   obj.GetType().GetProperty("SecondChoiceOptionId", BindingFlags.Instance | BindingFlags.Public).GetValue(obj),
                                   obj.GetType().GetProperty("ThirdChoiceOptionId", BindingFlags.Instance | BindingFlags.Public).GetValue(obj),
                                   obj.GetType().GetProperty("FourthChoiceOptionId", BindingFlags.Instance | BindingFlags.Public).GetValue(obj),
            };

            for (int i = 0; i < selections.Length; i++)
            {
                if (!hash.Contains((int)selections[i]))
                {
                    hash.Add((int)selections[i]);
                }
                else
                {
                    hash.Clear();
                    return new ValidationResult("You cannot select the same option twice.");
                }
            }
            hash.Clear();
            return ValidationResult.Success;
        }
    }
}
