using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Scripting.Hosting;
using ODLAPI.Models;

namespace ODLAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggersController : ControllerBase
    {
        private readonly TriggerDBContext _context;

        public TriggersController(TriggerDBContext context)
        {
            _context = context;
        }

        // GET: api/Triggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trigger>>> GetTriggers()
        {
            return await _context.Triggers.ToListAsync();
        }

        // GET: api/Triggers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trigger>> GetTrigger(Guid id)
        {
            var trigger = await _context.Triggers.FindAsync(id);

            if (trigger == null)
            {
                return NotFound();
            }

            return trigger;
        }

        // PUT: api/Triggers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrigger(Guid id, Trigger trigger)
        {
            if (id != trigger.Id)
            {
                return BadRequest();
            }

            _context.Entry(trigger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriggerExists(id))
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

        // POST: api/Triggers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trigger>> PostTrigger(Trigger trigger)
        {
            _context.Triggers.Add(trigger);
            await _context.SaveChangesAsync();

            // then run a python script with that triggers as an argument
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            var script = "C:\\Users\\someg\\OneDrive\\Documents\\UCL\\ODLAPI\\ODLAPI\\main.py";
            engine.ExecuteFile(script, scope);
            
            return CreatedAtAction("GetTrigger", new { id = trigger.Id }, trigger);
        }

        // DELETE: api/Triggers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrigger(Guid id)
        {
            var trigger = await _context.Triggers.FindAsync(id);
            if (trigger == null)
            {
                return NotFound();
            }

            _context.Triggers.Remove(trigger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TriggerExists(Guid id)
        {
            return _context.Triggers.Any(e => e.Id == id);
        }
    }
}
