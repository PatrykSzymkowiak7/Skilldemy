using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Skilldemy.Models
{
    public class CategoryDDLValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var createCourseViewModel = (CreateCourseViewModel)validationContext.ObjectInstance;

            if(createCourseViewModel != null)
            {
                if(createCourseViewModel.CategoryId == 0)
                {
                    return new ValidationResult("Należy wybrać kategorię");
                }
            }

            return ValidationResult.Success;
        }
    }
}