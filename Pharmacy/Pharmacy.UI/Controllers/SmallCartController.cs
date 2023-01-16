using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core;
using Pharmacy.Repos;
using System.Data;

namespace Pharmacy.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class SmallCartController : Controller
    {

        //private readonly ShopCartRepository _shopcartRepository;
        private readonly UsersRepository _usersRepository;
        private readonly MedicamentsRepository _medicamentsRepository;

        public SmallCartController(MedicamentsRepository medicamentsRepository, UsersRepository usersRepository)
        {
            _medicamentsRepository = medicamentsRepository;
            //_shopcartRepository = shoppingCartRepository;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {             
            List<ShopCartItem> cart = HttpContext.Session.GetJson<List<ShopCartItem>>("Cart");
            SmallCart smallCart;

            if(cart == null || cart.Count == 0)
            {
                smallCart = null;
            }
            else
            {
                smallCart = new()
                {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }

            return View(smallCart);
        }

        /*[HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddMedicament(int idMed, User user)
        {
            var med = await _medicamentsRepository.GetMedicament(idMed);
            //var user = await _usersRepository.GetUserAsync(id);
            var cart = await _shopcartRepository.GetUserShoppingCart(user.Id);
            ViewData["count"] = cart.Medicaments.Count();
            await _shopcartRepository.AddMedToCart(cart, med);
            return View("Index", cart);
        }*/
    }
}
