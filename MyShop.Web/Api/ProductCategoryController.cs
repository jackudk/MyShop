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
using System.Web.Script.Serialization;

namespace MyShop.Web.Api
{
    [RoutePrefix("api/productCategory")]
    [Authorize]
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

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetByID(id);
                var responseModel = Mapper.Map<ProductCategoryViewModel>(model);
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
                    ProductCategory newProductCategory = new ProductCategory();
                    newProductCategory.CloneProductCategory(productCategoryVM);
                    newProductCategory.CreatedDate = DateTime.Now;
                    //TODO: CreatedBy???

                    var model = _productCategoryService.Add(newProductCategory);
                    _productCategoryService.SaveChanges();

                    var responseModel = Mapper.Map<ProductCategoryViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseModel);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
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
                    ProductCategory dbProductCategory = _productCategoryService.GetByID(productCategoryVM.ID);
                    dbProductCategory.CloneProductCategory(productCategoryVM);
                    dbProductCategory.UpdatedDate = DateTime.Now;
                    //TODO: UpdateBy??

                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.SaveChanges();

                    var responseModel = Mapper.Map<ProductCategoryViewModel>(dbProductCategory);
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
                ProductCategory dbProductCategory = _productCategoryService.Delete(id);
                _productCategoryService.SaveChanges();

                var responseModel = Mapper.Map<ProductCategoryViewModel>(dbProductCategory);
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
                    _productCategoryService.Delete(id);
                }
                _productCategoryService.SaveChanges();
                
                return request.CreateResponse(HttpStatusCode.OK, listID.Count);
            });
        }
    }
}
