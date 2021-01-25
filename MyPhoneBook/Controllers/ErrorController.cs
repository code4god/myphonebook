using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhoneBook.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error-local-development", Name ="errorLocalDevelopment")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            Log.Error(context.Error, @context.Error.StackTrace);

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message); 
        }

        [HttpGet]
        [Route("/error", Name = "errorProduction")]
        public IActionResult Error()
        {

            var error = Problem();
            Log.Error(@error.Value.ToString());

            return error;
        }
    }
}
