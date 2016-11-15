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
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        private ISlideService _slideService;

        public SlideController(IErrorService erroservice, ISlideService SlideService)
            : base(erroservice)
        {
            this._slideService = SlideService;
        }

        [Route("getallpaging")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRows;
                var SlidesPaging = _slideService.GetAllPaging(keyWord, page, pageSize, out totalRows);

                var responseModel = Mapper.Map<IEnumerable<SlideViewModel>>(SlidesPaging);

                var pagingnationSet = new PaginationSet<SlideViewModel>()
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
                var model = _slideService.GetAll();
                var responseModel = Mapper.Map<IEnumerable<SlideViewModel>>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseModel);
            });
        }

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _slideService.GetByID(id);
                var responseModel = Mapper.Map<SlideViewModel>(model);
                return request.CreateResponse(HttpStatusCode.OK, responseModel);
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, SlideViewModel SlideVM)
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
                    Slide newSlide = new Slide();
                    newSlide.CloneSlide(SlideVM);
                    newSlide.CreatedDate = DateTime.Now;
                    newSlide.CreatedBy = User.Identity.Name;

                    var model = _slideService.Add(newSlide);
                    _slideService.SaveChanges();

                    var responseModel = Mapper.Map<SlideViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseModel);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, SlideViewModel SlideVM)
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
                    Slide dbSlide = _slideService.GetByID(SlideVM.ID);
                    dbSlide.CloneSlide(SlideVM);
                    dbSlide.UpdatedDate = DateTime.Now;
                    dbSlide.UpdatedBy = User.Identity.Name;

                    _slideService.Update(dbSlide);
                    _slideService.SaveChanges();

                    var responseModel = Mapper.Map<SlideViewModel>(dbSlide);
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
                Slide dbSlide = _slideService.Delete(id);
                _slideService.SaveChanges();

                var responseModel = Mapper.Map<SlideViewModel>(dbSlide);
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
                    _slideService.Delete(id);
                }
                _slideService.SaveChanges();

                return request.CreateResponse(HttpStatusCode.OK, listID.Count);
            });
        }
    }
}