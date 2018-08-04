using System;
using AutoMapper;
using DDDExample.Cqrs.Command.Customer;
using DDDExample.Cqrs.Query.Customer.Models;
using DDDExample.Cqrs.Query.Customer.Queries;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.SharedKernel.Cqrs;
using DDDExample.SharedKernel.Paging;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly IProcessor _processor;
        private readonly IEventRepository _eventRepository;

        public CustomerController(
            IDomainNotification notifications,
            IProcessor processor, IEventRepository eventRepository) : base(notifications)
        {
            _processor = processor;
            _eventRepository = eventRepository;
        }

        public IActionResult Index(Paging<CustomerIndex> paging, string search = null)
        {
            //paging.Top = 5;
            return View(_processor.Process(new GetCustomersIndex(paging, search)));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCustomer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _processor.Send(customer);
            if (IsValidOperation())
                ViewBag.Sucesso = "Customer Registered!";

            return View(customer);
        }
        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _processor.Process(new GetCustomerDetails(id.Value));

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _processor.Process(new GetCustomerDetails(id.Value));

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerDetails customer)
        {
            if (!ModelState.IsValid) return View(customer);

            _processor.Send(Mapper.Map<UpdateCustomer>(customer));

            if (IsValidOperation())
                ViewBag.Sucesso = "Customer Updated!";

            return View(customer);
        }

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _processor.Process(new GetCustomerDetails(id.Value));

            return customer == null ? (IActionResult)NotFound() : View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _processor.Send(new DeleteCustomer(id));

            if (!IsValidOperation())
                return View(_processor.Process(new GetCustomerDetails(id)));

            ViewBag.Sucesso = "Customer Removed!";
            return RedirectToAction("Index");
        }

        public IActionResult History()
        {
            return View(_eventRepository.GetAllHistories());
        }
    }
}