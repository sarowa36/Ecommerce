using FluentValidation.Results;

namespace ServiceLayer.Base
{
    public interface IServiceErrorContainer
    {
        bool IsSuccess { get; }
        Dictionary<string, string> Errors { get; set; }
        T? AddServiceResponse<T>(Func<Task<T>> func);
        void AddServiceResponse(Func<Task> func);
        T AddServiceResponse<T>(Func<T> func);
        void BindError(Dictionary<string, string> errors);
        void BindValidation(ValidationResult validation);
    }
}