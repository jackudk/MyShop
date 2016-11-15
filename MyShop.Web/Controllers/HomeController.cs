using AutoMapper;
using MyShop.Service;
using MyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ISlideService _slideService;

        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ISlideService slideService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._slideService = slideService;
        }

        public ActionResult Index()
        {
            var latestProducts = _productService.GetMulti(x => x.Status == true)
                .OrderByDescending(x => x.CreatedDate).Take(3);
            var hotestProducts = _productService.GetMulti(x => x.Status == true && x.HotFlag==true)
                .OrderByDescending(x => x.CreatedDate).Take(3);
            var homeViewModel = new HomeViewModel();
            homeViewModel.HotestProducts = Mapper.Map<IEnumerable<ProductViewModel>>(hotestProducts);
            homeViewModel.LatestProducts = Mapper.Map<IEnumerable<ProductViewModel>>(latestProducts);
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var productCategories = _productCategoryService.GetMulti(x => x.Status == true && x.HomeFlag == true)
                .OrderBy(x => x.DisplayOrder);

            var viewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategories);
            return PartialView(viewModel);
        }

        [ChildActionOnly]
        public ActionResult Slide()
        {
            var slides = _slideService.GetMulti(x => x.Status == true).OrderBy(x => x.DisplayOrder);

            var viewModel = Mapper.Map<IEnumerable<SlideViewModel>>(slides);
            return PartialView(viewModel);
        }
    }
}