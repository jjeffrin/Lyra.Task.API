﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lyra.TaskService.API.DataAccess;
using Lyra.TaskService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Lyra.TaskService.API.DTOs;

namespace Lyra.TaskService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkItemsController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public WorkItemsController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItems()
        {
            return await _context.WorkItems.ToListAsync();
        }

        // GET: api/WorkItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(int id)
        {
            var workItem = await _context.WorkItems.FindAsync(id);

            if (workItem == null)
            {
                return NotFound();
            }

            return workItem;
        }

        [HttpGet]
        [Route("GetByUserId")]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItemByUserId(int id)
        {
            IQueryable<WorkItem> workItem = _context.WorkItems.Where(x => x.UserId.Equals(id)).OrderByDescending(x => x.UpdatedTime);

            if (workItem == null)
            {
                return NotFound();
            }

            return await workItem.ToListAsync();
        }

        // PUT: api/WorkItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkItem(int id, WorkItem workItem)
        {
            if (id != workItem.Id)
            {
                return BadRequest();
            }

            workItem.UpdatedTime = this.GetIndianTime();
            _context.Entry(workItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkItemExists(id))
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

        // POST: api/WorkItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkItem>> PostWorkItem(WorkItem workItem)
        {
            workItem.CreatedTime = workItem.UpdatedTime = this.GetIndianTime();
            _context.WorkItems.Add(workItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkItem", new { id = workItem.Id }, workItem);
        }

        // DELETE: api/WorkItems
        [HttpDelete]
        public async Task<IActionResult> DeleteWorkItem(WorkItemDeleteRequest request)
        {
            var workItem = await _context.WorkItems.FirstAsync(x => x.UserId.Equals(request.UserId) && x.Id.Equals(request.Id));
            if (workItem == null)
            {
                return NotFound();
            }

            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkItemExists(int id)
        {
            return _context.WorkItems.Any(e => e.Id == id);
        }

        private DateTime GetIndianTime()
        {
            // get correct date & time
            TimeZoneInfo indTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, indTimeZone);
        }
    }
}
