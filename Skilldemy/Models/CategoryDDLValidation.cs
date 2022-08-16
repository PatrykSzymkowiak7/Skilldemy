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
            var type = value.GetType();

            if(type.Name == "CreateCourseViewModel")
            {
                var createCourseViewModel = (CreateCourseViewModel)validationContext.ObjectInstance;

                if (createCourseViewModel != null)
                {
                    if (createCourseViewModel.CategoryId == 0)
                    {
                        return new ValidationResult("Należy wybrać kategorię");
                    }
                }
            }
            else if(type.Name == "EditCourseViewModel")
            {
                var createCourseViewModel = (EditCourseViewModel)validationContext.ObjectInstance;

                if (createCourseViewModel != null)
                {
                    if (createCourseViewModel.CategoryId == 0)
                    {
                        return new ValidationResult("Należy wybrać kategorię");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}