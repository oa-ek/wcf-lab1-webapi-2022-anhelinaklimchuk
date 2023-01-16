using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core;
using Pharmacy.Repos;
using System.Security.Cryptography;

namespace Pharmacy.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly CartView _cartRepository;
        private readonly MedicamentsRepository _medicamentsRepository;
        private readonly OrderRepository _orderRepository;
        private readonly UsersRepository _usersRepository;
        private readonly UserManager<User> userManager;

        public OrderController(MedicamentsRepository medicamentsRepository,OrderRepository orderRepository,UsersRepository usersRepository)
        {
            _medicamentsRepository = medicamentsRepository;
            _orderRepository = orderRepository;
            _usersRepository = usersRepository;
        }

        // GET: OrderController
        public IActionResult FormOrder()
        {
            List<ShopCartItem> cart = HttpContext.Session.GetJson<List<ShopCartItem>>("Cart");
            return RedirectToAction("CreateOrder","Cart",cart);
        }

        // GET: OrderController/Details/5


        // GET: OrderController/Create
        public async Task<IActionResult> Create(OrderDetails orderDetails)
        {
            List<ShopCartItem> cart = HttpContext.Session.GetJson<List<ShopCartItem>>("Cart");
             var user = await _usersRepository.GetCurrentUser();
            //ar od = _orderRepository.GetOrderDetails(orderDetails);
            Order order =  await _orderRepository.CreateOrder(cart,user, orderDetails);
            //Order order = await _orderRepository.GetOrderUser(user);
            return RedirectToActionPermanent("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(string Address, string payment, string phone, string name, string typeofdelivery)
        {
            List<ShopCartItem> cart = HttpContext.Session.GetJson<List<ShopCartItem>>("Cart");
            OrderDetails d = await _orderRepository.CreateOrderDetails();
            
         // var od = await _orderRepository.GetOrderDetails(d.Id);
            await _orderRepository.CreateOrderItems(cart, d);            
            var orderitems = await _orderRepository.GetOrderItems(d.Id);
            var total = (float)0.0;
            foreach (var i in cart)
                total = cart.Sum(x => x.Total);

            if (typeofdelivery == "Нова пошта - Доставка до відділення")
                total += 50;
            else if (typeofdelivery == "Нова пошта - Доставка за адресою")
                total += 80;
            else if (typeofdelivery == "Укрпошта - Доставка до відділення")
                total += 50;
            else if (typeofdelivery == "Укрпошта - Доставка за адресою")
                total += 55;

            await _orderRepository.AddItems(d.Id, orderitems, total);
            await _orderRepository.AddInfo(d.Id, Address, phone, name, payment, typeofdelivery);
            return RedirectToAction("Create", d);
        }

        

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
