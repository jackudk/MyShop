using AutoMapper;
using MyShop.Model.Models;
using MyShop.Service;
using MyShop.Web.Infastructure.Core;
using MyShop.Web.Infastructure.Extensions;
using MyShop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService erroservice, IPostCategoryService postCategoryService) : base(erroservice)
        {
            this._postCategoryService = postCategoryService;
        }

        //Get all Post Category request
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var lstPostCategory = _postCategoryService.GetAll();
                var model = Mapper.Map<IEnumerable<PostCategoryViewModel>>(lstPostCategory);
                return request.CreateResponse(HttpStatusCode.OK, model);
            });
        }

        //Add a new Post Category request
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    PostCategory postCategory = new PostCategory();
                    postCategory.ClonePostCategory(postCategoryVM);

                    var model = _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();

                    var responseModel = Mapper.Map<PostCategoryViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseModel);
                }
                return response;
            });
        }

        //Update a Post Category request
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    var postCategory = _postCategoryService.GetByID(postCategoryVM.ID);
                    postCategory.ClonePostCategory(postCategoryVM);

                    _postCategoryService.Update(postCategory);
                    _postCategoryService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        //Delete a Post Category request
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var model = _postCategoryService.Delete(id);
                    _postCategoryService.SaveChanges();

                    var responseModel = Mapper.Map<PostCategoryViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseModel);
                }
                return response;
            });
        }
    }
}