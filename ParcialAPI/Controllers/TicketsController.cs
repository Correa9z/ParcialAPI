using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialAPI.DAL;
using ParcialAPI.DAL.Entities;

namespace ParcialAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class TicketsController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
            
        }

        [HttpPost, ActionName("Post")]
        [Route("Post")]
        public async Task<ActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                ticket.id = Guid.NewGuid();
                

                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    return Conflict("ya existe");
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }

            return Ok(ticket);
        }

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult> GetTicketById(Guid id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(j => j.id == id);

            if (ticket != null)
            {
                

                return Ok(ticket);

                

            }

            return Ok();

        }





        [HttpPut, ActionName("Put")]
        [Route("Put/{id}")]
        public async Task<ActionResult<Ticket>> Verificar(Guid? id, Ticket Ticket)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(j => j.id == id);

            if (ticket != null)
            {
                if (ticket.isUsed == false)
                {
                    try
                    {
                        ticket.useDate = DateTime.Now;
                        ticket.isUsed = true;
                        ticket.entranceGate = Ticket.entranceGate;

                        _context.Tickets.Update(ticket);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                        if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                            return Conflict("ya existe");
                    }
                    catch (Exception e)
                    {
                        return Conflict(e.Message);
                    }

                    return Ok(ticket);

                }
                return Conflict("La entrada ya se utilizo");


            }

            return NotFound();

        }

      


    }
}
