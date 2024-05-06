using Store.Domain.Model;

namespace Store.WebUI.ViewModels
{
    public class MainPageViewModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public string CurrentCategory { get; set; }

        public MainPageViewModel(string currentCategory, List<Product> products, PageInfo pageInfo)
        {
            Products = products;
            PageInfo = pageInfo;
            CurrentCategory = currentCategory;
        }
    }
}
