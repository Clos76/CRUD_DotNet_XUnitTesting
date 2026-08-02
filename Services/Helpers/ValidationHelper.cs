using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            //model VAlidations--
            //--- 1/ referenace to model to validate
            ValidationContext validationContext = new ValidationContext(obj); //supply model object to validate, 
            //2. list of validation errors to store. -
            List<ValidationResult> validationResults = new List<ValidationResult>();
            //3. Validator -validates the entire object, validation context, validation results of errors,
            //-- bool true to validate all model properties, --*if ignore it only validates required ones
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            //4. returns bool valid

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
