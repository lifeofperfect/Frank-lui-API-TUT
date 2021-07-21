using Microsoft.AspNetCore.Mvc;
using Platform.ModelValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.models
{
    public class Ticket
    {
        //[FromQuery(Name ="tid")]
        public int TicketId { get; set; }
        //[FromRoute(Name ="pid")]
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        [Ticket_EnsureDueDateForTicketOwner]
        [Ticket_EnsureDateInFuture]
        public DateTime? DueDate { get; set; }
        public DateTime? EnteredDate { get; set; }
    }
}
