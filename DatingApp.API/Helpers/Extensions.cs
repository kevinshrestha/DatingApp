using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    // Static means that we don't need to creat a new instance of extension when we want to use one of its methods
    public static class Extensions
    {
        // the extension method will add or say public static voids because we don't want anything returned
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            // In the event of an exception, when we send this back to our client
            // There's going to be a new header in there called Application error
            // It's going to have the error message as its value and then the following two methods below
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            // * is wildcards to effectively say allow any origin
            response.Headers.Add("Access-Control-Allow-Origin" , "*");
        }

    }
}