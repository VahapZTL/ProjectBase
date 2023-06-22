using FluentValidation.Results;

namespace Core.Extensions
{
    public class ValidationErrorDetail : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
