using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly BugsContext _db;
        public TicketController(BugsContext db)
        {
            _db = db;
        }
        //[HttpGet]
        //[Route("api/tickets")]
        //public IActionResult Get()
        //{
        //    return Ok("Reading all the tickets");
        //}

        [HttpGet]
        [Route("api/tickets/{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _db.Projects.Find(id);
            return Ok($"Reading tickets {ticket}");
        }

        [HttpGet]
        [Route("GetTickets")]
        public IActionResult GetTickets()
        {
            return Ok(_db.Projects.ToList());
        }

        [HttpGet]
        [Route("GetTickets/{id}")]
        public IActionResult GetTickets(int pid)
        {
            var tickets = _db.Tickets.Where(t => t.ProjectId == pid).ToList();
            if (tickets == null || tickets.Count <= 0)
                return NotFound();

            return Ok(tickets);
        }

        [HttpGet]
        [Route("GetTicket/{id}")]
        public IActionResult GetTicket(int id)
        {
            var project = _db.Projects.Find(id);

            if (project == null)
                return NotFound();

            return Ok(project);
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
        public IActionResult GetProjectTicket([FromQuery] Ticket ticket)
        {
            if (ticket == null) return BadRequest("Parameters are not provided properly");

            if (ticket.TicketId == 0)
                return Ok($"Reading all the tickets belong to project #{ticket.ProjectId}");
            else
                return Ok($"Reading project #{ticket.ProjectId}, ticket #{ticket.TicketId}, title: {ticket.Title} and Description: {ticket.Description}");
        }

        [HttpPost]
        //[Route("/api/post/ticket")]
        public IActionResult Post([FromBody]Project project)
        {
            _db.Projects.Add(project);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = project.ProjectId }, project);
        }

        [HttpPost]
        [Route("/api/post/ticket")]
        public IActionResult PostTicket(Ticket ticket)
        {
            return Ok(ticket);
        }

        //[HttpPost]
        //[Route("/api/v2/ticket")]
        //[Ticket_EnsureEnteredDate]
        //public IActionResult PostV2(Ticket ticket)
        //{
        //    return Ok(ticket);
        //}

        [HttpPut("{id}")]
        public IActionResult Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();
            _db.Entry(project).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {
                if (_db.Projects.Find(id) == null)
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _db.Projects.Find(id);
            if (project == null) return NotFound();

            _db.Projects.Remove(project);
            _db.SaveChanges();

            return Ok(project);
        }

    }
}
