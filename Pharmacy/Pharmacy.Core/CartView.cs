using Pharmacy.Core;

namespace Pharmacy.Core
{
    public class CartView
    {
        public List<ShopCartItem> shopCartItems { get; set; }
        public float GrandTotal { get; set; }
    }
}
