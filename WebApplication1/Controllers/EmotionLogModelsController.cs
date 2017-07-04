using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/EmotionLogModels")]
    public class EmotionLogModelsController : Controller
    {
        private readonly EmotionContext _context;

        public EmotionLogModelsController(EmotionContext context)
        {
            _context = context;
        }

        // GET: api/EmotionLogModels
        [HttpGet]
        public IEnumerable<EmotionLogModel> GetEmotionLogs()
        {
            return _context.EmotionLogs;
        }

        // GET: api/EmotionLogModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmotionLogModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emotionLogModel = await _context.EmotionLogs.SingleOrDefaultAsync(m => m.Id == id);

            if (emotionLogModel == null)
            {
                return NotFound();
            }

            return Ok(emotionLogModel);
        }

        // PUT: api/EmotionLogModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmotionLogModel([FromRoute] int id, [FromBody] EmotionLogModel emotionLogModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emotionLogModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(emotionLogModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmotionLogModelExists(id))
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

        // POST: api/EmotionLogModels
        [HttpPost]
        public async Task<IActionResult> PostEmotionLogModel([FromBody] EmotionLogModel emotionLogModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmotionLogs.Add(emotionLogModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmotionLogModel", new { id = emotionLogModel.Id }, emotionLogModel);
        }

        // DELETE: api/EmotionLogModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmotionLogModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emotionLogModel = await _context.EmotionLogs.SingleOrDefaultAsync(m => m.Id == id);
            if (emotionLogModel == null)
            {
                return NotFound();
            }

            _context.EmotionLogs.Remove(emotionLogModel);
            await _context.SaveChangesAsync();

            return Ok(emotionLogModel);
        }

        private bool EmotionLogModelExists(int id)
        {
            return _context.EmotionLogs.Any(e => e.Id == id);
        }
    }
}