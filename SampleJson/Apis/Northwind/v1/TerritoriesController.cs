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
	public class TerritoriesController : ControllerBase
	{
		private readonly NorthwindDbContext _context;

		public TerritoriesController(NorthwindDbContext context)
		{
			_context = context;
		}

		// GET: api/Territories
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Territory>>> GetTerritories()
		{
			return await _context.Territories.ToListAsync();
		}

		// GET: api/Territories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Territory>> GetTerritory(string id)
		{
			var territory = await _context.Territories.FindAsync(id);

			if (territory == null)
			{
				return NotFound();
			}

			return territory;
		}

		// PUT: api/Territories/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTerritory(string id, Territory territory)
		{
			if (id != territory.TerritoryId)
			{
				return BadRequest();
			}

			_context.Entry(territory).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TerritoryExists(id))
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

		// POST: api/Territories
		[HttpPost]
		public async Task<ActionResult<Territory>> PostTerritory(Territory territory)
		{
			_context.Territories.Add(territory);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (TerritoryExists(territory.TerritoryId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetTerritory", new { id = territory.TerritoryId }, territory);
		}

		// DELETE: api/Territories/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Territory>> DeleteTerritory(string id)
		{
			var territory = await _context.Territories.FindAsync(id);
			if (territory == null)
			{
				return NotFound();
			}

			_context.Territories.Remove(territory);
			await _context.SaveChangesAsync();

			return territory;
		}

		private bool TerritoryExists(string id)
		{
			return _context.Territories.Any(e => e.TerritoryId == id);
		}
	}
}
