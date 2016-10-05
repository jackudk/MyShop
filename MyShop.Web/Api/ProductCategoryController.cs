using MyShop.Web.Infastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyShop.Service;
using MyShop.Web.Models;
using AutoMapper;

namespace MyShop.Web.Api
{
    [RoutePrefix("api/productCategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService erroservice, IProductCategoryService productCategoryService)
            : base(erroservice)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                //var productCategories = _productCategoryService.GetAll();
                //int totalRows = productCategories.Count();

                //var productCategoriesPaging = productCategories.OrderByDescending(x => x.CreatedDate)
                //.Skip(page * pageSize).Take(pageSize);
                int totalRows;
                var productCategoriesPaging = _productCategoryService.GetAllPaging(keyWord, page, pageSize, out totalRows);

                var model = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategoriesPaging);

                var pagingnationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = model,
                    Page = page,
                    TotalCount = totalRows,
                    TotalPages = (int)Math.Ceiling((decimal)(totalRows / pageSize))
                };

                return request.CreateResponse(HttpStatusCode.OK, pagingnationSet);
            });
        }
    }
}
