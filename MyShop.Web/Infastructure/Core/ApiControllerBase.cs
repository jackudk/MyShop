using MyShop.Model.Models;
using MyShop.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyShop.Web.Infastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorSerive;

        public ApiControllerBase(IErrorService erroservice)
        {
            this._errorSerive = erroservice;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException dbvex)
            {
                foreach (var eve in dbvex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(dbvex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbvex.InnerException.Message);
            }
            catch (DbUpdateException dbex)
            {
                LogError(dbex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                error.CreateDate = DateTime.Now;

                _errorSerive.Add(error);
                _errorSerive.SaveChanges();
            }
            catch
            {
            }
        }
    }
}