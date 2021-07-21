using Microsoft.AspNetCore.Mvc;
using Platform.Filter;
using Platform.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[VersionDiscontinueResourceFilter]
    public class TicketController : ControllerBase
    {
        //[HttpGet]
        //[Route("api/tickets")]
        //public IActionResult Get()
        //{
        //    return Ok("Reading all the tickets");
        //}

        //[HttpGet]
        //[Route("api/tickets/{id}")]
        //public IActionResult GetById(int id)
        //{
        //    return Ok($"Reading tickets {id}");
        //}
        [HttpGet]
        [Route("GetTickets")]
        public IActionResult GetTickets()
        {
            return Ok("Reading all the tickets");
        }

        [HttpGet]
        [Route("GetTickets/{id}")]
        public IActionResult GetTickets(int id)
        {
            return Ok($"Get all the tickets for id #{id}");
        }

        [HttpGet]
        [Route("GetTicket/{id}")]
        public IActionResult GetTicket(int id)
        {
            return Ok($"Get a single ticket for this #{id}");
        }

        /// api/projects/{pid}/tickets?tid={tid}
        /// 
        //[HttpGet]
        //[Route("/api/projects/{pid}/tickets")]
        //public IActionResult GetProjectTicket(int pId, int tId)
        //{

        //    return Ok($"Reading project #{pId}, ticket #{tId}");
        //}

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTicket([FromQuery]Ticket ticket)
        {
            if (ticket == null) return BadRequest("Parameters are not provided properly");

            if (ticket.TicketId == 0)
                return Ok($"Reading all the tickets belong to project #{ticket.ProjectId}");
            else
                return Ok($"Reading project #{ticket.ProjectId}, ticket #{ticket.TicketId}, title: {ticket.Title} and Description: {ticket.Description}");
        }

        [HttpPost]
        [Route("/api/post/ticket")]
        public IActionResult PostTicket(Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpPost]
        [Route("/api/v2/ticket")]
        [Ticket_EnsureEnteredDate]
        public IActionResult PostV2(Ticket ticket)
        {
            return Ok(ticket);
        }
    }
}
