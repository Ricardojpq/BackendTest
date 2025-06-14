using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class ValidationFailedResult : BadRequestObjectResult
{
    public ValidationFailedResult(ModelStateDictionary modelState)
        : base(FormatErrors(modelState))
    {
        StatusCode = StatusCodes.Status400BadRequest;
    }

    private static object FormatErrors(ModelStateDictionary modelState)
    {
        var errors = modelState.Keys
            .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
            .ToList();

        return new
        {
            Message = "Validation Failed",
            Errors = errors
        };
    }
}

public class ValidationError
{
    public string Field { get; }
    public string Message { get; }

    public ValidationError(string field, string message)
    {
        Field = field != string.Empty ? field : null; 
        Message = message;
    }
}