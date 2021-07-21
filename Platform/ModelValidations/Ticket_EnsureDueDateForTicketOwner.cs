using Platform.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.ModelValidations
{
    public class Ticket_EnsureDueDateForTicketOwner : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if(ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if (!ticket.DueDate.HasValue)
                {
                    return new ValidationResult("Due date cannot be empty if there are owners");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class Ticket_EnsureDateInFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = validationContext.ObjectInstance as Ticket;

            if(ticket != null)
            {
                if(ticket.DueDate.HasValue && ticket.DueDate.Value < DateTime.Now)
                {
                    return new ValidationResult("The date must be in the future");
                }
            }

            return ValidationResult.Success;
        }
    }
}
