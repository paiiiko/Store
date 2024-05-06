using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Interfaces;

namespace Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartDbContext db;
        public CartController(ICartDbContext _db)
        {
            db = _db;
        }

        [Authorize]
        public ViewResult CartList()
        {

            return View();
        }
    }
}
