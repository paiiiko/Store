using Store.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Store.WebUI.ViewModels;
using Store.Domain.Interfaces;

namespace Store.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsDbContext db;
        public int pageSize = 12;
        public HomeController(IProductsDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public ViewResult ListOfProducts(string category,
                                         int page = 1,
                                         SortState sortOrder = SortState.Popular)
        {
            List<Product> productPerPages;
            if (category == null)
            {
                productPerPages = db.GetAll();
            }
            else
            {
                productPerPages = db.GetByCategory(category);
            }
            ViewBag.Category = category;
            ViewBag.NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewBag.PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = productPerPages.Count
            };
            productPerPages = sortOrder switch
            {
                SortState.Popular => productPerPages,// потом нужно добавить сюда фильтрацию
                                                     // по популярным товарам
                SortState.NameAsc => productPerPages.OrderBy(product => product.Name)
                                                      .ToList(),
                SortState.NameDesc => productPerPages.OrderByDescending(product => product.Name)
                                                      .ToList(),
                SortState.PriceAsc => productPerPages.OrderBy(product => product.Price)
                                                      .ToList(),
                SortState.PriceDesc => productPerPages.OrderByDescending(product => product.Price)
                                                      .ToList(),
            };
            productPerPages = productPerPages.Skip((page - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToList();
            MainPageViewModel pageModel = new MainPageViewModel(category, productPerPages, pageInfo);
            return View(pageModel);
        }
    }
}