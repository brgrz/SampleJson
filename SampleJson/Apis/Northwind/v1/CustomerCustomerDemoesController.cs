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
	public class CustomerCustomerDemoesController : ControllerBase
	{
		private readonly NorthwindDbContext _context;

		public CustomerCustomerDemoesController(NorthwindDbContext context)
		{
			_context = context;
		}

		// GET: api/CustomerCustomerDemoes
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CustomerCustomerDemo>>> GetCustomerCustomerDemos()
		{
			return await _context.CustomerCustomerDemos.ToListAsync();
		}

		// GET: api/CustomerCustomerDemoes/5
		[HttpGet("{id}")]
		public async Task<ActionResult<CustomerCustomerDemo>> GetCustomerCustomerDemo(string id)
		{
			var customerCustomerDemo = await _context.CustomerCustomerDemos.FindAsync(id);

			if (customerCustomerDemo == null)
			{
				return NotFound();
			}

			return customerCustomerDemo;
		}

		// PUT: api/CustomerCustomerDemoes/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCustomerCustomerDemo(string id, CustomerCustomerDemo customerCustomerDemo)
		{
			if (id != customerCustomerDemo.CustomerId)
			{
				return BadRequest();
			}

			_context.Entry(customerCustomerDemo).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CustomerCustomerDemoExists(id))
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

		// POST: api/CustomerCustomerDemoes
		[HttpPost]
		public async Task<ActionResult<CustomerCustomerDemo>> PostCustomerCustomerDemo(CustomerCustomerDemo customerCustomerDemo)
		{
			_context.CustomerCustomerDemos.Add(customerCustomerDemo);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (CustomerCustomerDemoExists(customerCustomerDemo.CustomerId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetCustomerCustomerDemo", new { id = customerCustomerDemo.CustomerId }, customerCustomerDemo);
		}

		// DELETE: api/CustomerCustomerDemoes/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<CustomerCustomerDemo>> DeleteCustomerCustomerDemo(string id)
		{
			var customerCustomerDemo = await _context.CustomerCustomerDemos.FindAsync(id);
			if (customerCustomerDemo == null)
			{
				return NotFound();
			}

			_context.CustomerCustomerDemos.Remove(customerCustomerDemo);
			await _context.SaveChangesAsync();

			return customerCustomerDemo;
		}

		private bool CustomerCustomerDemoExists(string id)
		{
			return _context.CustomerCustomerDemos.Any(e => e.CustomerId == id);
		}
	}
}
