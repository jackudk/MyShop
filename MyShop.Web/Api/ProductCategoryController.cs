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
using MyShop.Model.Models;
using MyShop.Web.Infastructure.Extensions;

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

        [Route("getallpaging")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRows;
                var productCategoriesPaging = _productCategoryService.GetAllPaging(keyWord, page, pageSize, out totalRows);

                var responseModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategoriesPaging);

                var pagingnationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseModel,
                    Page = page,
                    TotalCount = totalRows,
                    TotalPages = (int)Math.Ceiling((decimal)totalRows / pageSize)
                };

                return request.CreateResponse(HttpStatusCode.OK, pagingnationSet);
            });
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var responseModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseModel);
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.CloneProductCategory(productCategoryVM);

                    var model = _productCategoryService.Add(productCategory);
                    _productCategoryService.SaveChanges();

                    var responseModel = Mapper.Map<ProductCategoryViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseModel);
                }
                return response;
            });
        }
    }
}
