using AutoMapper;
using MyShop.Model.Models;
using MyShop.Service;
using MyShop.Web.Infastructure.Core;
using MyShop.Web.Infastructure.Extensions;
using MyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MyShop.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IErrorService erroservice, IProductService productService)
            : base(erroservice)
        {
            this._productService = productService;
        }

        [Route("getallpaging")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRows;
                var productsPaging = _productService.GetAllPaging(keyWord, page, pageSize, out totalRows);

                var responseModel = Mapper.Map<IEnumerable<ProductViewModel>>(productsPaging);

                var pagingnationSet = new PaginationSet<ProductViewModel>()
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
                var model = _productService.GetAll();
                var responseModel = Mapper.Map<IEnumerable<ProductViewModel>>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseModel);
            });
        }

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetByID(id);
                var responseModel = Mapper.Map<ProductViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseModel);
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductViewModel productVM)
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
                    Product newProduct = new Product();
                    newProduct.CloneProduct(productVM);
                    newProduct.CreatedDate = DateTime.Now;
                    newProduct.CreatedBy = User.Identity.Name;

                    var model = _productService.Add(newProduct);
                    _productService.SaveChanges();

                    var responseModel = Mapper.Map<ProductViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseModel);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ProductViewModel productVM)
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
                    Product dbProduct = _productService.GetByID(productVM.ID);
                    dbProduct.CloneProduct(productVM);
                    dbProduct.UpdatedDate = DateTime.Now;
                    dbProduct.UpdatedBy = User.Identity.Name;

                    _productService.Update(dbProduct);
                    _productService.SaveChanges();

                    var responseModel = Mapper.Map<ProductViewModel>(dbProduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseModel);
                }
                return response;
            });
        }

        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                Product dbProduct = _productService.Delete(id);
                _productService.SaveChanges();

                var responseModel = Mapper.Map<ProductViewModel>(dbProduct);
                return request.CreateResponse(HttpStatusCode.OK, responseModel);
            });
        }

        [Route("deletemulti")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, () =>
            {
                var listID = new JavaScriptSerializer().Deserialize<List<int>>(ids);
                foreach (var id in listID)
                {
                    _productService.Delete(id);
                }
                _productService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.OK, listID.Count);
            });
        }
    }
}