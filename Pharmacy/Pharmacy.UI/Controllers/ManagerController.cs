using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core;
using Pharmacy.Repos;

namespace Pharmacy.UI.Controllers
{
    public class ManagerController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly Service service;

        public ManagerController(OrderRepository orderRepository, Service service)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View("Orders", await _orderRepository.GetAllOrder());
        }

        public IActionResult SendEmailDefault()
        {
            service.SendEmailDefault();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string customerName)
        {
            if(customerName == null)
                return View("Orders", await _orderRepository.GetAllOrder());
            else
            return View("Orders", await _orderRepository.GetSearchAllOrder(customerName));
        }

            // EDIT

            [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var statuslist = new List<string>
            {
                "NEW",
                "підтверджено",
                "відправлено",
            };
            var order = await _orderRepository.GetOrder(id);
            ViewBag.Status = statuslist;
            ViewData["selectstatus"] = order.Status.ToString();
            return View(await _orderRepository.GetOrder(id));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(string modelId, string status, bool ispaid)
        {
                await _orderRepository.UpdateAsync(modelId, status, ispaid);
                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            var order = await _orderRepository.GetOrder(id);
            var detid = order.details;
            ViewBag.Order = order;
            var items = await _orderRepository.GetOrderItems(detid.Id);
            ViewBag.Items = items;
            return View(detid);
        }

        public async Task<IActionResult> SuccessfulOrder(Order order)
        {

            var details = order.details;
            ViewBag.Order = order;
            var items = await _orderRepository.GetOrderItems(details.Id);
            ViewBag.Items = items;
            return View("SuccessfulOrder",details.Id);
        }
    }
}
