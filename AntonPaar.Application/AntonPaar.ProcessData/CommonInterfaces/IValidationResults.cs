using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonPaar.ProcessData.CommonInterfaces
{
    /// <summary>
    /// This interface will only capture the validation result.
    /// Validation can be obtained from Various Component.
    /// Examples for Validation Component can be - 
    /// File Validation
    /// Data Validation
    /// Model Validation
    /// It can be any type of Validation.
    /// Yes, it is different from Logging - Because Validation errors are defined by us. It is different from any type of System Errors.
    /// Hence we are trying to keep it isolated
    /// We are only have 2 properties, because we only want to return whether Validation passed/failed with the validation error message only.
    /// </summary>
    public interface IValidationResults
    {
        public bool IsValid { get; set; }
        public string? ValidationErrorMessage { get; set; }
    }
}
