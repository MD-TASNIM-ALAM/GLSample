using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using XeroConnection.API.Controllers;

namespace XeroConnection.API.Functions
{
    public class HandleControllerAction
    {
        public static async Task<JsonResult> Execute<T>(BaseController controller, AuthRequest<T> request)
        {
            try
            {

                // Get user from custom header
                if (controller.HttpContext.Request.Headers.TryGetValue("User", out var userHeader))
                {
                    // request.User = ExtractUserFromHeader.Execute(userHeader);
                }
                else
                {
                    // Get user from claims
                    // request.User = ExtractUserFromClaims.Execute(controller.User.Claims);
                }
            }
            catch (Exception e)
            {
                controller._logger.LogInformation(e.Message);
                throw new ValidationException();
            }

            var result = await controller._mediator.Send(request);

            return HandleResponse.Execute(result, controller);
        }

        public static async Task<JsonResult> Execute<T>(BaseController controller, IRequest<Result<T>> request)
        {
            var result = await controller._mediator.Send(request);

            return HandleResponse.Execute(result, controller);
        }
    }
}
