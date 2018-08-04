using DDDExample.Cqrs.Command.OrderItem;
using DDDExample.Cqrs.Query.OrderItem.Queries;
using DDDExample.SharedKernel.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IProcessor _processor;

        public OrderItemController(IProcessor processor)
        {
            _processor = processor;
        }

        public IActionResult Index()
        {
            return View(_processor.Process(new GetOrderItensIndex()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateOrderItem orderItem)
        {
            _processor.Send(orderItem);
            return RedirectToAction("Index");
        }
    }
}