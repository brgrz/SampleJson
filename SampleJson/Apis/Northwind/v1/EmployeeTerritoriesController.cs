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
	public class EmployeeTerritoriesController : ControllerBase
	{
		private readonly NorthwindDbContext _context;

		public EmployeeTerritoriesController(NorthwindDbContext context)
		{
			_context = context;
		}

		// GET: api/EmployeeTerritories
		[HttpGet]
		public async Task<ActionResult<IEnumerable<EmployeeTerritory>>> GetEmployeeTerritories()
		{
			return await _context.EmployeeTerritories.ToListAsync();
		}

		// GET: api/EmployeeTerritories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<EmployeeTerritory>> GetEmployeeTerritory(int id)
		{
			var employeeTerritory = await _context.EmployeeTerritories.FindAsync(id);

			if (employeeTerritory == null)
			{
				return NotFound();
			}

			return employeeTerritory;
		}

		// PUT: api/EmployeeTerritories/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEmployeeTerritory(int id, EmployeeTerritory employeeTerritory)
		{
			if (id != employeeTerritory.EmployeeId)
			{
				return BadRequest();
			}

			_context.Entry(employeeTerritory).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EmployeeTerritoryExists(id))
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

		// POST: api/EmployeeTerritories
		[HttpPost]
		public async Task<ActionResult<EmployeeTerritory>> PostEmployeeTerritory(EmployeeTerritory employeeTerritory)
		{
			_context.EmployeeTerritories.Add(employeeTerritory);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (EmployeeTerritoryExists(employeeTerritory.EmployeeId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetEmployeeTerritory", new { id = employeeTerritory.EmployeeId }, employeeTerritory);
		}

		// DELETE: api/EmployeeTerritories/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<EmployeeTerritory>> DeleteEmployeeTerritory(int id)
		{
			var employeeTerritory = await _context.EmployeeTerritories.FindAsync(id);
			if (employeeTerritory == null)
			{
				return NotFound();
			}

			_context.EmployeeTerritories.Remove(employeeTerritory);
			await _context.SaveChangesAsync();

			return employeeTerritory;
		}

		private bool EmployeeTerritoryExists(int id)
		{
			return _context.EmployeeTerritories.Any(e => e.EmployeeId == id);
		}
	}
}
