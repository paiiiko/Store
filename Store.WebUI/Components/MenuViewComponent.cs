using Microsoft.AspNetCore.Mvc;
using Store.Domain.Interfaces;

namespace Store.WebUI.Components
{
    public class MenuViewComponent : ViewComponent
    {
        IProductsDbContext db;

        public MenuViewComponent(IProductsDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            List<string> categories = db.GetCategory();
            return View(categories);
        }
    }
}
