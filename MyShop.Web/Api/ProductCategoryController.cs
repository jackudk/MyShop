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
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var lstProductCategory = _productCategoryService.GetAll();
                var model = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(lstProductCategory);
                return request.CreateResponse(HttpStatusCode.OK, model);
            });
        }
    }
}
