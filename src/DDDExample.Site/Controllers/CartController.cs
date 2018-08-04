using System;
using DDDExample.Cqrs.Command.Cart;
using DDDExample.Cqrs.Query.OrderItem.Queries;
using DDDExample.SharedKernel.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly IProcessor _processor;

        public CartController(IProcessor processor)
        {
            _processor = processor;
        }

        public IActionResult Cart()
        {
            return View(_processor.Process(new GetCart()));
        }

        public IActionResult AddItemCart(Guid idOrderItem)
        {
            _processor.Send(new AddItemCart(idOrderItem, 1));
            return RedirectToAction("Cart");
        }
        public IActionResult LessItemCart(Guid idOrderItem)
        {
            _processor.Send(new AddItemCart(idOrderItem, -1));
            return RedirectToAction("Cart");
        }
    }
}