using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalForBiddingDB.Data;
using PortalForBiddingDB.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PortalForBidding.MySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumersController : ControllerBase
    {
        private readonly PortalForBiddingContext _context;

        public ConsumersController(PortalForBiddingContext context)
        {
            _context = context;
        }

        // GET: api/Consumers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consumer>>> GetConsumers()
        {
          if (_context.Consumers == null)
          {
              return NotFound();
          }
            return await _context.Consumers.ToListAsync();
        }

        // GET: api/Consumers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consumer>> GetConsumer(int id)
        {
          if (_context.Consumers == null)
          {
              return NotFound();
          }
            var consumer = await _context.Consumers.FindAsync(id);

            if (consumer == null)
            {
                return NotFound();
            }

            return consumer;
        }

        // PUT: api/Consumers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumer(int id, Consumer consumer)
        {
            if (id != consumer.Id)
            {
                return BadRequest();
            }

            _context.Entry(consumer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Consumers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consumer>> PostConsumer(Consumer consumer)
        {
          if (_context.Consumers == null)
          {
              return Problem("Entity set 'PortalForBiddingContext.Consumers'  is null.");
          }
            _context.Consumers.Add(consumer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsumer", new { id = consumer.Id }, consumer);
        }

        // DELETE: api/Consumers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumer(int id)
        {
            if (_context.Consumers == null)
            {
                return NotFound();
            }
            var consumer = await _context.Consumers.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }

            _context.Consumers.Remove(consumer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsumerExists(int id)
        {
            return (_context.Consumers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // combine farmer and consumer
        //[HttpGet("GetAllDetails")]
        //public async Task<Consumer?> GetAllDetails(int id)
        //{
        //    return await _context.Consumers
        //        .Include(c => c.Farmer)
        //        .Where(c => c.FarmerId == id).FirstOrDefaultAsync();
        //}

        [HttpGet("join")]
        public ActionResult<IEnumerable<object>> GetJoinedData()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32 // Set the maximum depth to avoid exceeding the limit
            };

            var query = from consumer in _context.Consumers
                        join farmer in _context.Farmers on consumer.FarmerId equals farmer.Id
                        select new { 
                            farmer.FarmerName,
                            farmer.FarmerMailId,
                            farmer.ProductName,
                            farmer.ProductCategory,
                            farmer.ProductQuantity,
                            farmer.ProductBasePrice,
                            consumer.ConsumerName,
                            consumer.ConsumerMailId,
                            consumer.BiddedPrice,
                            consumer.Status
                        };

            var results = query.ToList();

            return Ok(results);
        }

        // get consumer's profile
        [HttpGet("getConsumerProfile")]
        public ActionResult<IEnumerable<object>> GetConsumerProfile()
        {
            var query = _context.Consumers.GroupBy(e => e.ConsumerName).Select(
                g => new
                {
                    Consumer_Name=g.Key,
                    Consumer_MailId=g.First().ConsumerMailId,
                    No_of_Bidded_products=g.Count()
                }).ToList();
            return Ok(query);
        }
    }
}

