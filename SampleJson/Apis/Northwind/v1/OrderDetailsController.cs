﻿using System;
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
	public class OrderDetailsController : ControllerBase
	{
		private readonly NorthwindDbContext _context;

		public OrderDetailsController(NorthwindDbContext context)
		{
			_context = context;
		}

		// GET: api/OrderDetails
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
		{
			return await _context.OrderDetails.ToListAsync();
		}

		// GET: api/OrderDetails/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
		{
			var orderDetail = await _context.OrderDetails.FindAsync(id);

			if (orderDetail == null)
			{
				return NotFound();
			}

			return orderDetail;
		}

		// PUT: api/OrderDetails/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
		{
			if (id != orderDetail.OrderId)
			{
				return BadRequest();
			}

			_context.Entry(orderDetail).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderDetailExists(id))
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

		// POST: api/OrderDetails
		[HttpPost]
		public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
		{
			_context.OrderDetails.Add(orderDetail);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (OrderDetailExists(orderDetail.OrderId))
				{
					return Conflict();
				}
				else
				{
					throw;
				}
			}

			return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderId }, orderDetail);
		}

		// DELETE: api/OrderDetails/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<OrderDetail>> DeleteOrderDetail(int id)
		{
			var orderDetail = await _context.OrderDetails.FindAsync(id);
			if (orderDetail == null)
			{
				return NotFound();
			}

			_context.OrderDetails.Remove(orderDetail);
			await _context.SaveChangesAsync();

			return orderDetail;
		}

		private bool OrderDetailExists(int id)
		{
			return _context.OrderDetails.Any(e => e.OrderId == id);
		}
	}
}