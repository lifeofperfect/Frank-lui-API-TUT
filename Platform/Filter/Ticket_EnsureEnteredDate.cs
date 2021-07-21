using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Platform.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Filter
{
    public class Ticket_EnsureEnteredDate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ticket = context.ActionArguments["ticket"] as Ticket;

            if(ticket != null && ticket.EnteredDate.HasValue == false && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                context.ModelState.AddModelError("EnteredDate", "Entered date is required");

                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
