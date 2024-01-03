using FluentValidation.Results;
using ServiceLayer.Base;
using ToolsLayer.ErrorModel;

namespace ServiceLayer
{
    public class ServiceErrorContainer : IServiceErrorContainer
    {
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
        public bool IsSuccess { get { return Errors.Count == 0; } }
        public T? AddServiceResponse<T>(Func<Task<T>> func)
        {
            if (this.IsSuccess)
            {
                var task = func.Invoke();
                task.Wait();
                return task.Result;
            }
            return default(T);
        }
        public void AddServiceResponse(Func<Task> func)
        {
            if (this.IsSuccess)
            {
                var task = func.Invoke();
                task.Wait();
            }
        }
        public T? AddServiceResponse<T>(Func<T> func)
        {
            if (this.IsSuccess)
            {
                return func.Invoke();
            }
            return default(T);
        }
        public void BindError(Dictionary<string,string> errors)
        {
            foreach (var item in errors)
            {
                this.Errors.TryAdd(item.Key,item.Value);   
            }
        }
        public void BindValidation(ValidationResult validation)
        {
            BindError(validation.ToErrorModel());
        }
    }
}
