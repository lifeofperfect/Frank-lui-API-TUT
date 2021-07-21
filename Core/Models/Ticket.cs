using Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public class Ticket
    {
        //[FromQuery(Name ="tid")]
        public int? TicketId { get; set; }
        //[FromRoute(Name ="pid")]
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        
        public DateTime? DueDate { get; set; }

        [Ticket_EnsureReportDatePresent]
        public DateTime? ReportDate { get; set; }
        public Project Project { get; set; }

        ///<summary>
        ///
       /// when creating a ticket, if due date is entered, it has to be in the future
        /// 
        /// </summary>
        public bool ValidateFutureDate()
        {
            if (TicketId.HasValue) return true;
            if (!DueDate.HasValue) return true;

            return (DueDate.Value > DateTime.Now);
        }
        ///<summary>
        ///When owner is assigned, the report date has to be present
        ///</summary>
        public bool ValidateReportDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return ReportDate.HasValue;
        }

        ///<summary>
        /// When owner is assigned, the due date has to be present
        ///</summary>
        public bool ValidateDueDatePresence()
        {
            if (string.IsNullOrWhiteSpace(Owner)) return true;

            return DueDate.HasValue;
        }

        ///<summary>
        /// When due date and report date are present due date has to be later or eaual to report date
        ///</summary>
        public bool ValidateDueDateAfterReportDate()
        {
            if (!DueDate.HasValue || !ReportDate.HasValue) return true;

            return DueDate.Value.Date >= ReportDate.Value.Date;
        }
    }
}
