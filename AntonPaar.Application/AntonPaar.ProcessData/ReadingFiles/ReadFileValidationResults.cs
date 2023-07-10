
using AntonPaar.ProcessData.CommonInterfaces;

namespace AntonPaar.ProcessData.ReadingFiles
{
    /// <summary>
    /// This validations are only meant for Files.
    /// For other validation we need to inherit IValidationResults and create another class.
    /// This class should not get exposed to "Web" projects. Because it is only meant for "Proces Data project"
    /// </summary>
    internal class ReadFileValidationResults : IValidationResults
    {
        public bool IsValid { get; set; }
        public string? ValidationErrorMessage { get; set; }
    }
}
