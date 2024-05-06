using Moq;
using Moq.EntityFrameworkCore;
using Store.Domain.Interfaces;
using Store.Domain.Model;
using Store.WebUI.Controllers;
using Store.WebUI.ViewModels;

namespace Store.Tests
{
    [TestClass]
    public class HomeTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductsDbContext> mock = new Mock<IProductsDbContext>();
            mock.CallBase = true;
            mock.SetupGet(x => x.Products).ReturnsDbSet(CreateTestProducts(68));
            HomeController controller = new HomeController(mock.Object);
            controller.pageSize = 33;

            MainPageViewModel result = (MainPageViewModel)controller.ListOfProducts(null, 3).Model;

            List<Product> products = result.Products.ToList();
            Assert.IsTrue(products.Count == 2);
            Assert.AreEqual(products[0].Name, "Продукт67");
            Assert.AreEqual(products[1].Name, "Продукт68");
        }
        [TestMethod]
        public void Can_Filter_Products()
        {
            Mock<IProductsDbContext> mock = new Mock<IProductsDbContext>();
            mock.CallBase = true;
            mock.SetupGet(x => x.Products).ReturnsDbSet(CreateTestProducts(10));
            var controller = new HomeController(mock.Object);
            controller.pageSize = 3;

            MainPageViewModel result = (MainPageViewModel)controller.ListOfProducts("Категория1").Model;

            List<Product> products = result.Products.ToList();
            Assert.IsTrue(products.Count == 3);
            Assert.AreEqual(products[0].Category, "Категория1");
            Assert.AreEqual(products[2].Name, "Продукт7");
        }
        [TestMethod]
        public void Can_Specific_Product_Count()
        {
            Mock<IProductsDbContext> mock = new Mock<IProductsDbContext>();
            mock.CallBase = true;
            mock.SetupGet(x => x.Products).ReturnsDbSet(CreateTestProducts(13));
            var controller = new HomeController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((MainPageViewModel)controller.ListOfProducts("Категория1").Model).PageInfo.TotalItems;
            int res2 = ((MainPageViewModel)controller.ListOfProducts("Категория2").Model).PageInfo.TotalItems;
            int res3 = ((MainPageViewModel)controller.ListOfProducts("Категория3").Model).PageInfo.TotalItems;
            int resAll = ((MainPageViewModel)controller.ListOfProducts(null).Model).PageInfo.TotalItems;

            Assert.AreEqual(res1, 5);
            Assert.AreEqual(res2, 4);
            Assert.AreEqual(res3, 4);
            Assert.AreEqual(resAll, 13);
        }
        private static List<Product> CreateTestProducts(int quantity)
        {
            var products = new List<Product>();
            int category = 1;

            for (int i = 1; i <= quantity; i++)
            {
                var product = new Product
                {
                    Id = i,
                    Name = "Продукт" + i.ToString(),
                    Price = i,
                    Quantity = 1,
                    Category = "Категория" + category.ToString(),
                    Inactive = false
                };
                products.Add(product);
                category++;
                if (category > 3) category = 1;
            }
            return products;
        }
    }
}