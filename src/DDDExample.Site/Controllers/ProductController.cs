using System;
using AutoMapper;
using DDDExample.Cqrs.Command.Product;
using DDDExample.Cqrs.Query.Product.Models;
using DDDExample.Cqrs.Query.Product.Queries;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.SharedKernel.Cqrs;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProcessor _processor;
        private readonly IEventRepository _eventRepository;

        public ProductController(IDomainNotification notifications, IEventRepository eventRepository, IProcessor processor) : base(notifications)
        {
            _eventRepository = eventRepository;
            _processor = processor;
        }

        public IActionResult Index()
        {
            return View(_processor.Process(new GetProductsIndex()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProduct product)
        {
            if (!ModelState.IsValid) return View(product);
            _processor.Send(product);
            if (IsValidOperation())
                ViewBag.Sucesso = "Product Registered!";

            return View(product);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _processor.Process(new GetProductDetails(id.Value));

            return product == null ? (IActionResult)NotFound() : View(product);
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _processor.Process(new GetProductDetails(id));

            return product == null ? (IActionResult)NotFound() : View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductDetails product)
        {
            if (!ModelState.IsValid) return View(product);

            _processor.Send(Mapper.Map<UpdateProduct>(product));

            if (IsValidOperation()) ViewBag.Sucesso = "Product Updated";

            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _processor.Process(new GetProductDetails(id));

            return product == null ? (IActionResult)NotFound() : View(product);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _processor.Send(new DeleteProduct(id));
            if (!IsValidOperation())
            {
                return View(_processor.Process(new GetProductDetails(id)));
            }
            return RedirectToAction("Index");
        }
    }
}