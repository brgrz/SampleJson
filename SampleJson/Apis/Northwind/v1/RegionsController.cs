using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleJson.DataSources.Northwind.Models;

namespace SampleJson.Apis.Northwind.v1
{
	[Route("api/northwind/v1/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NorthwindDbContext _context;

		public RegionsController(NorthwindDbContext context)
		{
			_context = context;
		}

		// GET: api/Regions
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
		{
			return await _context.Regions.ToListAsync();
		}

		// GET: api/Regions/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Region>> GetRegion(int id)
		{
			var region = await _context.Regions.FindAsync(id);

			if (region == null)
			{
				return NotFound();
			}

			return region;
		}

		// PUT: api/Regions/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutRegion(int id, Region region)
		{
			if (id != region.RegionId)
			{
				return BadRequest();
			}

			_context.Entry(region).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!RegionExists(id))
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

		// POST: api/Regions
		[HttpPost]
		public async Task<ActionResult<Region>> PostRegion(Region region)
		{
			_context.Regions.Add(region);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (RegionExists(region.RegionId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetRegion", new { id = region.RegionId }, region);
		}

		// DELETE: api/Regions/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Region>> DeleteRegion(int id)
		{
			var region = await _context.Regions.FindAsync(id);
			if (region == null)
			{
				return NotFound();
			}

			_context.Regions.Remove(region);
			await _context.SaveChangesAsync();

			return region;
		}

		private bool RegionExists(int id)
		{
			return _context.Regions.Any(e => e.RegionId == id);
		}
	}
}
