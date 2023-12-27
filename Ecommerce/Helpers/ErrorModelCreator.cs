using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Helpers
{
    //public static class ErrorModelCreator
    //{
    //    public static Dictionary<string, string> ToErrorModel(this IdentityResult result)
    //    {
    //        string str = "";
    //        foreach (var key in result.Errors)
    //        {
    //            str += key.Description + "\n";
    //        }
    //        return new Dictionary<string, string>() { { "ModelOnly", str } }; ;
    //    }
    //    public static Dictionary<string, string> ToErrorModel(this FluentValidation.Results.ValidationResult result)
    //    {
    //        Dictionary<string, string> data = new Dictionary<string, string>();
    //        foreach (var error in result.Errors)
    //        {
    //            if (!data.TryAdd(error.PropertyName, error.ErrorMessage))
    //                data[error.PropertyName] = data[error.PropertyName] +"\n"+ error.ErrorMessage;
    //        }
    //        return data;
    //    }
    //    public static Dictionary<string, string> ToErrorModel(this SignInResult result)
    //    {
    //        string str = "";
    //        /*foreach (var key in result.Errors)
    //        {
    //            str += key.Description + "\n";
    //        }*/
    //        if (!result.Succeeded)
    //            str = "Email or Password is invalid";
    //        else if (result.IsLockedOut)
    //            str = "Your account is locked out";
    //        else if (result.RequiresTwoFactor)
    //            str = "Your account is require two factor enterance";
    //        else if (result.IsNotAllowed)
    //            str = "Your account is not allowed";
    //        return new Dictionary<string, string>() { { "ModelOnly", str } }; ;
    //    }
    //}
}
