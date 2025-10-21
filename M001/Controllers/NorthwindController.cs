using M001.Models;
using Microsoft.AspNetCore.Mvc;

namespace M001.Controllers;

public class NorthwindController(NorthwindContext db) : Controller
{
	public IActionResult Index() => View();

	[Route("/Northwind/ShowAllCustomers")]
	public IActionResult FilterCustomers(string country)
	{
		List<Customers> customers = string.IsNullOrWhiteSpace(country) ? db.Customers.ToList() : db.Customers.Where(e => e.Country == country).ToList();
		return View("Customers", customers);
	}

	public IActionResult ShowOrdersForCustomer(string id)
	{
		OrdersWithCustomerId o = new OrdersWithCustomerId()
		{
			Orders = db.Orders.Where(e => e.CustomerId == id).ToList(),
			CustomerId = id
		};

		ViewData["ID"] = id;

		return View("Orders", o);
	}

	public IActionResult ShowDetailsForOrder(int id)
	{
		List<OrderDetails> details = db.OrderDetails.Where(e => e.OrderId == id).ToList();

		int[] x = details.Select(e => e.ProductId).ToArray();
		List<string> p = db.Products.Where(e => x.Contains(e.ProductId)).Select(e => e.ProductName).ToList();

		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		List<OrderDetailWithProduct> odp = db.OrderDetails.Where(e => e.OrderId == id).Join(db.Products, od => od.ProductId, p => p.ProductId, (od, p) => new OrderDetailWithProduct()
		{
			OrderId = od.OrderId,
			Quantity = od.Quantity,
			UnitPrice = od.UnitPrice,
			ProductName = p.ProductName
		}).ToList();

		return View("OrderDetails", odp);
	}
}

public class OrdersWithCustomerId
{
	public List<Orders> Orders { get; set; }

	public string CustomerId { get; set; }
}

public class OrderDetailWithProduct
{
	public int OrderId { get; set; }

	public int Quantity { get; set; }

	public decimal UnitPrice { get; set; }

	public string ProductName { get; set; }
}