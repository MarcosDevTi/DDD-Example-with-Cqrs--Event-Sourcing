using System;
using DDDExample.Cqrs.Command.Order;
using DDDExample.Cqrs.Query.Product.Queries;
using DDDExample.Domain.Core;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.SharedKernel.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IProcessor _processor;
        private readonly IUser _user;

        public OrderController(IDomainNotification notifications, IProcessor processor, IUser user) : base(notifications)
        {
            _processor = processor;
            _user = user;
        }
        [HttpGet]
        public IActionResult Store(string message)
        {
            ViewBag.Message = message;
            return View(_processor.Process(new GetProductsIndex()));
        }

        //[HttpPost]
        public IActionResult AddCart(Guid id)
        {
            if (!_user.IsAuthenticated()) return RedirectToAction("Login", "Account");
            _processor.Send(new AddProductToCart(id, _user.UserId()));

            var product = _processor.Process(new GetProductDetails(id));
            return RedirectToAction("Store", new { message = $"{product.Name} Added in you Cart" });
        }
    }
}