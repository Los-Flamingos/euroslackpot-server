using System;
using System.Text;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public static class ResultExtensions
    {
        public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, Result<T> result)
        {
            return controller.ToActionResult((IResult)result);
        }

        internal static ActionResult ToActionResult(this ControllerBase controller, IResult result)
        {
            return result.Status switch
            {
                ResultStatus.Ok           => controller.Ok(result.GetValue()),
                ResultStatus.NotFound     => controller.NotFound(),
                ResultStatus.Unauthorized => controller.Unauthorized(),
                ResultStatus.Forbidden    => controller.Forbid(),
                ResultStatus.Invalid      => BadRequest(controller, result),
                ResultStatus.Error        => UnprocessableEntity(controller, result),
                _                         => throw new NotSupportedException($"Result {result.Status} conversion is not supported.")
            };
        }

        private static ActionResult BadRequest(ControllerBase controller, IResult result)
        {
            foreach (var error in result.ValidationErrors)
            {
                // TODO: Fix after updating to 3.0.0
                controller.ModelState.AddModelError(error.Identifier, error.ErrorMessage);
            }

            return controller.BadRequest(controller.ModelState);
        }

        private static ActionResult UnprocessableEntity(ControllerBase controller, IResult result)
        {
            var details = new StringBuilder("Next error(s) occured:");

            foreach (var error in result.Errors) details.Append("* ").Append(error).AppendLine();

            return controller.UnprocessableEntity(new ProblemDetails
            {
                Title = "Something went wrong.",
                Detail = details.ToString(),
            });
        }
    }
}
